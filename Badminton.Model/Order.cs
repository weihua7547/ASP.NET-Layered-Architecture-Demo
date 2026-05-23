using Badminton.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Model
{
    public class Order : Entity
    {
        public PayType PaymentMethod { get; set; }
        public int Price { get; set; }
        public int MemberId { get; set; }
        public StateType State { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PayDate { get; set; }

        public string Context { get; set; } = string.Empty;
        public int Hours { get; set; }
        public ICollection<TimeSlot>? TimeSlots { get; set; }

    }

    public enum PayType
    {
        Cash = 0,
        Credit = 1,
        LinePay = 2,
        ApplePay = 3,
    }
    public enum StateType
    {
        Ordering = 0,
        HaveBeenPaid = 1,
        Cancelled = 2,
        CanceledWithRefund = 3,
    }
}
