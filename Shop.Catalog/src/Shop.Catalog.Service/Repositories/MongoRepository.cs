using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Driver;
using Shop.Catalog.Service.Entities;

namespace Shop.Catalog.Service.Repositories
{

    public class MongoRepository<E> : IRepository<E> where E : IEntity
    {
    
        private readonly IMongoCollection<E> dbCollection;
        private readonly FilterDefinitionBuilder<E> filterBuilder = Builders<E>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        
        {
            dbCollection = database.GetCollection<E>(collectionName);
        }

        public async Task<IReadOnlyCollection<E>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<E> GetAsync(Guid id)
        {
            FilterDefinition<E> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(E entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(E entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<E> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<E> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}