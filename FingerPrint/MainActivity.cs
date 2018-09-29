using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using System.Threading;
using Plugin.Fingerprint.Abstractions;
using System;

namespace FingerPrint
{
    //First version
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private CancellationTokenSource _cancel;
        private bool _initialized;
        Button BtnAuthenticate;
        Switch currentSwitch;
        bool isToggled; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            currentSwitch = (Switch) FindViewById(Resource.Id.swAutoCancel);
            currentSwitch.CheckedChange += delegate (object sender, CompoundButton.CheckedChangeEventArgs e)
            {
                isToggled = e.IsChecked;
            };
            BtnAuthenticate = (Button) FindViewById(Resource.Id.authentication);

            BtnAuthenticate.Click += async(sender, e) => 
            {
                await AuthenticationAsync("Put your finger!");


            };
        }
        private async Task AuthenticationAsync(string reason, string cancel = null, string fallback = null, string tooFast = null)
        {

            _cancel = isToggled? new CancellationTokenSource(TimeSpan.FromSeconds(10)) : new CancellationTokenSource();
            var dialogConfig = new AuthenticationRequestConfiguration(reason)
            { // all optional
                CancelTitle = cancel,
                FallbackTitle = fallback,

            };

            // optional

            var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);

            await SetResultAsync(result);
        }
        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
            {

                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Xamarin Android FingerPrint Sample");
                alert.SetMessage("Success");
                alert.Show();


            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Xamarin Android FingerPrint Sample");
                alert.SetMessage("Failed");
                alert.Show();

            }
        }
    }
}

