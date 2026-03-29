using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TypeTheWord.App.Interfaces;

namespace TypeTheWord.Presentation.ViewModels.PlayPages
{
    [QueryProperty(nameof(WordSetId),"WordSetId")]
    public class WordGamePlayPageViewModel : INotifyPropertyChanged
    {
        //Settings
        private int _gameLengthSeconds = 10;

        public string WordSetId = "";
        public bool IsActivePage;
        private bool _isStarted;

        private IWordGameService _wordGameService;
        public WordGamePlayPageViewModel(IWordGameService wordGameService)
        {
            _wordGameService = wordGameService;
            TestWordCommand = new Command(async () => { await SendWordAsync(); });
        }
        public ICommand TestWordCommand { get; set; }

        #region Property changed
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string nameOfProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameOfProperty));
        }
        #endregion

        //LOAD DATA
        public async Task LoadData()
        {
            _isStarted = false;
            CountDownTimer = _gameLengthSeconds.ToString();
            
            try 
            { 
                await _wordGameService.InitializeGame(WordSetId, _gameLengthSeconds); 
            }
            catch (Exception ex) 
            { 
                Shell.Current.DisplayAlert("ERROR", $"Could not Initializegame\n{ex.Message}", "OK"); 
            }

            WordsRowTop = UpdateWordRowTop();
            WordsRowBottom = new ObservableCollection<string>(_wordGameService.WordRowBottom);

        }

        private string _wordInput = "";
        public string WordInput { get { return _wordInput; } set { _wordInput = value; OnPropertyChanged(nameof(WordInput)); } }

        private float _streakAmount;
        private int _totalStreaks;
        public float StreakAmount { get { return _streakAmount; } set { _streakAmount = value; OnPropertyChanged(nameof(StreakAmount)); } }
        public int TotalStreaks { get { return _totalStreaks; } set { _totalStreaks = value; OnPropertyChanged(nameof(TotalStreaks)); } }
        private async Task SendWordAsync()
        {
            if (string.IsNullOrEmpty(WordInput))
                return;

            if (_isStarted == false)
            {
                _isStarted = true;
                RunGameAsync(); //No need to await //Make start check outside this, only run once?
            }

            if (_wordGameService.TrySubmitWord(WordInput))
            {
                UpdateWordsUI();
                WordInput = "";
            }

        }

        private async Task RunGameAsync()
        {
            _wordGameService.Start();
            StartCountDown();

            await Task.Delay(_gameLengthSeconds * 1000); //Convert To Seconds

            if(IsActivePage)
                await GoResultPageAsync();
        }


        private string _countDownTimer = "";
        public string CountDownTimer { get { return _countDownTimer; } set { _countDownTimer = value; OnPropertyChanged(nameof(CountDownTimer)); } }
        private async Task StartCountDown()
        {
            int countdown = _gameLengthSeconds;
            while (true)
            {
                await Task.Delay(1000);
                countdown -= 1;
                CountDownTimer = countdown.ToString();

                if (countdown == 0 || IsActivePage == false)
                    break;
            }
        }

        private async Task GoResultPageAsync()
        {
            await Shell.Current.Navigation.PushAsync(new Views.PlayPages.WordGameResultPage(_wordGameService.GetResult()));
        }


        //Word Rows Dispaly vars
        private ObservableCollection<WordGameText> _wordsRowTop = new ObservableCollection<WordGameText>();
        private ObservableCollection<string> _wordsRowBottom = new ObservableCollection<string>();
        public ObservableCollection<WordGameText> WordsRowTop { get { return _wordsRowTop; } set { _wordsRowTop = value; OnPropertyChanged(nameof(WordsRowTop)); } }
        public ObservableCollection<string> WordsRowBottom { get { return _wordsRowBottom; } set { _wordsRowBottom = value; OnPropertyChanged(nameof(WordsRowBottom)); } }

        private void UpdateWordsUI()
        {
            var result = _wordGameService.GetResult();
            StreakAmount = result.StreakAmount; //Updating streak progressbar
            TotalStreaks = result.TotalStreaks;

            
            WordsRowTop = UpdateWordRowTop();
            WordsRowBottom = new ObservableCollection<string>(_wordGameService.WordRowBottom);
        }

        private ObservableCollection<WordGameText> UpdateWordRowTop() 
        {
            ObservableCollection<WordGameText> MyWordGameTexts = new ObservableCollection<WordGameText>();
            for (int i = 0; i < _wordGameService.WordRowTop.Count; i++)
            {
                var myWordGameText = new WordGameText();
                myWordGameText.Text = _wordGameService.WordRowTop[i];

                if (i == 0)
                    myWordGameText.TextColor = Colors.Yellow;
                else
                    myWordGameText.TextColor = Colors.White;

                MyWordGameTexts.Add(myWordGameText);
            }
            return MyWordGameTexts;
        }
    }



    //Used for setting differet textcolors in Collection view depending on index. Can be used for other things to.
    public class WordGameText
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }

    }

}
