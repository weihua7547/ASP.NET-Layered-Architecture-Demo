using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Global
{
    public class OrderBy
    {
        public enum SortType
        {
            Ascending,
            Descending
        }

        public required string SortBy { get; set; } 
        public SortType SortOrder { get; set; } 
    }
}
