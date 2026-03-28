using Domain.Entities.Models.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TypeTheWord.Presentation.ViewModels.PlayPages
{
    public class WordGameResultPageViewModel : INotifyPropertyChanged
    {
        public WordGameResultPageViewModel(WordGameResult result)
        {
            Result = result;
            GoMainPageCommand = new Command(async () => await Shell.Current.GoToAsync($"///{nameof(MainPage)}"));
        }

        public ICommand GoMainPageCommand { get; set; }

        private WordGameResult _result;
        public WordGameResult Result { get { return _result; } set { _result = value; OnPropertyChanged(nameof(Result)); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
