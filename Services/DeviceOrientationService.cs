namespace BigBazar.Services;

public class DeviceOrientationService : IDeviceOrientationService
{
    public void SetOrientationLandscape()
    {
#if ANDROID
    Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation =    Android.Content.PM.ScreenOrientation.Locked;  
    Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;  
#endif
    }
    public void RestSetOrientation()
    {
#if ANDROID
    Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.RequestedOrientation =    Android.Content.PM.ScreenOrientation.User;  
            
#endif
    }
}