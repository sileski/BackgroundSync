using Android.Content;
using AndroidX.Work;

namespace BackgroundSync;

public class DataSyncWorker : Worker
{
    public const string TAG = "DataSyncWorker";

    public DataSyncWorker(Context context, WorkerParameters workerParams)
        : base(context, workerParams)
    {
    }

    public override Result DoWork()
    {
        try
        {
            Console.WriteLine("Data Sync started...");

            Thread.Sleep(5000); // Simulate a long-running task
            var notification = Application.Current?.Windows[0].Page?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();
            notification?.SendNotification("Title", "Test");
            Console.WriteLine("Data Sync completed!");
            

            return Result.InvokeSuccess(); 
        }
        catch (Exception ex)
        {
            return Result.InvokeFailure(); 
        }
    }
}