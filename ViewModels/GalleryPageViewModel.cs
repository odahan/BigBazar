#nullable disable
using System.Collections.ObjectModel;
using BigBazar.Models;
using BigBazar.Services;
using BigBazar.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BigBazar.ViewModels;

public partial class GalleryPageViewModel : BaseViewModel, IQueryAttributable
{
    public GalleryPageViewModel(IDataService data, IDialogService dialogService)
    {
        Title = "Gallery";
        Photos = [];
        photosDirectory = data.PhotosDirectory;
        screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        dataService = data;
        dialog = dialogService;
    }

    private IDataService dataService;
    private IDialogService dialog;
    private bool firsttime = true;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!firsttime) return;
        if (loaded) return;
        firsttime = false;
        PhotoNumberFilter = -1;
        if (query.ContainsKey("PhotoNumber"))
        {
            PhotoNumberFilter = Convert.ToInt32(query["PhotoNumber"]);
        }
        await LoadPhotos();
    }

    [RelayCommand]
    private async Task PageAppearing()
    {
        await LoadPhotos();
    }


    [ObservableProperty]
    private int photoNumberFilter = -1;

    [ObservableProperty]
    private ObservableCollection<Photo> photos = [];

    private readonly string photosDirectory;

    [ObservableProperty]
    private double screenWidth;

    private bool loading;
    //public bool Loaded;
    private bool loaded;
    private async Task LoadPhotos()
    {
        if (loading) return;
        if (loaded) return;
        loading = true;
        loaded = false;
        IsBusy = true;
        await Task.Run(() =>
        {
            try
            {
                Photos.Clear();
                var imageFiles = Directory.GetFiles(photosDirectory, "*.jpg", SearchOption.AllDirectories).OrderBy(f => f).ToList();

                foreach (var file in imageFiles)
                {

                    var (nx, ny) = Utils.ImagePathParser.ExtractNumbersAsInt(file);
                    if (PhotoNumberFilter > -1)
                    {
                        if (nx == PhotoNumberFilter)
                        {
                            Photos.Add(new Photo(file));
                        }
                    }
                    else Photos.Add(new Photo(file));
                }

            }
            catch (Exception ex)
            {
                Logger.LogErrorAsync(Services.AppLogger.LogLevel.Error, "Error loading photo names \n" + ex.Message);
                Photos = [];
            }
            finally
            {
                IsBusy = false;
                loading = false;
                loaded = true;
            }
        });
    }




    [ObservableProperty]
    private bool isGoingToPhotoDetail;

    [RelayCommand]
    private async Task ItemTapped(Photo photo)
    {
        IsGoingToPhotoDetail = true;
        var factory = GetServiceHelper<IViewModelFactory>();
        var photoPage = new FullScreenPhotoPage(photo.ImagePath, new DeviceOrientationService(), false, factory);
        await Shell.Current.Navigation.PushAsync(photoPage);
        IsGoingToPhotoDetail = false;
    }

    [RelayCommand]
    private async Task DeleteImage(string imageName)
    {
        var response = await dialog.ShowMessageWithBooleanResponse("Delete Image", "Are you sure you want to delete this image?", "Yes", "No");
        if (!response) return;

        await Task.Run(async () =>
        {
            try
            {
                File.Delete(imageName);
                loaded = false;
                await LoadPhotos();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(Services.AppLogger.LogLevel.Error, "Error deleting photo \n" + ex.Message);
            }
        });

    }

    [RelayCommand]
    private async Task GotoBox(int boxNumber)
    {
        var box = await dataService.GetBoxByNumberAsync(boxNumber);
        await Shell.Current.GoToAsync($"BoxDetail?boxid={box.Id}&caller={GetType().Name}");
    }
}
