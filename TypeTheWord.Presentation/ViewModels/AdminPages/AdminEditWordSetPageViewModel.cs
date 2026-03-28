using Domain.Entities.Models.Db;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TypeTheWord.App.Interfaces;

namespace TypeTheWord.Presentation.ViewModels.AdminPages
{
    [QueryProperty(nameof(WordSetId), "WordSetId")]
    public class AdminEditWordSetPageViewModel : INotifyPropertyChanged
    {
        public string WordSetId { get; set; }
        private bool _isNewWordSet = false;
        IWordSetService _wordSetService;
        public AdminEditWordSetPageViewModel(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;

            SaveWordSetCommand = new Command( async () => await SaveWordSetAsync());
            DeleteWordSetCommand = new Command(async () => await DeleteWordSetAsync());

            AddNewWordCommand = new Command(() => AddNewWord()); //For Collection view entries
            
        }
        public ICommand SaveWordSetCommand { get; set; }
        public ICommand AddNewWordCommand { get; set; }
        public ICommand DeleteWordSetCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WordSet _myWordSet;
        public WordSet MyWordSet { get { return _myWordSet; } set { _myWordSet = value; OnPropertyChanged(nameof(MyWordSet)); } }
        private string _newWord;
        public string NewWord { get { return _newWord; } set { _newWord = value; OnPropertyChanged(nameof(NewWord)); } }

        private ObservableCollection<WordItem> _wordList = new();
        public ObservableCollection<WordItem> WordList { get { return _wordList; } set { _wordList = value; OnPropertyChanged(nameof(WordList)); } }

        public async Task LoadData()
        {
            var wordSet = await _wordSetService.GetOneAsync(WordSetId);

            if (wordSet != null)
            {
                MyWordSet = wordSet;
                WordList = new ObservableCollection<WordItem>(MyWordSet.WordList.Select(x => new WordItem { Text = x}));
            }
            else
            {
                _isNewWordSet = true;
                MyWordSet = new();
            }
        }

        private async Task SaveWordSetAsync()
        {
            if (MyWordSet == null)
                return;

            //Set WordList to collection view entires
            MyWordSet.WordList = WordList.Select(x => x.Text).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            //SAVE NEW
            if (_isNewWordSet == true)
            {
                await _wordSetService.AddAsync(MyWordSet);
                WordSetId = MyWordSet.Id; //Set Id to generated id
                _isNewWordSet = false;    // Word set has been saved. No longer "new"
            }
            //UPDATE Existing
            else
            {
                await _wordSetService.UpdateAsync(MyWordSet);
            }

            await LoadData(); //Refreshpage
        }


        private async Task DeleteWordSetAsync()
        {
            if (MyWordSet == null)
                return;

            if (MyWordSet.Id == null)
                await Shell.Current.GoToAsync("..");

            var response = await Shell.Current.DisplayActionSheet("Do you want to Delete this Wordset?", "Cancle", "Delete");
            if (response.Equals("Delete"))
            {
                await _wordSetService.DeleteAsync(MyWordSet);
                await Shell.Current.GoToAsync("..");
            }
        }

        //For Collection view entries
        private void AddNewWord()
        {
            if(string.IsNullOrWhiteSpace(NewWord))
                return;

            WordList.Add(new WordItem { Text = NewWord });
            NewWord = "";
        }


    }


    //Move somewhere else? Used for updating Collection view
    public class WordItem
    {
        public string Text { get; set; }
    }
}
