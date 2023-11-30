using System;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T>  where T : class

    {
        private readonly CareerCloudContext context = new CareerCloudContext();

        private DbSet<T> entity;

        public EFGenericRepository()
        {
            entity = context.Set<T>();
        }

        public void Add(params T[] items)
        {
            entity.AddRange(items);
            context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
            
        }

        public IList<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = context.Set<T>();

            if (where != null)
            {
                query = query.Where(where);
            }
            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            T result = query.SingleOrDefault();

            return result;
        }

        public void Remove(params T[] items)
        {
            entity.RemoveRange(items);
            context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            entity.UpdateRange(items);
            context.SaveChanges();
        }
    }
}

