using equip.api.Business.Entities;
using equip.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Infrastructure.Data.Repositories
{
    public class EquipRepository : IEquipRepository
    {

        private readonly EquipDbContext _context;

        public EquipRepository(EquipDbContext context)
        {
            _context = context;
        }

        public void Add(Equip equip)
        {
            _context.Equip.Add(equip);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Equip> GetByUser(int userCode)
        {
            return _context.Equip.Include(i => i.User).Where(w => w.UserCode == userCode).ToList();
        }
    }
}
