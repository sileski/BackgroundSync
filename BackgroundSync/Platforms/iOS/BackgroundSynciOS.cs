using BackgroundTasks;
using Foundation;

namespace BackgroundSync;

public class BackgroundSynciOS : IBackgroundSync
{
    public void SyncData()
    {
        BGTaskScheduler.Shared.Register("com.yourapp.refreshTask", null, task => 
        {
            if (task is BGAppRefreshTask refreshTask)
            {
                HandleBackgroundTask(refreshTask);
            }
        });
        
        var request = new BGAppRefreshTaskRequest("com.yourapp.refreshTask")
        {
            EarliestBeginDate = NSDate.FromTimeIntervalSinceNow(10 * 60) // 10 minutes from now
        };

        NSError error;
        if (BGTaskScheduler.Shared.Submit(request, out error))
        {
            Console.WriteLine("Background task request submitted successfully.");
        }
        else
        {
            Console.WriteLine($"Error submitting background task request: {error.LocalizedDescription}");
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