#nullable disable
using BigBazar.Services;
using BigBazar.ViewModels;

namespace BigBazar.Views;

public partial class FullScreenPhotoPage : BasePage
{
    private readonly FullScreenPhotoPageViewModel viewModel;
    public FullScreenPhotoPage(IViewModelFactory viewModelFactory)
    {
        InitializeComponent();
        viewModel = viewModelFactory.Create<FullScreenPhotoPageViewModel>();
        BindingContext = viewModel;
    }

    private readonly IDeviceOrientationService deviceOrientationService;
    private readonly bool resetOrientation;

    public FullScreenPhotoPage(string photoPath, IDeviceOrientationService orientationService, bool resetOrientation, IViewModelFactory viewModelFactory) : this(viewModelFactory)
    {
        this.resetOrientation = resetOrientation;
        deviceOrientationService = orientationService;
        viewModel.PhotoPath = photoPath;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        deviceOrientationService.SetOrientationLandscape();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (resetOrientation)
            deviceOrientationService.RestSetOrientation();
    }
}