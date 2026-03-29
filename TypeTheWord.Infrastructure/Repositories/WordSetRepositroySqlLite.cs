using Domain.Entities.Interface;
using Domain.Entities.Models.Db;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeTheWord.Infrastructure.Connections;

namespace TypeTheWord.Infrastructure.Repositories
{
    public class WordSetRepositroySqlLite : IwordSetRepository
    {
        private readonly MongoDbConnection _mongoDbConnection;
        private readonly IMongoCollection<WordSet> _collection;

        public WordSetRepositroySqlLite(MongoDbConnection connection)
        {
            _mongoDbConnection = connection;
            _collection = connection.GetDatabase().GetCollection<WordSet>("WordSetCollection"); // Creates new Collection in DB if does not exists
        }

        public async Task<WordSet?> GetOneAsync(string id)
        {
            try
            {
                return await _collection.Find(x => x.Id == id).FirstAsync();
            }
            catch (InvalidOperationException)
            {
                Debug.WriteLine($"Error: WordSetRepository GetOneAsync: 'Sequence contains no elements' ID:{id}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"WordSetRepo GetOneAsync Something went wrong  ID:{id}");
            }

            return null;

        }
        public async Task<List<WordSet>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }


        public async Task AddAsync(WordSet wordSet)
        {
            await _collection.InsertOneAsync(wordSet);
        }
        public async Task UpdateAsync(WordSet wordSet)
        {
            var filter = Builders<WordSet>.Filter.Eq(x => x.Id, wordSet.Id);
            await _collection.ReplaceOneAsync(filter, wordSet);
        }

        public async Task DeleteAsync(WordSet wordSet)
        {
            var filter = Builders<WordSet>.Filter.Eq(x => x.Id, wordSet.Id);
            await _collection.DeleteOneAsync(filter);
        }

    }
}
