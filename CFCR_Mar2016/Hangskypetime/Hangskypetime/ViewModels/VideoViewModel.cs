using System;
using Xamarin.Forms;

namespace Hangskypetime
{
    public class VideoViewModel : NotifyingObject
    {
        App App { get; }
        IPopupService Popups { get; }
        INavigation Navigation { get; }
        IVideoView VideoView { get; }
        bool LocalMediaStarted { get; set; }
        bool ConferenceStarted { get; set; }

        public VideoViewModel(App app, Page page, string sessionId, IVideoView videoView)
        {
            App = app;
            Popups = new PopupService(page);
            page.Appearing += HandlePageAppearing;
            page.Disappearing += HandlePageDisappearing;
            Navigation = page.Navigation;
            VideoView = videoView;
            SessionId = sessionId;
            NextCameraCommand = new Command(NextCamera);
        }

        public string SessionId { get; private set; }
        public Command NextCameraCommand { get; private set; }

        private void HandlePageAppearing(object sender, EventArgs e)
        {
            if (LocalMediaStarted)
                return;

            StartLocalMedia();

            Popups.MakeToast("Double tap to switch camera.");
        }

        private void HandlePageDisappearing(object sender, EventArgs e)
        {
            if (ConferenceStarted)
            {
                StopConference();
                ConferenceStarted = false;
            }

            if (LocalMediaStarted)
            {
                StopLocalMedia();
                LocalMediaStarted = false;
            }
        }

        private void NextCamera(object o)
        {
            App.UseNextVideoDevice();
        }

        private void StartLocalMedia()
        {
            App.StartLocalMedia(VideoView.NativeContainer, (error) =>
            {
                if (error != null)
                {
                    Popups.MakeToast(error);
                }
                else
                {
                    // Start conference now that the local media is available.
                    StartConference();
                }
            });
            LocalMediaStarted = true;
        }

        private void StopLocalMedia()
        {
            App.StopLocalMedia((error) =>
            {
                if (error != null)
                {
                    Popups.MakeToast(error);
                }
            });
        }

        private void StartConference()
        {
            App.StartConference((error) =>
            {
                if (error != null)
                {
                    Popups.MakeToast(error);
                }
            }, SessionId);
            ConferenceStarted = true;
        }

        private void StopConference()
        {
            App.StopConference((error) =>
            {
                if (error != null)
                {
                    Popups.MakeToast(error);
                }
            });
        }
    }
}
