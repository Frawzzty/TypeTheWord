using Domain.Entities.Interface;
using Domain.Entities.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeTheWord.App.Interfaces;

namespace TypeTheWord.App.Services
{
    public class WordSetService : IWordSetService
    {
        private IwordSetRepository _wordSetRepository;
        public WordSetService(IwordSetRepository wordSetRepository)
        {
            _wordSetRepository = wordSetRepository;
        }

        public async Task<WordSet?> GetOneAsync(string id)
        {
            return await _wordSetRepository.GetOneAsync(id);
        }
        public async Task<List<WordSet>> GetAllAsync()
        {
            return await _wordSetRepository.GetAllAsync();
        }


        public async Task AddAsync(WordSet wordSet)
        {

                
            await _wordSetRepository.AddAsync(wordSet);
        }
        public async Task UpdateAsync(WordSet wordSet)
        {
            await _wordSetRepository.UpdateAsync(wordSet);
        }

        public async Task DeleteAsync(WordSet wordSet)
        {
            await _wordSetRepository.DeleteAsync(wordSet);
        }

    }
}
