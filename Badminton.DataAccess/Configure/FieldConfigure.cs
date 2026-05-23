using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.DataAccess.Configure
{
    internal class FieldConfigure : EntityConfigure<Field>
    {
        protected override void ConfigureHelper(EntityTypeBuilder<Field> builder)
        {
            builder.ToTable(x => x.HasComment("場地資料表"));
            builder.Property(x=>x.Code).HasMaxLength(20).IsRequired().HasComment("場地代碼");
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().HasComment("場地名稱");
            builder.Property(x => x.Description).HasMaxLength(200).HasComment("場地描述");
            builder.HasMany(x => x.TimeSlots).WithOne().HasForeignKey(x => x.FieldId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
