#nullable disable
using BigBazar.Views;
using System.Reflection;

namespace BigBazar
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("BoxDetail", typeof(BoxPage));
            Routing.RegisterRoute("CatSelection", typeof(CatSelectionPage));
            Routing.RegisterRoute("FullPhoto", typeof(FullScreenPhotoPage));
            Routing.RegisterRoute("detail/Gallery", typeof(GalleryPage));

            /* if (DeviceInfo.Platform == DevicePlatform.WinUI)
             {
                 // Verrouillez le flyout pour qu'il reste toujours ouvert sur Windows
                 this.FlyoutBehavior = FlyoutBehavior.Locked;
             }*/
        }

        // Clear the cache when navigating to a new page
        ShellContent previousShellContent;

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            if (CurrentItem?.CurrentItem?.CurrentItem is not null &&
                previousShellContent is not null)
            {
                var property = typeof(ShellContent)
                    .GetProperty("ContentCache",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                property?.SetValue(previousShellContent, null);

            }

            previousShellContent = CurrentItem?.CurrentItem?.CurrentItem;
        }

    }
}
