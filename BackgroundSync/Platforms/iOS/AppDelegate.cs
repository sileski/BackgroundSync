using BackgroundTasks;
using CoreFoundation;
using Foundation;
using UIKit;

namespace BackgroundSync;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        BGTaskScheduler.Shared.Register("com.yourapp.refreshTask", null, HandleBackgroundTask);
        return base.FinishedLaunching(application, launchOptions);
    }
    
    private void HandleBackgroundTask(BGTask task)
    {
        Console.WriteLine("Background task is running.");
    
        // Handle the background task (this will be executed when the task is triggered)
        BackgroundSynciOS.HandleBackgroundTask(task);
    }
}