using Badminton.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Model
{
    public class Field:Entity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<TimeSlot>? TimeSlots { get; set; }
    }
}
