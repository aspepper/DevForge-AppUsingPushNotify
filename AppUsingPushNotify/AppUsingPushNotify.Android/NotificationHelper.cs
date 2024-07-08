using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace AppUsingPushNotify.Android;

public class NotificationHelper
{
    private readonly Context _context;

    public NotificationHelper(Context context)
    {
        _context = context;
        CreateNotificationChannel();
    }

    public void ShowNotification(string message, string title)
    {
        string body = message;

        NotificationCompat.Builder builder = new NotificationCompat.Builder(_context, "default_channel_id")
            .SetContentTitle(title)
            .SetContentText(body)
            .SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)  // Adjust the icon as needed
            .SetPriority(NotificationCompat.PriorityDefault)
            .SetAutoCancel(true);

        Notification notification = builder.Build();

        var notificationService = _context?.GetSystemService(Context.NotificationService);
        if (notificationService != null)
        {
            var notificationManager = (NotificationManager)notificationService;
            notificationManager?.Notify(0, notification);
        }
    }

    private void CreateNotificationChannel()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelName = "Default Channel";
            var channelDescription = "Channel for default notifications";
            var channel = new NotificationChannel("default_channel_id", channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationService = _context?.GetSystemService(Context.NotificationService);
            if (notificationService != null)
            {
                var notificationManager = (NotificationManager)notificationService;
                notificationManager?.CreateNotificationChannel(channel);
            }
        }
    }
}
