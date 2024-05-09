using BigBazar.Services;

namespace BigBazar.Views;

public class BasePage : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        this.Dispatcher.Dispatch(async () =>
        {
            var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");
            if (result) System.Environment.Exit(0);
        });
        /*Device.BeginInvokeOnMainThread(async () =>
        {
            var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");
            if (result) System.Environment.Exit(0);
        });*/
        return true;
    }
}
