using Domain.Entities.Models.Game;

namespace TypeTheWord.Presentation.Views.PlayPages;

public partial class WordGameResultPage : ContentPage
{
	public WordGameResultPage(WordGameResult result)
	{
		InitializeComponent();
        
        BindingContext = new ViewModels.PlayPages.WordGameResultPageViewModel(result);
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();


    }
}