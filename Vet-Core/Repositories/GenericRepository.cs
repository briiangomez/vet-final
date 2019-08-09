using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Context;

namespace Vet_Core.Repositories
{
    public class GenericRepository<T> where T : class
    {
        internal VetDbContext context = new VetDbContext();
        internal DbSet<T> dbSet;

        public GenericRepository()
        {
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> List(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

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
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IQueryable<T> Query(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

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
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query;
            }
        }

        public virtual T Find(
            Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);

            List<T> lstTemp = query.ToList();

            if (lstTemp.Count > 0)
            {
                return lstTemp[0];
            }
            else
            {
                return null;
            }
        }

        public virtual T Find(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual void BatchAdd(IEnumerable<T> entity)
        {
            dbSet.AddRange(entity);
        }
        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Update(T entityToUpdate, Func<T, int> getKey)
        {
            var entry = context.Entry<T>(entityToUpdate);

            if (entry.State == EntityState.Detached)
            {
                T attachedEntity = dbSet.Find(getKey(entityToUpdate));

                if (attachedEntity != null)
                {
                    var attachedEntry = context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entityToUpdate);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public virtual void Unmark(T entity)
        {
            context.Entry(entity).State = EntityState.Unchanged;
        }

        public virtual IQueryable<T> Queryable()
        {
            return dbSet.AsQueryable<T>();
        }

        public virtual void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }

}
