#nullable disable
using BigBazar.Services;
using BigBazar.ViewModels;
using BigBazar.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BigBazar
{
    /// <summary>
    /// The main program class for the Maui application.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates and configures the Maui application.
        /// </summary>
        /// <returns>The configured Maui application.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SpaceMono-Regular.ttf", "MonoRegular");
                });

            ConfigureServices(builder.Services);
            ConfigureViewsAndViewModels(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IPathService, PathService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
            services.AddSingleton<IVersionService, VersionService>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        }

        /// <summary>
        /// Configures the views and view models for the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public static void ConfigureViewsAndViewModels(IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            services.AddTransient<MainPageViewModel>();

            services.AddTransient<LogPage>();
            services.AddTransient<LogPageViewModel>();

            services.AddTransient<BoxListPage>();
            services.AddTransient<BoxListPageViewModel>();

            services.AddTransient<BoxPage>();
            services.AddTransient<BoxPageViewModel>();

            services.AddTransient<CatSelectionPage>();
            services.AddTransient<CatSelectionPageViewModel>();

            services.AddTransient<CategoryPage>();
            services.AddTransient<CategoryPageViewModel>();

            services.AddTransient<GalleryPage>();
            services.AddTransient<GalleryPageViewModel>();

            services.AddTransient<FullScreenPhotoPage>();
            services.AddTransient<FullScreenPhotoPageViewModel>();

            services.AddTransient<AddBoxPage>();    
            services.AddTransient<AddBoxPageViewModel>();

            services.AddTransient<AboutPage>();
            services.AddTransient<AboutPageViewModel>();

            services.AddTransient<AreaPage>();
            services.AddTransient<AreaPageViewModel>();
        }

        /// <summary>
        /// Gets a service from the application's service provider.
        /// </summary>
        /// <typeparam name="T">The type of service to get.</typeparam>
        /// <returns>The requested service, or null if it could not be found.</returns>
        public static T GetServiceHelper<T>()
        {
            return (T)Application.Current?.Handler?.MauiContext?.Services.GetService(typeof(T));
        }
    }
}
