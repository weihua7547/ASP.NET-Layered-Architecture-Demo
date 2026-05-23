using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Model.Global
{
    public class Page
    {
        /// <summary>
        /// 頁數索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 總筆數
        /// </summary>
        public int? Total { get; set; }

        /// <summary>
        /// 單頁大小
        /// </summary>
        public int PageSize { get; set; } = 100;

        public Page(int index)
        {
            Index = index;
        }

    }
}
