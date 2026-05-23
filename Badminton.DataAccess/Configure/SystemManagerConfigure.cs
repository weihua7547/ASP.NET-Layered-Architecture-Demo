using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.DataAccess.Configure
{
    public class SystemManagerConfigure : EntityConfigure<SystemManager>
    {
        protected override void ConfigureHelper(EntityTypeBuilder<SystemManager> builder)
        {
            builder.ToTable(x => x.HasComment("系統管理員"));
            builder.Property(x=>x.Code).HasComment("編號").IsRequired();
            builder.Property(x=>x.UserId).HasComment("使用者Id").IsRequired();
        }
    }
}
