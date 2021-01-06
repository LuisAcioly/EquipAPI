using equip.api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Configuration
{
    public interface IAuthenticationService
    {
        public string GenerateToken(UserViewModelOutput userViewModelOutput);
    }
}
