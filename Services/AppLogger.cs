#nullable disable

using System.Globalization;

namespace BigBazar.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Maui.Storage;

    public partial class AppLogger : IAppLogger
    {
        private readonly string logDirectoryPath;
        private int logFileCounter;
        private const int LogLimitKo = 20;
        private const string AppLogName = "BigBazar";
        private const int LogHistoryLimit = 6;
        private const string LogSuffix = ".Log.txt";


        public AppLogger()
        {
#if DEBUG
            MinLogLevel = LogLevel.Debug;
#else
            MinLogLevel = LogLevel.Warning;
#endif

            logDirectoryPath = Path.Combine(FileSystem.AppDataDirectory, "logs");

            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            initializeLogFileCounter();
        }

        public LogLevel MinLogLevel { get; set; } 

        private string getCurrentLogFileName() => $"{AppLogName}.{logFileCounter:D3}{LogSuffix}";

        public async Task LogErrorAsync(LogLevel level, string message)
        {

            if ((int)level < (int)MinLogLevel)
            {
                return;
            }

            try
            {
                var logFilePath = Path.Combine(logDirectoryPath, getCurrentLogFileName());
                var errorMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {level}: {message}\n";
                var exceptionDetails = errorMessage.ToString().Replace(Environment.NewLine, " ");
                await File.AppendAllTextAsync(logFilePath, errorMessage);

                await rotateLogFileIfNeeded(logFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log error: {ex.Message}");
            }
        }

        private async Task rotateLogFileIfNeeded(string currentLogFilePath)
        {
            await Task.Run(() =>
            {
                try
                {
                    var logFile = new FileInfo(currentLogFilePath);
                    if (logFile.Length <= LogLimitKo * 1024) return;
                    logFileCounter++;
                    cleanOldLogs();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to rotate log file: {ex.Message}");
                }
            });
        }

        private void cleanOldLogs()
        {
            var logFiles = Directory.GetFiles(logDirectoryPath, $"*.Log.txt")
                                    .Select(path => new FileInfo(path))
                                    .OrderByDescending(fi => fi.Name)
                                    .ToList();

            if (logFiles.Count <= LogHistoryLimit)
            {
                return;
            }

            var oldLogs = logFiles.Skip(LogHistoryLimit);
            foreach (var logFile in oldLogs)
            {
                try
                {
                    logFile.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete log file: {ex.Message}");
                }
            }
        }

        private void initializeLogFileCounter()
        {
            var logFiles = Directory.GetFiles(logDirectoryPath, $"*.Log.txt");
            if (logFiles.Length <= 0)
            {
                // Handle the case when no log files are found
                return;
            }

            try
            {
                logFileCounter = logFiles.Select(path => new FileInfo(path))
                                        .Select(fi => fi.Name)
                                        .Select(name => name.Split('.'))
                                        .Select(split => split[1])
                                        .Select(int.Parse)
                                        .Max();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to retrieve log file counter: {ex.Message}");
            }
        }

        public async Task<string> GetCurrentLogContentAsync()
        {
            try
            {
                var logFilePath = Path.Combine(logDirectoryPath, getCurrentLogFileName());

                if (!File.Exists(logFilePath))
                {
                    return "Log file does not exist.";
                }

                //var fname = Path.GetFileName(logFilePath);
                //return await File.ReadAllTextAsync(logFilePath) + Environment.NewLine + $"Log File n°{logFileCounter} : {fname} \n {Path.GetDirectoryName(logFilePath)}";

                var lines = await File.ReadAllLinesAsync(logFilePath);
                var sortedLogContent = string.Empty;
                try
                {
                    var sortedLines = (lines
                        .Select(line => new { Line = line, Date = DateTime.ParseExact(line.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) })
                        .OrderByDescending(x => x.Date)
                        .Select(x => x.Line)).ToList();

                    sortedLogContent = string.Join(Environment.NewLine, sortedLines);
                }
                catch
                { 
                    sortedLogContent = string.Join(Environment.NewLine, lines);
                }

                var fname = Path.GetFileName(logFilePath);
                var final = sortedLogContent + Environment.NewLine;
                final += $"Log File n°{logFileCounter} : {fname} \n {Path.GetDirectoryName(logFilePath)}";
                return final;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get current log content: {ex.Message}");
                return string.Empty;
            }
        }

        public Task<bool> DeleteAllLogs()
        {
            return Task.Run(() =>
            {
                try
                {
                    var logFiles = Directory.GetFiles(logDirectoryPath, $"*{LogSuffix}");
                    foreach (var logFile in logFiles)
                    {
                        File.Delete(logFile);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete logs: {ex.Message}");
                    return false;
                }
            });
        }
    }

}
