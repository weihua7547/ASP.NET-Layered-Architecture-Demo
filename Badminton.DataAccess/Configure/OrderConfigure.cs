using Badminton.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.DataAccess.Configure
{
    public class OrderConfigure : EntityConfigure<Order>
    {
        protected override void ConfigureHelper(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(x => x.HasComment("訂單表"));
            builder.Property(x=>x.PaymentMethod).HasComment("支付方式");
            builder.Property(x => x.Price).HasComment("價格");
            builder.Property(x => x.MemberId).HasComment("會員ID");
            builder.Property(x => x.State).HasComment("訂單狀態");
            builder.Property(x => x.OrderDate).HasComment("訂單日期");
            builder.Property(x => x.PayDate).HasComment("支付日期");
            builder.Property(x => x.Context).HasComment("訂單備註");
            builder.Property(x => x.Hours).HasComment("訂單預約小時數");
            builder.HasMany(x => x.TimeSlots).WithOne().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
