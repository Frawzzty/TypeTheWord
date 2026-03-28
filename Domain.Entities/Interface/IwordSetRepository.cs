using Domain.Entities.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Interface
{
    public interface IwordSetRepository
    {
        public Task<WordSet> GetOneAsync(string id);
        public Task<List<WordSet>> GetAllAsync();
        
        public Task AddAsync(WordSet wordSet);
        public Task UpdateAsync(WordSet wordSet);
        public Task DeleteAsync(WordSet wordSet);
    }
}
