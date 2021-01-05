using equip.api.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equip.api.Infrastructure.Data.Mappings
{
    public class EquipMapping : IEntityTypeConfiguration<Equip>
    {
        public void Configure(EntityTypeBuilder<Equip> builder)
        {
            builder.ToTable("TB_EQUIPMENT");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code).ValueGeneratedOnAdd();
            builder.Property(p => p.Name);
            builder.Property(p => p.Damage);
            builder.HasOne(p => p.User)
                .WithMany().HasForeignKey(fk => fk.UserCode);
        }
    }
}
