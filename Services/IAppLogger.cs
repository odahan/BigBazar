#nullable disable

namespace BigBazar.Services
{
    public interface IAppLogger
    {
        Task LogErrorAsync(AppLogger.LogLevel level, string message);
        Task<string> GetCurrentLogContentAsync();
        Task<bool> DeleteAllLogs();

        AppLogger.LogLevel MinLogLevel { get; set; }
    }
}
