namespace BackgroundSync;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        
        var backgroundSync = Application.Current?.Windows[0].Page?.Handler?.MauiContext?.Services.GetService<IBackgroundSync>();
        backgroundSync?.SyncData();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        
        var notification = Application.Current?.Windows[0].Page?.Handler?.MauiContext?.Services.GetService<INotificationManagerService>();
        notification?.SendNotification("Title", $"Hi {count}");

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}