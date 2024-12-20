using Android.Content;

namespace BackgroundSync;

[BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
public class AlarmHandler : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        if (intent?.Extras != null)
        {
            string title = intent.GetStringExtra(NotificationManagerAndroidService.TitleKey) ?? string.Empty;
            string message = intent.GetStringExtra(NotificationManagerAndroidService.MessageKey) ?? string.Empty;

            NotificationManagerAndroidService managerAndroid = NotificationManagerAndroidService.Instance ?? new NotificationManagerAndroidService();
            managerAndroid.Show(title, message);
        }
    }
}