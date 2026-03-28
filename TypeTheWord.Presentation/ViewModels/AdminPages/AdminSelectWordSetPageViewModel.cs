using Domain.Entities.Models.Db;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TypeTheWord.App.Interfaces;

namespace TypeTheWord.Presentation.ViewModels.AdminPages
{
    public class AdminSelectWordSetPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string nameofProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameofProperty));
        }


        private IWordSetService _wordSetService;
        public AdminSelectWordSetPageViewModel(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;
            AddNewWordSetCommand = new Command(async () => await Shell.Current.GoToAsync($"{nameof(Views.AdminPages.AdminEditWordSetPage)}"));
        }

        public ICommand AddNewWordSetCommand { get; set; }

        public async Task LoadData()
        {
            try
            {
                var wordSets = await _wordSetService.GetAllAsync();
                WordSets = new ObservableCollection<WordSet>(wordSets);
            }
            catch (Exception ex) 
            {
                await Shell.Current.DisplayAlert("ERROR", "Could not load Wordsets", "OK");
            }
        }

        private ObservableCollection<WordSet> _wordSets = new ObservableCollection<WordSet>();
        public ObservableCollection<WordSet> WordSets { get { return _wordSets; } set { _wordSets = value; OnPropertyChanged(nameof(WordSets)); } }

        private WordSet _selectedWordSet;
        public WordSet SelectedWordSet 
        { 
            get { return _selectedWordSet; } 
            set 
            {
                if (_selectedWordSet == value)
                    return;

                _selectedWordSet = value;

                OnPropertyChanged(nameof(SelectedWordSet));
                OnSelectedWordSet(value);
            } 
        }

        private async Task OnSelectedWordSet(WordSet wordSet)
        {
            if (wordSet == null)
                return;

            //Navigate
            await Shell.Current.GoToAsync($"{nameof(Views.AdminPages.AdminEditWordSetPage)}?WordSetId={SelectedWordSet.Id}");

            SelectedWordSet = null; //Deselect
           
        }
    }
}
