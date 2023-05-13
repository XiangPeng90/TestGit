using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Entities.Config
{
    internal class ShopInfoConfig : IEntityTypeConfiguration<ShopInfo>
    {
        public void Configure(EntityTypeBuilder<ShopInfo> builder)
        {
            builder.ToTable("t_ShopInfo");
            builder.HasKey(s => s.id);
            builder.HasOne(t => t.Shop).WithOne(s => s.ShopInfo).HasForeignKey<ShopInfo>(s => s.ShopId);
            //builder.Property(s => s.FavorableRate).HasConversion<Star>();
        }
    }
}
