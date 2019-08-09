using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Interfaces;

namespace Vet_Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> List();

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        TEntity GetByID(int id);

        void Update(TEntity entity);

        void Insert(TEntity entity);

        void Delete(int id);
    }
}
