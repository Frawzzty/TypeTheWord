using Domain.Entities.Models;
using System.Runtime.CompilerServices;
using TypeTheWord.Presentation.ViewModels;

namespace TypeTheWord.Presentation
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _vm;
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();

            _vm = vm;
            BindingContext = _vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await _vm.LoadData();

        }


    }
}
