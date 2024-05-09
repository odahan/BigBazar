#nullable disable
using BigBazar.Services;
using BigBazar.ViewModels;

namespace BigBazar.Views;

public partial class GalleryPage : BasePage
{
    private IDeviceOrientationService deviceOrientationService;
    private bool disposedValue;

    public GalleryPage(IViewModelFactory viewModelFactory, IDeviceOrientationService orientationService)
    {
        InitializeComponent();
        deviceOrientationService = orientationService;
        BindingContext = viewModelFactory.Create<GalleryPageViewModel>();
    }
    

    protected override void OnAppearing()
    {
        base.OnAppearing();
        deviceOrientationService.SetOrientationLandscape();
       if (BindingContext is GalleryPageViewModel vm)
        {
    //        vm.Loaded = false;
            vm.PageAppearingCommand.Execute(null);
        }
    
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (!((GalleryPageViewModel)BindingContext).IsGoingToPhotoDetail) deviceOrientationService.RestSetOrientation();
    }
    
}