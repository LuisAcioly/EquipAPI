using equip.api.Business.Entities;
using equip.api.Filters;
using equip.api.Infrastructure.Data;
using equip.api.Models;
using equip.api.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace equip.api.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Autenticação de usuario já cadastrado e ativo
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retorna status ok, dados do usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidationFieldViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("login")]
        [CustomValidationModelState]
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            var userViewModelOutput = new UserViewModelOutput() { 
                Code = 1,
                Login = "luisw",
                Email = "luiswagner8@gmail.com"
            };

            var secret = Base64UrlEncoder.DecodeBytes("9ST5hQe5dUNfAJOQZAtt19uiDhNtKKUt");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuario = userViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permitecadastrar um usuario cadastrado não existente
        /// </summary>
        /// <param name="registerViewModelInput">ViewModel do registro de login</param>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidationFieldViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("register")]
        [CustomValidationModelState]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EquipDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=Equip;Integrated Security=True");
            EquipDbContext context = new EquipDbContext(optionsBuilder.Options);

            var pendingMigration = context.Database.GetPendingMigrations();

            if(pendingMigration.Count() > 0)
            {
                context.Database.Migrate();
            }

            var user = new User();
            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;
            user.Email = registerViewModelInput.Email;

            context.User.Add(user);
            context.SaveChanges();

            return Created("", registerViewModelInput);
        }
    }
}
