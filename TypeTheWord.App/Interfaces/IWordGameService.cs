using Domain.Entities.Models.Db;
using Domain.Entities.Models.Game;

namespace TypeTheWord.App.Interfaces
{
    public interface IWordGameService
    {
        public WordSet TheWordSet { get; set; }

        public List<string> WordRowTop { get; set; }
        public List<string> WordRowBottom { get; set; }

        public Task InitializeGame(string wordsetId, int gameLengthSeconds);
        public WordGameResult GetResult();
        public bool TryCurrentWord(string input);
        public void LoadNextWord();
        public bool IsGameCompleted();


        public string GetCurrentWord();
        public void Start();
        public void Finish();
    }
}
