using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Contract.DTO
{
    /// <summary>
    /// 實體物件Key的DTO
    /// </summary>
    public class EntityKeyDTO
    {
        /// <summary>
        /// 序號
        /// </summary>
        public required int Id { get; set; }
    }
}
