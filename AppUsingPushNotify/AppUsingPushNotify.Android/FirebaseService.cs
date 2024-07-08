using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;
using System.Linq;

namespace AppUsingPushNotify.Android;

[Service(Name = "AppUsingPushNotify.Android.FirebaseService", Exported = true)]
[IntentFilter(["com.google.firebase.MESSAGING_EVENT"])]
public class FirebaseService : FirebaseMessagingService
{
    const string TAG = "FirebaseService";

    public override void OnMessageReceived(RemoteMessage message)
    {
        base.OnMessageReceived(message);

        Log.Debug(TAG, "From: " + message.From);
        Log.Debug(TAG, "Data Values: " + message.Data.Values);

        var notification = message.GetNotification();
        if (notification != null)
        {
            Log.Debug(TAG, "Notification Title: " + (notification.Title ?? "No title"));
            Log.Debug(TAG, "Notification Body: " + (notification.Body ?? "No body"));
            Log.Debug(TAG, "Data Values: " + string.Join(", ", message.Data?.Select(kvp => $"{kvp.Key}: {kvp.Value}") ?? []));

            /*if (notification != null) MostrarNotificacao(notification);*/
        }
    }

    /*public static void MostrarNotificacao(RemoteMessage.Notification notification)
    {
        if (notification == null) return;
        string title = notification.Title ?? "Message Title";

        // Criação do construtor de notificação
        NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context, "default_channel_id")
            .SetContentTitle(title)
            .SetContentText(notification.Body)
            .SetSmallIcon(_Microsoft.Android.Resource.Designer.ResourceConstant.Drawable.ic_stat_ic_notification) // Ajuste o ícone conforme necessário
            .SetPriority(NotificationCompat.PriorityDefault);

        Notification notificationAndroid = builder.Build();

        // Gerenciamento da notificação
        var notificationService = Application.Context?.GetSystemService(Context.NotificationService);
        if (notificationService != null)
        {
            var notificationManager = (NotificationManager)notificationService;
            notificationManager?.Notify(0, notificationAndroid);
        }
    }*/

    public override void OnNewToken(string token)
    {
        base.OnNewToken(token);
        Log.Debug(TAG, $"OnNewToken - token: {token}");
    }
}
