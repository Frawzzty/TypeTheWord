
using System.Threading.Tasks;

namespace TypeTheWord.Presentation.Views.PlayPages;

public partial class WordGamePlayPage : ContentPage, IQueryAttributable
{
    ViewModels.PlayPages.WordGamePlayPageViewModel _vm;
    public WordGamePlayPage(ViewModels.PlayPages.WordGamePlayPageViewModel vm)
    {
        InitializeComponent();

        _vm = vm;
        BindingContext = _vm;



    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("WordSetId", out var wordSetId))
        {
            _vm.WordSetId = wordSetId.ToString();
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        _vm.IsActivePage = true;

        
        await _vm.LoadData();

        await Task.Delay(100);
        EntryWordInput.Focus();
    }

    protected async override void OnDisappearing()
    {
        base.OnDisappearing();
        _vm.IsActivePage = false;
    }

    private async void OnWordInputChanged(object sender, TextChangedEventArgs e)
    {
        //Check if word should be submited
        _vm.TestWordCommand.Execute(e);

    }

}
