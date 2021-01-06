using equip.api.Business.Entities;
using equip.api.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EquipDbContext _context;

        public UserRepository(EquipDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.User.Add(user);
        }

        public void Commit()
        {
           _context.SaveChanges();
        }

        public User GetUser(string login)
        {
            return _context.User.FirstOrDefault(u => u.Login == login);
        }
    }
}
