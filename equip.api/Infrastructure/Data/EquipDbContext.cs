using equip.api.Business.Entities;
using equip.api.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Infrastructure.Data
{
    public class EquipDbContext : DbContext
    {
        public EquipDbContext(DbContextOptions<EquipDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EquipMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> User { get; set; }

        public DbSet<Equip> Equip { get; set; }
    }
}
