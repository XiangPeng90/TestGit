using System;
using Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("T_Order");
            builder.HasKey(o => o.id);
            builder.Property(o => o.ordertime).IsRequired().HasMaxLength(20);
            builder.HasOne(o=>o.shop).WithMany(o => o.orders).HasForeignKey(o => o.ShopId);
            builder.HasOne(o=>o.user).WithMany(u => u.orders).HasForeignKey(o => o.UserId);
            builder.HasMany(o=>o.Foods).WithMany(f=>f.orders);
            builder.HasMany(o => o.views).WithOne();
        }
    }
}