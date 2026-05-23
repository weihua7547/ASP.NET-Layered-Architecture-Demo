using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.DataAccess.Configure
{
    public class TimeSlotConfigure : EntityConfigure<TimeSlot>
    {
        protected override void ConfigureHelper(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.ToTable(x=>x.HasComment("場地預約時間段表"));
            builder.Property(x => x.FieldId).HasComment("場地ID");
            builder.Property(x => x.OrderId).HasComment("訂單ID");
            builder.Property(x => x.StartTime).HasComment("開始時間");
            builder.Property(x => x.EndTime).HasComment("結束時間");
        }
    }
}
