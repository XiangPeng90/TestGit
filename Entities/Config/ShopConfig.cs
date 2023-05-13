using System;
using Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Config
{
    public class ShopConfig : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("T_Shop");
            builder.HasKey(s => s.id);
            builder.Property(s => s.s_account).IsRequired().HasMaxLength(10);
            builder.Property(s => s.s_name).IsRequired().HasMaxLength(16);
            builder.Property(s => s.s_password).IsRequired().HasMaxLength(16);
            builder.Property(s => s.s_regist_time).IsRequired();
            builder.HasOne(s => s.menu).WithOne(m=>m.shop);
            builder.Ignore(s => s.s_Confirmpassword);
            builder.Property(s=>s.address).HasMaxLength(50);
            builder.Property(s => s.phone_number).HasMaxLength(12);
        }
    }
}