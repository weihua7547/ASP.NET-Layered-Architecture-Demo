using System;
using System.Collections.Generic;
using System.Text;

namespace Badminton.Model.Abstract
{
    public class Entity
    {
        public int Id { get; set; }
        public Guid DeleteKey { get; set; }
        public int CreatorId { get; set;  }
        public DateTime CreatedDateTime { get; set; }
        public int UpdaterId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public void GenerateDeleteKey()
        {
            DeleteKey = Guid.NewGuid();
        }
    }
}
