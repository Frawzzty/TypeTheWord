using Domain.Entities.Models.Db;
using Domain.Entities.Models.Game;
using System.Diagnostics;
using TypeTheWord.App.Interfaces;

namespace TypeTheWord.App.Services
{
    public class WordGameService : IWordGameService
    {
        //Settings
        private int _wordAmountOnRows = 5;


        private readonly IWordSetService _wordSetService;
        public WordGameService(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;
            _result = new WordGameResult();
        }

        public WordSet TheWordSet { get; set; } // Make private

        private bool _isCompleted;
        private WordGameResult _result;

        public List<string> WordRowTop { get; set; } = new List<string>();
        public List<string> WordRowBottom { get; set; } = new List<string>();

        public bool IsGameCompleted()
            => _isCompleted;
        public string GetCurrentWord()
            => WordRowTop.First();
        public WordGameResult GetResult()
            => _result;

        public void Start()
        {
            Debug.WriteLine("Game started");
        }

        public void Finish()
        {
            _isCompleted = true;
            Debug.WriteLine("Game Finnished");
        }

        private void ResetGame()
        {
            _isCompleted = false;
            _result = new();

            Debug.WriteLine("Game reset");
        }

        public async Task InitializeGame(string wordsetId, int gameLengthSeconds)
        {
            ResetGame();
            _result.TimeSettingSeconds = gameLengthSeconds; //Make better?

            TheWordSet = await _wordSetService.GetOneAsync(wordsetId);

            if (TheWordSet == null)
                return;

            SetFirstWords();

            Debug.WriteLine("Game Initialized");
        }



        public void LoadNextWord()
        {
            if(_isCompleted)
                return;

            WordRowTop.RemoveAt(0);

            if (WordRowTop.Count == 0)
                UpdateWordRows();
        }

        private void UpdateWordRows()
        {
            WordRowTop = WordRowBottom;
            WordRowBottom = GetRandomWords(_wordAmountOnRows);
        }

        private void SetFirstWords()
        {
            if (TheWordSet.WordList != null && TheWordSet.WordList.Count > 0)
            {
                WordRowTop = GetRandomWords(_wordAmountOnRows);
                WordRowBottom = GetRandomWords(_wordAmountOnRows);

                Debug.WriteLine($"{typeof(WordGameService)}: WordList: Loaded");
                return;
            }

            Debug.WriteLine($"{typeof(WordGameService)}: WordList: Could not load");
        }

        private List<string> GetRandomWords(int amount)
        {
            List<string> words = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                string word = TheWordSet.WordList[Random.Shared.Next(0, TheWordSet.WordList.Count)];
                words.Add(word);
            }
            return words;
        }


        public bool TryCurrentWord(string input)
        {
            bool IsCorrect;

            //Correct
            if (GetCurrentWord() == input)
            {
                _result.WordsCorrect++;
                _result.StreakAmount += 0.2f;
                IsCorrect = true;
                Debug.WriteLine("Correct word");

                if (_result.StreakAmount == 1) //1 is max for MAUI progressbar
                {
                    _result.TotalStreaks++;
                    _result.StreakAmount = 0;
                }
                
            }
            else //Wrong
            {
                _result.WordsWrong++;
                _result.StreakAmount = 0;
                IsCorrect = false;
                Debug.WriteLine("Wrong word");
            }


                


            //Update words if needed
            if (WordRowTop.Count == 0)
                UpdateWordRows();

            LoadNextWord();

            return IsCorrect;
        }

    }
}
