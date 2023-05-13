using System;
using Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Config
{
    public class FoodConfig : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("T_Food");
            builder.HasKey(f => f.id);
            builder.Property(f => f.price).IsRequired().HasMaxLength(5);
            builder.Property(f => f.f_name).IsRequired().HasMaxLength(8);
            builder.Property(f=>f.description).HasMaxLength(40);
            builder.HasOne(f=>f.menu).WithMany(m => m.foods).HasForeignKey(f=>f.menuId);
            builder.HasMany(f => f.trolleys).WithMany(t => t.foods);
        }
    }
}

