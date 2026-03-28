using Domain.Entities.Models.Db;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TypeTheWord.App.Interfaces;

namespace TypeTheWord.Presentation.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private IWordSetService _wordSetService;
        public MainPageViewModel(IWordSetService wordSetService)
        {
            _wordSetService = wordSetService;
            GoAdminPageCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Views.AdminPages.AdminSelectWordSetPage)));
        }

        public ICommand GoAdminPageCommand { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string nameOfProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameOfProperty));
        }
        private async Task OnWordSetSelectedAsync(WordSet wordSet)
        {
            if (wordSet == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(Views.PlayPages.WordGamePlayPage)}?WordSetId={wordSet.Id}");

            SelectedWordSet = null !; //Deselect
        }

        //LOAD DATA
        public async Task LoadData()
        {
            try
            {
                var wordSets = await _wordSetService.GetAllAsync();
                WordSets = new ObservableCollection<WordSet>(wordSets);
            }
            catch (TimeoutException)
            {
                await Shell.Current.DisplayAlert("Connection Error", $"Connection timedout when requesting WordSets. Check your connection or try disabling your VPN if you are using it", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Unexpected Error", $"Error when requesting WordSets\n{ex.Message}", "OK");
            }
        }


        //PROPS
        private ObservableCollection<WordSet> _wordSets = new ObservableCollection<WordSet>();
        public ObservableCollection<WordSet> WordSets { get { return _wordSets; } set { _wordSets = value; OnPropertyChanged(nameof(WordSets)); } }

        private WordSet _selectedWordSet;
        //Bind to ItemSelected in CollectionView
        public WordSet SelectedWordSet { 
            get { return _selectedWordSet; } 
            set {

                if (SelectedWordSet == value)
                    return;

                _selectedWordSet = value; 
                OnPropertyChanged(nameof(SelectedWordSet));

                OnWordSetSelectedAsync(value);
            } 
        }
    }
}
