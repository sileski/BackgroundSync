using BackgroundTasks;
using CoreFoundation;
using Foundation;

namespace BackgroundSync;

public class BackgroundSynciOS : IBackgroundSync
{
    public void SyncData()
    {
        var request = new BGAppRefreshTaskRequest("com.yourapp.refreshTask");
        request.EarliestBeginDate = NSDate.FromTimeIntervalSinceNow(10 * 60); // e.g., schedule for 10 minutes from now

        NSError error = null;
        bool success = BGTaskScheduler.Shared.Submit(request, out error);

        if (success)
        {
            Console.WriteLine("Background sync task requested successfully.");
        }
        else
        {
            Console.WriteLine($"Error submitting background task request: {error?.LocalizedDescription}");
        }
    }
    
    public static void HandleBackgroundTask(BGTask task)
    {
        try
        {
            Console.WriteLine("Starting data sync...");
            
            Thread.Sleep(5000); 

            Console.WriteLine("Data sync completed.");

            // Mark the background task as completed
            task.SetTaskCompleted(success: true);
            
            var request = new BGAppRefreshTaskRequest("com.yourapp.refreshTask");
            request.EarliestBeginDate = NSDate.FromTimeIntervalSinceNow(20 * 60); // Reschedule for 20 minutes later

            NSError error = null;
            bool rescheduleSuccess = BGTaskScheduler.Shared.Submit(request, out error);

            if (rescheduleSuccess)
            {
                Console.WriteLine("Background task rescheduled successfully.");
            }
            else
            {
                Console.WriteLine($"Error rescheduling background task: {error?.LocalizedDescription}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while processing background task: {ex.Message}");
            task.SetTaskCompleted(success: false);
        }
    }
}