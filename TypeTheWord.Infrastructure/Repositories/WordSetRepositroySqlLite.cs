using Domain.Entities.Interface;
using Domain.Entities.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly SqlLiteDbContext _db;

        public WordSetRepositroySqlLite(SqlLiteDbContext sqlLitedbContext)
        {
            _db = sqlLitedbContext;
        }

        public async Task<WordSet?> GetOneAsync(string id)
        {
            await _db.Database.EnsureCreatedAsync();

            try
            {
                
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
            await _db.Database.EnsureCreatedAsync();
            return null;
        }


        public async Task AddAsync(WordSet wordSet)
        {
            await _db.Database.EnsureCreatedAsync();

        }
        public async Task UpdateAsync(WordSet wordSet)
        {
            await _db.Database.EnsureCreatedAsync();

        }

        public async Task DeleteAsync(WordSet wordSet)
        {
            await _db.Database.EnsureCreatedAsync();

        }

    }
}
