using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Hangskypetime.Droid
{
    [Activity(Label = "Hangskypetime", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Load native libraries.
//TODO: Load libraries if not using the community edition
//            if (Build.CpuAbi.ToLower().Contains("v7a"))
//            {
//                Java.Lang.JavaSystem.LoadLibrary("audioprocessing");
//                Java.Lang.JavaSystem.LoadLibrary("audioprocessingJNI");
//            }
//            Java.Lang.JavaSystem.LoadLibrary("opus");
//            Java.Lang.JavaSystem.LoadLibrary("opusJNI");
//            Java.Lang.JavaSystem.LoadLibrary("vpx");
//            Java.Lang.JavaSystem.LoadLibrary("vpxJNI");
//
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
