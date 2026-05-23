using Badminton.Contract;
using Badminton.DataAccess;
using Badminton.Model.Abstract;
using Badminton.Model.CustomException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Badminton.Contract.IDataGenericService;

namespace Badminton.Service
{
    public class DataGenericService<T> : IDataGenericService<T> where T : Entity
    {
        private readonly BadmintonDbContext _dbContext;
        public DataGenericService(BadmintonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T DeleteSoft(int id)
        {
            var existingEntity = _dbContext.Set<T>().Find(id);
            if (existingEntity != null)
            {
                existingEntity.GenerateDeleteKey();
                _dbContext.SaveChanges();
                return existingEntity;
            }
            else
            {
                throw new EntityNotExistException();
            }
        }

        public T Update(T entity)
        {
            var existingEntity = _dbContext.Set<T>().Find(entity.Id);
            if (existingEntity == null) throw new EntityNotExistException();
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
            return existingEntity;
        }



        public int GetCreatedCount(DateTime targetDateTime, GetCreatedCountTimeRanges range)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            query = range switch
            {
                GetCreatedCountTimeRanges.Year => query.Where(x => x.CreatedDateTime.Year == targetDateTime.Year),
                GetCreatedCountTimeRanges.Month => query.Where(x => x.CreatedDateTime.Year == targetDateTime.Year &&
                                                             x.CreatedDateTime.Month == targetDateTime.Month),
                GetCreatedCountTimeRanges.Day => query.Where(x => x.CreatedDateTime.Year == targetDateTime.Year &&
                                                             x.CreatedDateTime.Month == targetDateTime.Month &&
                                                             x.CreatedDateTime.Day == targetDateTime.Day),
                _ => throw new ArgumentException("Invalid time range specified"),
            };

            return query.IgnoreQueryFilters().Count();
        }
    }
}
