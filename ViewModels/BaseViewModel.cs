#nullable disable
using BigBazar.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BigBazar.ViewModels;

public partial class BaseViewModel : ObservableRecipient, IDisposable

{
    [ObservableProperty]
    private IAppLogger logger;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string title;

    public static T GetServiceHelper<T>() => MauiProgram.GetServiceHelper<T>();

    public BaseViewModel()
    {
        Logger = GetServiceHelper<IAppLogger>();
        Title = this.GetType().Name;
    }

    partial void OnIsBusyChanged(bool value)
    {
        if (value)
        {
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                if (IsBusy)
                {
                    MainThread.BeginInvokeOnMainThread(() => IsBusyVisible = true);
                }
            });
        }
        else
        {
            IsBusyVisible = false;
        }
    }

    [ObservableProperty]
    private bool isBusyVisible;


    public void Dispose()
    {
        logger = null;
        isBusy = false;
        title = null;
        DisposeResources();
    }
    protected virtual void DisposeResources() { }

}