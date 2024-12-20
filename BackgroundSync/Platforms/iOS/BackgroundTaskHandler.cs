using BackgroundTasks;
using Foundation;

namespace BackgroundSync;

public class BackgroundTaskHandler
{
    public void HandleBackgroundTask(BGTask task)
    {
        try
        {
            Console.WriteLine("Starting data sync...");

            // Simulate data syncing with a 5-second pause
            SimulateDataSync();

            // Mark the background task as completed
            task.SetTaskCompleted(success: true);

            // Optionally reschedule the background task to run again after a certain period
            RescheduleBackgroundTask();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while processing background task: {ex.Message}");
            task.SetTaskCompleted(success: false);
        }
    }
    
    private void RescheduleBackgroundTask()
    {
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
    
    private void SimulateDataSync()
    {
        Thread.Sleep(5000);

        Console.WriteLine("Data sync completed.");
    }
}