using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.DataAccess.Configure
{
    public class MemberConfigure : EntityConfigure<Member>
    {
        protected override void ConfigureHelper(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(x=>x.HasComment("會員表"));
            builder.Property(x=>x.CreditCardNumber).HasMaxLength(20).HasComment("信用卡號");
            builder.Property(x => x.UserId).HasComment("使用者ID");
        }
    }
}
