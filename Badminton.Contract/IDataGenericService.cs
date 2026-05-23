using Badminton.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badminton.Contract
{
    public interface IDataGenericService
    {
        public enum GetCreatedCountTimeRanges
        {
            Year, Month, Day
        }
    }

    public interface IDataGenericService<T> : IDataGenericService where T : Entity
    {
        public T Create(T entity);
        public T Update(T entity);
        public T DeleteSoft(int id);

        /// <summary>
        /// 取得指定時間點創建的實體的數量
        /// </summary>
        /// <param name="targetDateTime">指定時間</param>
        /// <param name="range">指定範圍(年、月、日)</param>
        /// <returns></returns>
        public int GetCreatedCount(DateTime targetDateTime, GetCreatedCountTimeRanges range);


    }
}
