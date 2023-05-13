using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Entities.Config
{
    public class ViewConfig : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
            builder.ToTable("T_View");
            builder.HasKey(v => v.id);
            builder.Property(v => v.view).HasMaxLength(40);
            builder.HasOne(v => v.order).WithMany(o => o.views).HasForeignKey(v=>v.OrderId);            
        }
    }
}
