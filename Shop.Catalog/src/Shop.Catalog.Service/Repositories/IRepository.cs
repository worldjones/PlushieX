using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Catalog.Service.Entities;

namespace Shop.Catalog.Service.Repositories
{
    public interface IRepository<E> where E : IEntity
    {
        Task CreateAsync(E entity);

        Task<IReadOnlyCollection<E>> GetAllAsync();

        Task<E> GetAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task UpdateAsync(E entity);
    }
}