namespace BigBazar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Liens vers la documentation Microsoft et les bibliothèques utilisées
            // https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
            // https://www.nuget.org/packages/sqlite-net-pcl/
            // https://www.nuget.org/packages/SQLitePCLRaw.bundle_green/

            // Initialisation de la bibliothèque supplémentaire temporaire
            SQLitePCL.Batteries.Init();

            UserAppTheme = AppTheme.Dark;

            MainPage = new AppShell();
        }
     
    }
}
