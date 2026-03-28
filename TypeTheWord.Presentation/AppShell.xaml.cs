namespace TypeTheWord.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Play pages
            Routing.RegisterRoute(nameof(Views.PlayPages.WordGamePlayPage), typeof(Views.PlayPages.WordGamePlayPage));

            //Admin pages
            Routing.RegisterRoute(nameof(Views.AdminPages.AdminSelectWordSetPage), typeof(Views.AdminPages.AdminSelectWordSetPage));
            Routing.RegisterRoute(nameof(Views.AdminPages.AdminEditWordSetPage), typeof(Views.AdminPages.AdminEditWordSetPage));
        }
    }
}
