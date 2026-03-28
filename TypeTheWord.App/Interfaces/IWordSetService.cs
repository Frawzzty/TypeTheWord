using Domain.Entities.Models.Db;

namespace TypeTheWord.App.Interfaces
{
    public interface IWordSetService
    {
        public Task<WordSet?> GetOneAsync(string id);
        public Task<List<WordSet>> GetAllAsync();


        public Task AddAsync(WordSet wordSet);
        public Task UpdateAsync(WordSet wordSet);

        public Task DeleteAsync(WordSet wordSet);

    }
}
