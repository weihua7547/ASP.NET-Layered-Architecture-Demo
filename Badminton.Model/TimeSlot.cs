using Badminton.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Model
{
    public class TimeSlot:Entity
    {
        public int FieldId { get; set; }
        public int OrderId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
