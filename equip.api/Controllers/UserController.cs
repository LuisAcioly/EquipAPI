using equip.api.Business.Entities;
using equip.api.Business.Repositories;
using equip.api.Configuration;
using equip.api.Filters;
using equip.api.Infrastructure.Data;
using equip.api.Infrastructure.Data.Repositories;
using equip.api.Models;
using equip.api.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;


        public UserController(IUserRepository userRepository, IConfiguration Configuration, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

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
            var user = _userRepository.GetUser(loginViewModelInput.Login);

            if (user == null) {
                return BadRequest("Houve um erro ao tentar acessar");
            }

            //if (user.Password != loginViewModelInput.Password.GenerateEncryptedPassword())
            //{
            //    return BadRequest("Houve um erro ao tentar acessar");
            //}

            var userViewModelOutput = new UserViewModelOutput() { 
                Code = user.Code,
                Login = user.Login,
                Email = user.Email
            };

            var token = _authenticationService.GenerateToken(userViewModelOutput);

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
            //var optionsBuilder = new DbContextOptionsBuilder<EquipDbContext>();
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=Equip;Integrated Security=True");
            //EquipDbContext context = new EquipDbContext(optionsBuilder.Options);

            //var pendingMigration = context.Database.GetPendingMigrations();

            //if(pendingMigration.Count() > 0)
            //{
            //    context.Database.Migrate();
            //}

            var user = new User();
            user.Login = registerViewModelInput.Login;
            user.Password = registerViewModelInput.Password;
            user.Email = registerViewModelInput.Email;

            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", registerViewModelInput);
        }
    }
}
