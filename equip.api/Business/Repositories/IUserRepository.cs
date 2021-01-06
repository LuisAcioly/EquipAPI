using equip.api.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        User GetUser(string login);
    }
}
