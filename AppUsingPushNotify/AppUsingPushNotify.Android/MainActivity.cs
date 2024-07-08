using Android;
using Android.App;
using Android.Content.PM;
using Android.Gms.Tasks;
using Android.OS;
using Android.Util;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;
using Firebase;
using Firebase.Installations;


namespace AppUsingPushNotify.Android;

[Activity(
    Label = "AppUsingPushNotify.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.PostNotifications) == Permission.Denied)
        {
            ActivityCompat.RequestPermissions(this, [ Manifest.Permission.PostNotifications ], 1);
        }

                // Initialize Firebase
        var options = new FirebaseOptions.Builder()
                            .SetApplicationId("1:532149143610:android:26dbc14a6cefcf9ffc8d03")
                            .SetApiKey("AIzaSyB2Ag1IY0r9Aqs6iwFE_kPsgRWFPhAM6fM")
                            .SetGcmSenderId("532149143610")
                            .SetProjectId("appavaloniatestepushnotify")
                            .SetStorageBucket("appavaloniatestepushnotify.appspot.com")
                            .Build();

        if (Firebase.FirebaseApp.GetApps(this).Count == 0)
        {
            Firebase.FirebaseApp.InitializeApp(this, options);
        }

        // Get the FCM token
        FirebaseInstallations.Instance.GetToken(true).AddOnSuccessListener(new OnTokenSuccessListener());
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        /*var notificationHelper = new NotificationHelper(this);
        notificationHelper.ShowNotification("This is a test notification", "Título");*/

        return base.CustomizeAppBuilder(builder)
                   .WithInterFont()
                   .UseReactiveUI();
    }

    public class OnTokenSuccessListener : Java.Lang.Object, IOnSuccessListener
    {
        const string TAG = "OnTokenSuccessListener.OnTokenSuccessListener";

        public void OnSuccess(Java.Lang.Object result)
        {
            FirebaseInstallations firebaseInstallations = FirebaseInstallations.Instance;
            var installId = firebaseInstallations.GetId().Result;
            Log.Debug(TAG, "Firebase Installation ID: " + installId.ToString());

            if (result is InstallationTokenResult tokenResult)
            {
                string token = tokenResult.Token;
                Log.Debug(TAG, "FCM Token: " + token);
                Log.Debug(TAG, "FirebaseApp.Instance.PersistenceKey: " + Firebase.FirebaseApp.Instance.PersistenceKey);
            }

        }
    }
}

