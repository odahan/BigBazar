#nullable disable
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace BigBazar.Services;

public class DialogService(IAppLogger appLogger) : IDialogService
{
    // constructor with injection of logger

    private readonly IAppLogger logger = appLogger;

    private Page GetCurrentPage()
    {
        var p = Application.Current != null ? Application.Current.MainPage : null;
        if (p == null)
        {
            _ = logger.LogErrorAsync(AppLogger.LogLevel.Error, "No current MainPage found, DialogService will not work");
        }
        return p;
    }

    public async Task ShowMessage(string title, string message, string buttonOk)
    {
        var p = GetCurrentPage();
        if (p != null)
        {
            await p.DisplayAlert(title, message, buttonOk);
            return;
        }
    }

    public async Task ShowToast(string message, ToastDuration duration)
    {
        await Toast.Make(message, duration).Show(CancellationToken.None);
    }

    public async Task<string> ShowMessageWithResponse(string title, string message, string buttonOk, string buttonCancel)
    {
        var p = GetCurrentPage(); 
        if (p != null)
        {
            return await p.DisplayPromptAsync(title, message, buttonOk, buttonCancel);
        }
        return string.Empty;
    }

    public async Task<bool> ShowMessageWithBooleanResponse(string title, string message, string buttonOk, string buttonCancel)
    {
        var p = GetCurrentPage(); 
        if (p != null)
        {
            return await p.DisplayAlert(title, message, buttonOk, buttonCancel);
        }
        return false;
    }

    public async Task<string> InputTextDialogAsync(string title, string message, string placeholder, string initialvalue="")
    {
        var p = GetCurrentPage();
        Guard.IsNotNull(p);
        return await p.DisplayPromptAsync(title,message,"OK","Cancel",
            placeholder,-1,Keyboard.Text,initialvalue);
    }
}