
namespace TypeTheWord.Presentation.Views.AdminPages;

public partial class AdminEditWordSetPage : ContentPage, IQueryAttributable
{

    private ViewModels.AdminPages.AdminEditWordSetPageViewModel _vm;
	public AdminEditWordSetPage(ViewModels.AdminPages.AdminEditWordSetPageViewModel vm)
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _vm.LoadData();
    }
}