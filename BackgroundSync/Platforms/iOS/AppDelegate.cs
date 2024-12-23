using BackgroundTasks;
using Foundation;
using UIKit;

namespace BackgroundSync;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    private static bool isTaskRegistered = false;

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        RegisterBackgroundTasks();
        return base.FinishedLaunching(application, launchOptions);
    }

    private void RegisterBackgroundTasks()
    {
        if (isTaskRegistered)
        {
            Console.WriteLine("Background task already registered.");
            return;
        }

        try
        {
            BGTaskScheduler.Shared.Register("com.yourapp.refreshTask", null, task =>
            {
                Console.WriteLine("Background task handler triggered.");
                HandleBackgroundTask(task);
            });

            Console.WriteLine("Background task registered successfully.");
            isTaskRegistered = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering background task: {ex.Message}");
        }
    }

    private void HandleBackgroundTask(BGTask task)
    {
        Console.WriteLine("Background task is running.");

        // Add your actual task handling logic here
        BackgroundSynciOS.HandleBackgroundTask(task);

        // Mark the task as completed
        task.SetTaskCompleted(success: true);
    }
}