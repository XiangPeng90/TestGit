using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;

namespace Entities.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_User");
            builder.HasKey(u => u.id);
            builder.Property(u => u.u_account).IsRequired().HasMaxLength(20);
            builder.Property(u => u.u_name).IsRequired().HasMaxLength(20);
            builder.Property(u => u.u_password).IsRequired().HasMaxLength(20);
            builder.Property(u => u.u_regist_time).IsRequired();
            builder.Ignore(u => u.u_Confirmpassword);
            builder.Property(u => u.phone_number).HasMaxLength(12);
            builder.HasOne(u => u.trolley).WithOne(t => t.user).HasForeignKey<Trolley>(t=>t.userId);
        }
    }
}