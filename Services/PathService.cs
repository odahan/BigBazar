#nullable disable
namespace BigBazar.Services;

public class PathService : IPathService
{
    public string GetPhotoGalleryPath()
    {
#if ANDROID 
        return "//storage//emulated//0//DCIM//Camera";
#elif WINDOWS
        return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);  
#else
        return string.Empty;
#endif
    }

}

