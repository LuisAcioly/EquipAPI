using equip.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Configuration
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<EquipDbContext>
    {

        public EquipDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EquipDbContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=Equip;Integrated Security=True");

            EquipDbContext context = new EquipDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
