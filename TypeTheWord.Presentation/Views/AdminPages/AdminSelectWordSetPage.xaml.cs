namespace TypeTheWord.Presentation.Views.AdminPages;

public partial class AdminSelectWordSetPage : ContentPage
{
    private ViewModels.AdminPages.AdminSelectWordSetPageViewModel _vm;

    public AdminSelectWordSetPage(ViewModels.AdminPages.AdminSelectWordSetPageViewModel vm)
	{
		InitializeComponent();

        _vm = vm;
        BindingContext = _vm;

    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.LoadData();
    }
}