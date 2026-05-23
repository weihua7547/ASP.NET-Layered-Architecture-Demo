using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.DataAccess.Configure
{
    public class UserConfigure : EntityConfigure<User>
    {
        protected override void ConfigureHelper(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(x => x.HasComment("使用者"));
            builder.Property(x=>x.Account).HasMaxLength(20).IsRequired().HasComment("帳號");
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired().HasComment("密碼");
            builder.Property(x=>x.Email).HasMaxLength(50).IsRequired().HasComment("電子郵件");
            builder.Property(x => x.Phone).HasMaxLength(20).IsRequired().HasComment("電話");
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired().HasComment("姓名");
            builder.HasMany(x=>x.Roles).WithOne(x=>x.User).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
