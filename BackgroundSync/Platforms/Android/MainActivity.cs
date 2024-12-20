using Android.App;
using Android.Content.PM;
using Android.OS;

namespace BackgroundSync;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        RequestNotificationsPermission();
    }

    private async Task RequestNotificationsPermission()
    {
        if (await Permissions.CheckStatusAsync<Permissions.PostNotifications>() != PermissionStatus.Granted)
            await Permissions.RequestAsync<Permissions.PostNotifications>();
    }
}