using System;
using Foundation;
using UIKit;
using AVFoundation;

namespace Hangskypetime.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AVAudioSession.SharedInstance().SetCategory(AVAudioSessionCategory.PlayAndRecord,
                AVAudioSessionCategoryOptions.AllowBluetooth |
                AVAudioSessionCategoryOptions.DefaultToSpeaker);

            // Hide the status bar to give us more screen real estate.
            // Also disable the idle timer since there isn't much touch
            // screen interaction during a video chat.
            UIApplication.SharedApplication.StatusBarHidden = true;
            UIApplication.SharedApplication.IdleTimerDisabled = true;

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
