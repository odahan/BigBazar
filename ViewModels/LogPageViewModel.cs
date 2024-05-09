#nullable disable
using BigBazar.Services;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BigBazar.ViewModels
{
    public partial class LogPageViewModel : BaseViewModel
    {
        public LogPageViewModel(IDialogService dialog)
        {
            Guard.IsNotNull(dialog);
            Title = "Log Page";
            dialogService = dialog;
            GetLog();
        }

       
        private IDialogService dialogService;

        // Get the log content
        [ObservableProperty]
        private string logs;

        [RelayCommand]
        private void GetLog()
        {
            IsBusy = true;
            try
            {
                var l = Task.Run(async () => await Logger.GetCurrentLogContentAsync()).GetAwaiter().GetResult();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Logs = l;
                });
                
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Delete all logs
        [RelayCommand]
        private async Task DeleteLogs()
        {
            var res = await dialogService.ShowMessageWithBooleanResponse("Delete ALL Logs", "Are you sure you want to delete all logs?", "Yes", "No");
            if (res)
            {
                IsBusy = true;
                try
                {
                    await Logger.DeleteAllLogs();
                }
                finally
                {
                    IsBusy = false;
                    Logs = string.Empty;
                }
            }
        }

        // Send log by mail
        [RelayCommand]
        private async Task SendLogByMail()
        {
            if (!Email.Default.IsComposeSupported)
            {
                _ = Logger.LogErrorAsync(AppLogger.LogLevel.Error, "Email is not supported on this device");
                await dialogService.ShowMessage("Error", "Email is not supported on this device", "Ok");
                return;
            }

            try
            {
                // Make sure that all database operations are completed before continuing.

                var emailMessage = new EmailMessage
                {
                    BodyFormat = EmailBodyFormat.PlainText,
                    Subject = "Backup BigBazar Log File",
                    Body = "Backup BigBazar Log performed on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    + "\n" + Logs
                };
                emailMessage.To?.Add("odahan@gmail.com");

                // Attach the database file to the email.
                // Note: Make sure the file can be read and is not locked by any ongoing operation.
                //emailMessage.Attachments?.Add(new EmailAttachment(dbPath));

                // Use Email.ComposeAsync to prepare the email.
                await Email.Default.ComposeAsync(emailMessage);
                return;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(AppLogger.LogLevel.Error, "Error sending log backup: " + ex.Message);
                await dialogService.ShowMessage("Error", "Error sending log backup: " + ex.Message, "Ok");
                return;
            }

        }
    }
}
