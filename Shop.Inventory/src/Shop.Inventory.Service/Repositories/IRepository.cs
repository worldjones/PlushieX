using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shop.Inventory.Service.Entities;

namespace Shop.Inventory.Service.Repositories
{
    public interface IRepository<E> where E : IEntity
    {
        Task CreateAsync(E entity);

        Task<IReadOnlyCollection<E>> GetAllAsync();

        Task<IReadOnlyCollection<E>> GetAllAsync(Expression<Func<E, bool>> filter);

        Task<E> GetAsync(Guid id);

        Task<E> GetAsync(Expression<Func<E, bool>> filter);

        Task RemoveAsync(Guid id);

        Task UpdateAsync(E entity);
    }
}