using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Domain.DataAccess
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            this.context = new EfContext();
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "", bool tracking = true)
        {
            IQueryable<TEntity> query = dbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (!tracking)
            {
                return query.AsNoTracking<TEntity>().SingleOrDefault();
            }
            else
            {
                return query.SingleOrDefault();
            }
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool tracking = true)
        {
            IQueryable<TEntity> query = dbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (!tracking)
                {
                    return orderBy(query).AsNoTracking<TEntity>().ToList();
                }
                else
                {
                    return orderBy(query).ToList();
                }
            }
            else
            {
                if (!tracking)
                {
                    return query.AsNoTracking<TEntity>().ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }

        public virtual IEnumerable<TEntity> GetByMultipleConditions(
            List<Expression<Func<TEntity, bool>>> filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool tracking = true)
        {
            IQueryable<TEntity> query = dbSet;


            if (filters != null)
            {
                foreach (Expression<Func<TEntity, bool>> filter in filters)
                {
                    query = query.Where(filter);   
                }
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (!tracking)
                {
                    return orderBy(query).AsNoTracking<TEntity>().ToList();
                }
                else
                {
                    return orderBy(query).ToList();
                }
            }
            else
            {
                if (!tracking)
                {
                    return query.AsNoTracking<TEntity>().ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
        }

        public virtual TEntity First(
    Expression<Func<TEntity, bool>> filter = null,
    string includeProperties = "", bool tracking = true)
        {
            IQueryable<TEntity> query = dbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }


            if (!tracking)
            {
                return query.AsNoTracking<TEntity>().FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public virtual IEnumerable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool tracking = true, int skip = 0, int limit = 0)
        {
            IQueryable<TEntity> query = dbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (!tracking)
                {
                    query = orderBy(query);

                    query = query.Skip(skip);

                    if (limit != 0)
                    {
                        query = query.Take(limit);
                    }
                    return orderBy(query).AsNoTracking<TEntity>().ToList();
                }
                else
                {
                    query = orderBy(query);

                    query = query.Skip(skip);

                    if (limit != 0)
                    {
                        query = query.Take(limit);
                    }
                    return orderBy(query).ToList();
                }
            }
            else
            {
                if (!tracking)
                {

                    query = query.Skip(skip);

                    if (limit != 0)
                    {
                        query = query.Take(limit);
                    }
                    return query.AsNoTracking<TEntity>().ToList();
                }
                else
                {

                    query = query.Skip(skip);

                    if (limit != 0)
                    {
                        query = query.Take(limit);
                    }
                    return query.ToList();
                }
            }
        }

        public virtual TEntity GetByID(object id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity != null)
            {
                context.Entry<TEntity>(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry<TEntity>(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (context.Entry<TEntity>(entityToUpdate).State == EntityState.Detached)
            {
                dbSet.Attach(entityToUpdate);
            }

            context.Entry<TEntity>(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
