using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())//IDisposable pattern implementation of c#
            {
                var addingData = context.Entry(entity);
                addingData.State = EntityState.Added;
                context.SaveChanges();

                //return true;
            }
        }

        public bool Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletingData = context.Entry(entity);
                deletingData.State = EntityState.Deleted;
                context.SaveChanges();

                return true;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public bool Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updateData = context.Entry(entity);
                updateData.State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
        }
    }
}
