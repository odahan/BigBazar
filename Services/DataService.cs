#nullable disable
using System.Text;
using BigBazar.Models;
using SQLite;
using SkiaSharp;
using CommunityToolkit.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Reflection;

namespace BigBazar.Services;


public class DataService : IDataService
{
    #region private fields
    private const string DatabaseName = "BigBazar.db3";
    private const string DatabaseNameBackup = "BigBazar.Bak.db3";
    private const string BoxPrefix = "box";
    private SQLiteAsyncConnection database;
    private readonly string dbPath;
    private readonly IAppLogger logger;
    private readonly string photosDirectory;
    // ReSharper disable once NotAccessedField.Local
    private readonly IPathService imagesPathService;
    private IDialogService dialogService;
    #endregion

    #region public fields, consts & properties
    public const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    public bool IsInitialized { get; private set; }

    public string PhotosDirectory => photosDirectory;

    #endregion


    #region Ctor
    public DataService(IAppLogger logger, IPathService imagesPathService, IDialogService dialog)
    {
        IsInitialized = false;
        this.imagesPathService = imagesPathService;
        this.logger = logger;
        dialogService = dialog;

        // Construct database path

        dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
        // ex: /data/user/0/com.companyname.bigbazar/files/BigBazar.db3
        // accessible avec Android Device Explorer dans Android Studio, répertoire data/data/com.companyname.bigbazar/files
        try
        {
            database = new SQLiteAsyncConnection(dbPath, Flags);
        }
        catch (Exception ex)
        {
            logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error SQLite initializing database: " + ex.Message);
            IsInitialized = false;
            return;
        }

        try
        {
            database.CreateTableAsync<Box>();
            database.CreateTableAsync<Category>();
            database.CreateTableAsync<BoxCat>();
            database.CreateTableAsync<Area>();
        }
        catch (Exception ex)
        {
            logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error creating tables: " + ex.Message);
            IsInitialized = false;
            return;
        }


        photosDirectory = Path.Combine(FileSystem.AppDataDirectory, "Photos");
        //logger.LogErrorAsync(AppLogger.LogLevel.Information, "Database Service initialized"+new string('-',30));
        IsInitialized = true;
    }

    public async Task CreateTablesAsync()
    {
        IsInitialized = false;
        try
        {
            await database.CreateTableAsync<Box>();
            await database.CreateTableAsync<Category>();
            await database.CreateTableAsync<BoxCat>();
            await database.CreateTableAsync<Area>();
            IsInitialized = true;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error creating tables: " + ex.Message);
            IsInitialized = false;
        }

    }
    #endregion

    #region Box operations
    // Save or update a Box
    public async Task<int> SaveBoxAsync(Box box)
    {
        Guard.IsNotNull(box);
        int id;
        try
        {
            if (box.Id > 0)
            {
                await database.UpdateAsync(box);
                id = box.Id;
            }
            else
            {
                id = await database.InsertAsync(box);
            }
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error saving box: " + ex.Message);
            return -1;
        }
        return id;
    }


    public async Task<bool> DeleteBoxByBoxNumber(int boxNumber)
    {
        Guard.IsGreaterThan(boxNumber, 0);
        try
        {
            var box = await GetBoxByNumberAsync(boxNumber);
            if (box != null)
            {
                await DeleteAllPhotosForBoxNumberAsync(box.Number);
                await database.Table<BoxCat>().DeleteAsync(bc => bc.BoxId == box.Id);
                await database.DeleteAsync<Box>(box.Id);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error deleting box by number: " + ex.Message);
            return false;
        }
    }

    // Retrieve all Boxes
    public async Task<List<Box>> GetBoxesAsync()
    {
        try
        {
            return await database.Table<Box>().OrderBy((b) => b.Number).ToListAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving boxes: " + ex.Message);
            return [];
        }
    }

    // Retrieve all Boxes matching a search text
    public async Task<List<Box>> SearchBoxesDescAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return await GetBoxesAsync();
        try
        {
            var boxes = await database.Table<Box>().ToListAsync();
            return boxes.Where(box =>
                         box.Description.Contains(text, StringComparison.OrdinalIgnoreCase))
                        .OrderBy((b) => b.Number).ToList();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error searching boxes on text [{text}]: " + ex.Message);
            return [];
        }

    }

    // retrieve all boxes having a category matching the search text
    public async Task<List<Box>> SearchBoxesCatAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return await GetBoxesAsync();
        try
        {
            var allBoxes = await database.Table<Box>().ToListAsync();
            var allBoxCats = await database.Table<BoxCat>().ToListAsync();
            var allCats = await database.Table<Category>().ToListAsync();

            var boxes = (from b in allBoxes
                         join bc in allBoxCats on b.Id equals bc.BoxId
                         join c in allCats on bc.CatId equals c.Id
                         where c.Name.Contains(text, StringComparison.OrdinalIgnoreCase)
                         select b).Distinct().OrderBy(b => b.Number).ToList();
            return boxes;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error searching boxes on category text [{text}]: " + ex.Message);
            return [];
        }

    }

    // Retrieve all boxes having an area matching the search text
    public async Task<List<Box>> SearchBoxesAreaAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return await GetBoxesAsync();
        try
        {
            var area = await database.Table<Area>()
            .Where(a => a.Name == text).FirstOrDefaultAsync();
            if (area == null) return await GetBoxesAsync();

            var boxes = await database.Table<Box>().Where(b => b.AreaCode == area.Id)
                        .OrderBy(b => b.Number).ToListAsync();
            return boxes;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error searching boxes on Area [{text}]: " + ex.Message);
            return [];
        }

    }



    // Retrieve a Box by ID
    public async Task<Box> GetBoxByIdAsync(int id)
    {
        Guard.IsGreaterThan(id, 0);
        try
        {
            return await database.Table<Box>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving box by ID: " + ex.Message);
            return null;
        }
    }

    // Retrieve a Box by number
    public async Task<Box> GetBoxByNumberAsync(int id)
    {
        Guard.IsGreaterThan(id, 0);
        try
        {
            return await database.Table<Box>().Where(x => x.Number == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving box by number: " + ex.Message);
            return null;
        }
    }

    public async Task<int> GetLastBoxNumberAsync()
    {
        try
        {
            var lastBox = await database.Table<Box>().OrderByDescending(b => b.Number).FirstOrDefaultAsync();
            return lastBox?.Number ?? 0;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving last box number: " + ex.Message);
            return -1;
        }
    }

    public async Task<int> GetNewBoxNumber()
    {
        try
        {
            var firstMissing = await GetFirstMissingBoxNumbersAsync();
            if (firstMissing > 0)
            {
                return firstMissing;
            }

            var lastBox = await database.QueryAsync<int>("SELECT Number FROM Boxes ORDER BY Number DESC");
            var lastBoxNumber = lastBox.FirstOrDefault();
            return lastBoxNumber + 1;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving first available box number: " + ex.Message);
            return -1;
        }
    }

    public async Task<int> GetFirstMissingBoxNumbersAsync()
    {
        try
        {
            var allLines = await database.Table<Box>().ToListAsync();
            //var allNumbers = await database.QueryAsync<int>("SELECT Number FROM Boxes ORDER BY Number ASC");
            var allNumbers = allLines.Select(b => b.Number).OrderBy(n => n).ToList();
            allLines = null;

            int? firstMissingNumber;

            for (var i = 0; i < allNumbers.Count - 1; i++)
            {
                if (allNumbers[i] + 1 != allNumbers[i + 1])
                {
                    firstMissingNumber = allNumbers[i] + 1;
                    return firstMissingNumber.Value;
                }
            }
            return -1;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error searching first missing Box numbers: " + ex.Message);
            return -1;
        }
    }

    public async Task<List<int>> GetAllActiveBoxNumbers()
    {
        return await database.QueryAsync<int>("SELECT Number FROM Boxes ORDER BY Number ASC");
    }

    #endregion

    #region Category operations
    // Save or update a Category
    public async Task<int> SaveCategoryAsync(Category category)
    {
        Guard.IsNotNull(category);
        if (category.Id > 0)
        {
            try
            {
                await database.UpdateAsync(category);
                return category.Id;
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error updating category: " + ex.Message);
                return -1;
            }
        }
        else
        {
            try
            {
                return await database.InsertAsync(category);
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error inserting category: " + ex.Message);
                return -1;
            }
        }
        //return -1;
    }

    public async Task<int> HowMuchTimesCategoryIsUsed(int categoryId)
    {
        Guard.IsGreaterThan(categoryId, 0);
        try
        {
            return await database.Table<BoxCat>().CountAsync(bc => bc.CatId == categoryId);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error counting category usage: " + ex.Message);
            return -1;
        }
    }

    // Delete a Category and related BoxCats
    public async Task<int> DeleteCategoryAsync(int categoryId)
    {
        Guard.IsGreaterThan(categoryId, 0);
        try
        {
            await database.Table<BoxCat>().DeleteAsync(bc => bc.CatId == categoryId);
            return await database.DeleteAsync<Category>(categoryId);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error deleting category: " + ex.Message);
            return -1;
        }
    }

    // Retrieve all Categories
    public async Task<List<Category>> GetCategoriesAsync()
    {
        try
        {
            return await database.Table<Category>().OrderBy((c) => c.Name).ToListAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving categories: " + ex.Message);
            return [];
        }
    }

    // Retrieve a Category by ID
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        Guard.IsGreaterThan(id, 0);
        try
        {
            return await database.Table<Category>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving category by ID: " + ex.Message);
            return null;
        }
    }
    #endregion

    #region BoxCat operations

    // save or update a BoxCat relationship 
    public async Task<int> SaveBoxCatAsync(BoxCat boxCat)
    {
        Guard.IsNotNull(boxCat);
        if (boxCat.Id > 0)
        {
            try
            {
                await database.UpdateAsync(boxCat);
                return boxCat.Id;
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error updating BoxCat: " + ex.Message);
                return -1;
            }
        }
        else
        {
            try
            {
                return await database.InsertAsync(boxCat);

            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error inserting BoxCat: " + ex.Message);
                return -1;
            }
        }
        //return -1;
    }

    // Add a new BoxCat relationship
    public async Task<int> AddBoxCatAsync(BoxCat boxCat)
    {
        Guard.IsNotNull(boxCat);
        try
        {
            await database.InsertAsync(boxCat);
            return boxCat.Id;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error inserting BoxCat: " + ex.Message);
            return -1;
        }
    }

    // Delete a BoxCat relationship
    public async Task<int> DeleteBoxCatAsync(int boxCatId)
    {
        Guard.IsGreaterThan(boxCatId, 0);
        try
        {
            return await database.DeleteAsync<BoxCat>(boxCatId);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error deleting BoxCat: " + ex.Message);
            return -1;
        }
    }

    // Retrieve all BoxCat relationships
    public async Task<List<BoxCat>> GetBoxCatsAsync()
    {
        try
        {
            return await database.Table<BoxCat>().ToListAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving BoxCat relationships: " + ex.Message);
            return [];
        }
    }

    public async Task<List<BoxCatForDisplay>> GetCatsForBoxId(int boxId)
    {
        Guard.IsGreaterThan(boxId, 0);
        try
        {
            var boxCats = await database.Table<BoxCat>().Where(bc => bc.BoxId == boxId).ToListAsync();
            var cats = new List<BoxCatForDisplay>();
            foreach (var boxCat in boxCats)
            {
                var cat = await GetCategoryByIdAsync(boxCat.CatId);
                if (cat != null)
                {
                    cats.Add(new BoxCatForDisplay(boxCat.Id, boxCat.CatId, boxCat.BoxId, cat.Name));
                }
            }
            return cats.OrderBy((c) => c.CatName).ToList();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error retrieving categories for box Id {boxId}: " + ex.Message);
            return [];
        }
    }
    #endregion

    #region Crud operations for Area entity
    public async Task<int> SaveAreaAsync(Area area)
    {
        Guard.IsNotNull(area);
        if (area.Id > 0)
        {
            try
            {
                await database.UpdateAsync(area);
                return area.Id;
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error updating Areas: " + ex.Message);
                return -1;
            }
        }
        else
        {
            try
            {
                return await database.InsertAsync(area);

            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error inserting Area: " + ex.Message);
                return -1;
            }
        }
        //return -1;
    }

    public async Task<int> AddAreaAsync(Area area)
    {
        Guard.IsNotNull(area);
        try
        {
            await database.InsertAsync(area);
            return area.Id;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error inserting Area: " + ex.Message);
            return -1;
        }
    }

    public async Task<int> DeleteAreaAsync(int areaId)
    {
        Guard.IsGreaterThan(areaId, 0);
        try
        {
            return await database.DeleteAsync<Area>(areaId);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error deleting Area: " + ex.Message);
            return -1;
        }
    }

    public async Task<List<Area>> GetAreasAsync()
    {
        try
        {
            return await database.Table<Area>().OrderBy(o => o.Name).ToListAsync();
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error retrieving Areas: " + ex.Message);
            return [];
        }
    }

    public async Task<Area> GetAreaForBoxId(int boxId)
    {
        Guard.IsGreaterThan(boxId, 0);
        try
        {
            var box = await GetBoxByIdAsync(boxId);
            if (box != null)
            {
                return await database.Table<Area>().FirstOrDefaultAsync(a => a.Id == box.AreaCode);
            }
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error retrieving Area for Box Id {boxId}: " + ex.Message);
            return null;
        }
        return null;
    }

    public async Task<Area> GetAreaById(int areaCode)
    {
        Guard.IsGreaterThan(areaCode, 0);
        try
        {
            return await database.Table<Area>().FirstOrDefaultAsync(a => a.Id == areaCode);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error retrieving Area for code {areaCode}: " + ex.Message);
            return null;
        }
    }

    public async Task<int> GetCountUsageForAreaId(int areaCode)
    {
        try
        {
            return await database.Table<Box>().CountAsync(b => b.AreaCode == areaCode);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error counting usage for Area code {areaCode}: " + ex.Message);
            return int.MaxValue;
        }
    }
    #endregion

    #region Backup database
    // Backup database and send it via share
    public async Task<bool> BackupDatabaseAsync()
    {
        if (database != null)
        {
            await database.CloseAsync();
        }

        return await Task.Run(async () =>
        {

            try
            {
                // Make sure that all database operations are completed before continuing.
                var dbCopy = Path.Combine(FileSystem.AppDataDirectory, DatabaseNameBackup);
                // Copy the database file to the backup file

                try
                {
                    File.Copy(dbPath, dbCopy, true);
                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = "BigBazar Database",
                        File = new ShareFile(dbCopy)
                    });
                }
                catch (Exception ex)
                {
                    await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error creating backup file: " + ex.Message);
                    await dialogService.ShowMessage("Error creating backup file", ex.Message, "OK");
                    return false;
                }
                finally
                {
                    try
                    {
                        database = new SQLiteAsyncConnection(dbPath, Flags);
                    }
                    catch (Exception ex)
                    {
                        await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error SQLite creating database: " + ex.Message);
                        IsInitialized = false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error sending sql backup by share: " + ex.Message);
                return false;
            }
        });
    }

    public async Task<bool> RestoreDatabaseAsync()
    {
        if (database != null)
        {
            await database.CloseAsync();
        }

        return await Task.Run(async () =>
        {
            try
            {
                // Open a file picker to select the backup file
                var result = await FilePicker.PickAsync();

                if (result != null)
                {
                    var backupFilePath = result.FullPath;

                    // Check if the selected file is a .db3 file
                    if (Path.GetExtension(backupFilePath).ToLower() != ".db3")
                    {
                        await dialogService.ShowMessage("Invalid file type", "Please select a .db3 file", "OK");
                        return false;
                    }

                    // Copy the backup file to the database file
                    try
                    {
                        File.Copy(backupFilePath, dbPath, true);
                    }
                    catch (Exception ex)
                    {
                        await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error restoring backup file: " + ex.Message);
                        await dialogService.ShowMessage("Error restoring backup file", ex.Message, "OK");
                        return false;
                    }
                    finally
                    {
                        try
                        {
                            database = new SQLiteAsyncConnection(dbPath, Flags);
                        }
                        catch (Exception ex)
                        {
                            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error SQLite creating database: " + ex.Message);
                            IsInitialized = false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error restoring database: " + ex.Message);
                return false;
            }
        });
    }




    public async Task<string> ExportDataAsync()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Exported data from BigBazar on {DateTime.Now.ToString(new CultureInfo("en-US"))}");

        // Récupérer toutes les boîtes
        var boxes = await database.Table<Box>().ToListAsync();

        foreach (var box in boxes)
        {
            sb.Append($"Box Number: {box.Number}, Description: {box.Description.Replace("\n", " ").Replace("\r", " ")}");

            // Récupérer toutes les catégories liées à cette boîte
            var boxCats = await database.Table<BoxCat>().Where(bc => bc.BoxId == box.Id).ToListAsync();

            if (boxCats.Count > 0)
            {
                sb.Append(", Categories: ");
            }

            var first = true;

            foreach (var boxCat in boxCats)
            {
                var category = await database.Table<Category>().FirstOrDefaultAsync(c => c.Id == boxCat.CatId);
                if (category != null)
                {
                    sb.Append(first ? "" : ", ");
                    first = false;
                    sb.Append($"{category.Name}");
                }
            }

            if (box.AreaCode != null)
            {
                var area = await database.Table<Area>().FirstOrDefaultAsync(a => a.Id == box.AreaCode);
                if (area != null)
                {
                    sb.Append($", Area: {area.Name}");
                }
            }

            sb.AppendLine();
        }
        return sb.ToString();
    }




    #endregion

    #region Photo operations

    public async Task ExportAndSharePhotosAsync()
    {
        try
        {
            string photoPath = await GetPhotoPathAsync();
            string zipPath = Path.Combine(Path.GetTempPath(), "photos.zip");
            if (File.Exists(zipPath)) File.Delete(zipPath);

            ZipFile.CreateFromDirectory(photoPath, zipPath);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "BigBazar Photos",
                File = new ShareFile(zipPath)
            });
        }
        catch (Exception ex)
        {
            var nomMethode = MethodBase.GetCurrentMethod()?.Name;
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, nomMethode + " : " + ex.Message);
        }

    }

    public async Task RestorePhotosAsync()
    {
        try
        {
            var response = await dialogService.ShowMessageWithBooleanResponse("Restore Photos", "This will delete all current photos and restore the photos from a zip file. Do you want to continue?", "Yes", "No");
            if (!response) return;

            // Open a file picker to select the zip file
            var result = await FilePicker.PickAsync();

            if (result != null)
            {
                var zipFilePath = result.FullPath;

                // Check if the selected file is a .zip file
                if (Path.GetExtension(zipFilePath).ToLower() != ".zip")
                {
                    throw new Exception("Invalid file type. Please select a .zip file.");
                }

                // Delete all current photos
                var photoPath = await GetPhotoPathAsync();
                try { Directory.Delete(photoPath, true); } 
                catch (Exception ex)
                {
                    await logger.LogErrorAsync(AppLogger.LogLevel.Warning, "Error deleting photos directory. "+ex.Message);
                }
                Directory.CreateDirectory(photoPath);

                // Extract the zip file to the photos directory
                ZipFile.ExtractToDirectory(zipFilePath, photoPath);
            }
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error restoring photos: " + ex.Message);
            await dialogService.ShowMessage("Error restoring photos", ex.Message, "OK");
        }
    }


    private async Task<bool> CheckCameraPermission()
    {
        // Vérifie et demande la permission de la caméra.
        var cameraStatus = await CheckAndRequestPermissionAsync<Permissions.Camera>();
        if (!cameraStatus)
        {
            await LogErrorAsync("Camera permission denied.");
            return false;
        }

        // À partir d'Android 10 (API niveau 29), la permission de stockage externe n'est pas requise pour la plupart des cas d'utilisation.
        if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 10)
        {
            return true;
        }

        // Vérifie et demande la permission de lecture du stockage.
        var readStorageStatus = await CheckAndRequestPermissionAsync<Permissions.StorageRead>();
        if (!readStorageStatus)
        {
            await LogErrorAsync("Storage read permission denied.");
            return false;
        }

        // Vérifie et demande la permission d'écriture du stockage.
        var writeStorageStatus = await CheckAndRequestPermissionAsync<Permissions.StorageWrite>();
        if (!writeStorageStatus)
        {
            await LogErrorAsync("Storage write permission denied.");
            return false;
        }

        return true;
    }

    private async Task<bool> CheckAndRequestPermissionAsync<T>() where T : Permissions.BasePermission, new()
    {
        var status = await Permissions.CheckStatusAsync<T>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<T>();
        }
        return status == PermissionStatus.Granted;
    }

    private async Task LogErrorAsync(string message)
    {
        // Assurez-vous que `logger` est déclaré et accessible dans votre classe.
        await logger.LogErrorAsync(AppLogger.LogLevel.Error, message);
    }

    public async Task<string> AddPickPhotoForBoxNumberAsync(int boxNumber)
    {
        Guard.IsGreaterThan(boxNumber, 0);
        try
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select a photo"
            });
            if (result != null)
            {
                // Handle the selected photo here
                var originalPhotoPath = result.FullPath;
                // Do something with the photo path, such as saving it or displaying it in your app

                Directory.CreateDirectory(photosDirectory); // Make sure the directory exists
                var nextPhotoSequence = await NextPhotoSequenceForNumber(boxNumber);
                var bnext = nextPhotoSequence.ToString("D4");
                var bnum = boxNumber.ToString("D4");
                var photoFileName = $"{BoxPrefix}{bnum}_{bnext}.jpg";
                var photoPath = Path.Combine(photosDirectory, photoFileName);

                File.Copy(originalPhotoPath, photoPath);
                return photoPath;
            }
            else
            {
                // No photo was selected
                return null;
            }
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error picking photo: " + ex.Message);
            return null;
        }
    }


    // Add a photo for a Box
    public async Task<string> AddPhotoForBoxNumberAsync(int boxNumber)
    {
        Guard.IsGreaterThan(boxNumber, 0);
        // Check and request permission to use the camera
        var status = await CheckCameraPermission();
        if (!status) return null;
        //check if taking a photo is supported  
        if (!MediaPicker.IsCaptureSupported)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Taking a photo is not supported on this device.");
            return null;
        }

        // Take a photo
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable once HeuristicUnreachableCode
            if (photo == null) return null; // User canceled or capture failed

            // Determine the directory and file name for the photo
            Directory.CreateDirectory(photosDirectory); // Make sure the directory exists
            var nextPhotoSequence = await NextPhotoSequenceForNumber(boxNumber);
            var bnext = nextPhotoSequence.ToString("D4");
            var bnum = boxNumber.ToString("D4");
            var photoFileName = $"{BoxPrefix}{bnum}_{bnext}.jpg";
            var photoPath = Path.Combine(photosDirectory, photoFileName);

            // Save the photo to the file system
            await using (var stream = await photo.OpenReadAsync())
            {
                await using (var newFile = File.OpenWrite(photoPath))
                {
                    await stream.CopyToAsync(newFile);
                }
            }

            return photoFileName;
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error taking photo: " + ex.Message);
            return null;
        }
    }

    private async Task<int> NextPhotoSequenceForNumber(int boxNumber)
    {
        Guard.IsGreaterThan(boxNumber, 0);
        return await Task.Run(async () =>
        {
            try
            {
                var nb = boxNumber.ToString("D4");
                var existingPhotos = Directory.EnumerateFiles(photosDirectory, $"{BoxPrefix}{nb}_*.jpg");
                var maxNumber = existingPhotos.Select(file =>
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    if (int.TryParse(fileName.Split('_').Last(), out var number))
                    {
                        return number;
                    }
                    return 1;
                }).DefaultIfEmpty(0).Max();

                return maxNumber + 1;
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error getting next photo number: " + ex.Message);
                return -1;
            }
        });
    }

    public async Task<bool> DeleteAllPhotosForBoxNumberAsync(int boxNumber)
    {
        Guard.IsGreaterThan(boxNumber, 0);
        return await Task.Run(() =>
        {
            try
            {
                if (Directory.Exists(photosDirectory))
                {
                    var formattedBoxNumber = boxNumber.ToString("D4");
                    var boxPhotos = Directory.EnumerateFiles(photosDirectory, $"{BoxPrefix}{formattedBoxNumber}_*.jpg");
                    foreach (var photo in boxPhotos)
                    {
                        try
                        {
                            File.Delete(photo);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error deleting photo {photo}: {ex.Message}");
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error deleting photos for box {boxNumber}: {ex.Message}");
                return false;
            }
        });
    }

    public async Task<bool> DeletePhotoAsync(string photoName)
    {
        Guard.IsNotNullOrEmpty(photoName);
        var photoPath = Path.Combine(photosDirectory, photoName);

        return await Task.Run(async () =>
        {
            try
            {
                // Check if the specified path exists and corresponds to a file.
                if (File.Exists(photoPath))
                {
                    // Optional: Add additional checks here, e.g., check the file extension.
                    if (Path.GetExtension(photoPath).Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            File.Delete(photoPath);
                            Console.WriteLine($"Photo {photoName} successfully deleted.");
                            return true;
                        }
                        catch (Exception ex)
                        {
                            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error deleting photo {photoName}: {ex.Message}");
                        }
                    }
                    else
                    {
                        await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"The file {photoName} can't be found.");
                    }
                }
                else
                {
                    await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Photo {photoName} does not exist.");
                }
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"Error deleting photo: {ex.Message}");
            }
            return false;
        });
    }

    // Get statistics for photos (total size is in Ko)
    public async Task<(int photoCount, long totalSize)> GetPhotosStatsKbAsync()
    {
        return await Task.Run(() =>
        {
            var photoCount = 0;
            long totalSize = 0;

            try
            {
                if (!Directory.Exists(photosDirectory))
                {
                    logger.LogErrorAsync(AppLogger.LogLevel.Error, "Photos directory does not exist.");
                    return (photoCount, totalSize);
                }

                var photoFiles = Directory.GetFiles(photosDirectory, "*.jpg", SearchOption.AllDirectories);
                photoCount = photoFiles.Length;

                totalSize = photoFiles.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Length).Sum() / 1024;
            }
            catch (Exception ex)
            {
                logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error getting photos statistics: " + ex.Message);
                return (-1, -1);
            }

            return (photoCount, totalSize);
        });
    }

    public async Task<List<string>> GetAllImagePathsAsync(bool fullPath = true)
    {
        try
        {
            return await Task.Run(() =>
            {
                if (!Directory.Exists(photosDirectory))
                {
                    // If the directory does not exist, return an empty list or null depending on your application logic.
                    return [];
                }

                // Get all .jpg files in the specified directory.
                // Adjust the search pattern if necessary to include other types of images.
                var imageFiles = Directory.GetFiles(photosDirectory, "*.jpg", SearchOption.AllDirectories);

                return fullPath ? [.. imageFiles] : imageFiles.Select(file => Path.GetFileName(file)).ToList();
            });
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error getting all image paths: " + ex.Message);
            return [];
        }
    }

    public Task<string> GetPhotoPathAsync()
    {
        return Task.FromResult(photosDirectory);
    }
    #endregion

    #region For Testing purpose
    // For testing & maintenance purpose

    // Reset the database and delete all photos
    public async Task ResetDatabaseAndImagesAsync()
    {
        if (database != null)
        {
            await database.CloseAsync();
        }

        // Delete the database file
        try
        {
            File.Delete(dbPath);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, $"An error occurred while deleting the database: {ex.Message}");
            throw;
        }

        try
        {
            database = new SQLiteAsyncConnection(dbPath, Flags);
        }
        catch (Exception ex)
        {
            await logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error SQLite creating database: " + ex.Message);
            IsInitialized = false;
            return;
        }

        await CreateTablesAsync(); // Recreate tables

        // Delete all photos
        if (Directory.Exists(photosDirectory))
        {
            Directory.Delete(photosDirectory, true); // true suppresses all files in subdirectories
        }

        // recreate photos directory
        Directory.CreateDirectory(photosDirectory);
    }

    // Populate the database with test data and generate test images
    public async Task PopulateDatabaseAndImagesWithTestDataAsync()
    {
        var syllables = new List<string> { "lo", "rem", "ip", "sum", "do", "lor", "sit", "a", "met", "con", "sec", "te", "tur", "ad", "i", "pis", "cing", "e", "lit", "sed", "do", "eius", "mod", "tem", "por", "in", "ci", "di", "dunt", "ut", "la", "bo", "re", "et", "do", "lo", "re", "mag", "na", "a", "li", "qua" };
        const int MaxItems = 30;
        const int MaxAreas = 10;
        var random = new Random();
        // add test data to the database
        for (var i = 1; i <= MaxAreas; i++)
        {
            var randomWord = string.Concat(syllables[random.Next(syllables.Count)], syllables[random.Next(syllables.Count)], syllables[random.Next(syllables.Count)]) + " Area";
            await database.InsertAsync(new Area { Name = $"{randomWord} {i}" });
        }

        for (var i = 1; i <= MaxItems; i++)
        {
            var randomWord = string.Concat(syllables[random.Next(syllables.Count)], syllables[random.Next(syllables.Count)], syllables[random.Next(syllables.Count)]);
            int? areaCode = random.Next(MaxAreas);
            if (random.Next(100) < 50) areaCode = null;
            await database.InsertAsync(new Box { Number = i, Description = $"Box {randomWord} {i}", AreaCode = areaCode });
            randomWord = string.Concat(syllables[random.Next(syllables.Count)], syllables[random.Next(syllables.Count)], syllables[random.Next(syllables.Count)]);
            await database.InsertAsync(new Category { Name = $"{randomWord} {i}" });

        }

        // add BoxCat relationships
        for (var i = 1; i <= MaxItems * 4; i++)
        {
            var boxCat = new BoxCat { BoxId = i % MaxItems, CatId = random.Next(MaxItems) + 1 };
            await database.InsertAsync(boxCat);
        }

        // generate test images
        Directory.CreateDirectory(photosDirectory); // S'assurer que le répertoire existe

        for (var i = 1; i <= MaxItems * 2; i++)
        {
            var bn = i % MaxItems;
            if (i == 0) bn = 1;
            var photoPath = Path.Combine(photosDirectory, $"{BoxPrefix}{bn.ToString("D4")}_0001.jpg");
            await CreateTestImageSkiaSharpAsync(photoPath);
        }
    }

    private static SKColor RandomSkColor()
    {
        var rnd = new Random();
        return new SKColor((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
    }

    private static int RandomNumber(int min, int max)
    {
        var rnd = new Random();
        return rnd.Next(min, max);
    }

    private async Task CreateTestImageSkiaSharpAsync(string imagePath)
    {
        Guard.IsNotNullOrEmpty(imagePath);
        const int width = 1280; // HD Dimension
        const int height = 720; // HD Dimension

        await Task.Run(() =>
        {

            using (var bitmap = new SKBitmap(width, height))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(RandomSkColor());

                for (var i = 0; i < 10; i++) // Dessiner 10 lignes
                {
                    var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        StrokeWidth = RandomNumber(2, 11), // Randomise entre 2 et 10 inclus
                        Color = RandomSkColor()
                    };
                    var startPoint = new SKPoint(RandomNumber(0, width), RandomNumber(0, height));
                    var endPoint = new SKPoint(RandomNumber(0, width), RandomNumber(0, height));
                    canvas.DrawLine(startPoint, endPoint, paint);
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
                {
                    using (var stream = File.OpenWrite(imagePath))
                    {
                        data.SaveTo(stream);
                    }
                }
            }
        });
    }
    #endregion


}
