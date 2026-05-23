using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Global
{
    /// <summary>
    /// 包裝回傳清單結果的物件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResultPackage<T>
    {
        /// <summary>
        /// 頁數物件
        /// </summary>
        public Page? Page { get; set; }
        /// <summary>
        /// 結果
        /// </summary>
        public ICollection<T>? Result { get;  }

        public QueryResultPackage(ICollection<T>? result)
        {
            Result = result;
        }
    }
}
