using AndroidX.Work;

namespace BackgroundSync;

public class BackgroundSyncAndroid : IBackgroundSync
{
    public void SyncData()
    {
        if (NetworkType.Connected is null) return;
        
        using var builder = new Constraints.Builder();
        builder.SetRequiredNetworkType(NetworkType.Connected);
        var workConstraints = builder.Build();
        
        SetupWorker<DataSyncWorker>(TimeSpan.FromMinutes(16), workConstraints);
    }
    
    private void SetupWorker<TWorker>(TimeSpan interval, Constraints workConstraints) where TWorker : Worker
    {
        var request = PeriodicWorkRequest.Builder.From<TWorker>(interval)
            .SetConstraints(workConstraints)
            .Build();
        
        if (ExistingPeriodicWorkPolicy.Keep is null) return;
        
        WorkManager.GetInstance(Platform.AppContext)
            .EnqueueUniquePeriodicWork(nameof(TWorker), ExistingPeriodicWorkPolicy.Keep, request);        
    }
}