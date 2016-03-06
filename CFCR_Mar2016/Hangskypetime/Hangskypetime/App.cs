using System;
using Xamarin.Conference.WebRTC.Opus;
using Xamarin.Conference.WebRTC.VP8;

using FM;
using FM.IceLink;
using FM.IceLink.WebRTC;
using Xamarin.Forms;

namespace Hangskypetime
{
    public class App : Application
    {
        private string IceLinkServerAddress = "demo.icelink.fm:3478";
        private string WebSyncServerUrl = "http://v4.websync.fm/websync.ashx"; // WebSync On-Demand

        private Signalling Signalling;
        private LocalMedia LocalMedia;

        private AudioStream AudioStream;
        private VideoStream VideoStream;
        private Conference Conference;
        private Certificate Certificate;

        public App()
        {
            // Strongly typed singleton for later reference
            Instance = this;

            // Log to the console.
            Log.Provider = new ConsoleLogProvider(LogLevel.Info);

            // WebRTC has chosen VP8 as its mandatory video codec.
            // Since video encoding is best done using native code,
            // reference the video codec at the application-level.
            // This is required when using a WebRTC video stream.
            VideoStream.RegisterCodec("VP8", () =>
            {
                return new Vp8Codec();
            }, true);

            // For improved audio quality, we can use Opus. By
            // setting it as the preferred audio codec, it will
            // override the default PCMU/PCMA codecs.
            AudioStream.RegisterCodec("opus", 48000, 2, () =>
            {
                return new OpusCodec();
            }, true);

            // To save time, generate a DTLS certificate when the
            // app starts and reuse it for multiple conferences.
            Certificate = Certificate.GenerateCertificate();

            // The root page of the application
            MainPage = new NavigationPage(new SessionPage());
        }

        public static App Instance { get; private set; }

        public void StartSignalling(Action<string> callback)
        {
            Signalling = new Signalling(WebSyncServerUrl);
            Signalling.Start(callback);
        }

        public void StopSignalling(Action<string> callback)
        {
            Signalling.Stop(callback);
            Signalling = null;
        }

        public void StartLocalMedia(object videoView, Action<string> callback)
        {
            LocalMedia = new LocalMedia();
            LocalMedia.Start(videoView, callback);
        }

        public void StopLocalMedia(Action<string> callback)
        {
            LocalMedia.Stop(callback);
            LocalMedia = null;
        }

        public void StartConference(Action<string> callback, string sessionId)
        {
            // Create a WebRTC audio stream description (requires a
            // reference to the local audio feed).
            AudioStream = new AudioStream(LocalMedia.LocalMediaStream);

            // Create a WebRTC video stream description (requires a
            // reference to the local video feed). Whenever a P2P link
            // initializes using this description, position and display
            // the remote video control on-screen by passing it to the
            // layout manager created above. Whenever a P2P link goes
            // down, remove it.
            VideoStream = new VideoStream(LocalMedia.LocalMediaStream);
            VideoStream.OnLinkInit += AddRemoteVideoControl;
            VideoStream.OnLinkDown += RemoveRemoteVideoControl;

            // Create a conference using our stream descriptions.
            Conference = new FM.IceLink.Conference(IceLinkServerAddress, new Stream[] { AudioStream, VideoStream });

            // Use our pre-generated DTLS certificate.
            Conference.DtlsCertificate = Certificate;

            // Supply TURN relay credentials in case we are behind a
            // highly restrictive firewall. These credentials will be
            // verified by the TURN server.
            Conference.RelayUsername = "test";
            Conference.RelayPassword = "pa55w0rd!";

            // Add a few event handlers to the conference so we can see
            // when a new P2P link is created or changes state.
            Conference.OnLinkInit += LogLinkInit;
            Conference.OnLinkUp += LogLinkUp;
            Conference.OnLinkDown += LogLinkDown;

            Signalling.Attach(Conference, sessionId, callback);
        }

        private void AddRemoteVideoControl(StreamLinkInitArgs e)
        {
            try
            {
                var remoteVideoControl = e.Link.GetRemoteVideoControl();
                LocalMedia.LayoutManager.AddRemoteVideoControl(e.PeerId, remoteVideoControl);
            }
            catch (Exception ex)
            {
                Log.Error("Could not add remote video control.", ex);
            }
        }

        private void RemoveRemoteVideoControl(StreamLinkDownArgs e)
        {
            try
            {
                if (LocalMedia != null && LocalMedia.LayoutManager != null)
                {
                    LocalMedia.LayoutManager.RemoveRemoteVideoControl(e.PeerId);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Could not remove remote video control.", ex);
            }
        }

        private void LogLinkInit(LinkInitArgs e)
        {
            Log.Info("Link to peer initializing...");
        }

        private void LogLinkUp(LinkUpArgs e)
        {
            Log.Info("Link to peer is UP.");
        }

        private void LogLinkDown(LinkDownArgs e)
        {
            Log.Info(string.Format("Link to peer is DOWN. {0}", e.Exception.Message));
        }

        public void StopConference(Action<string> callback)
        {
            // Detach signalling from the conference.
            Signalling.Detach((error) =>
            {
                Conference.OnLinkInit -= LogLinkInit;
                Conference.OnLinkUp   -= LogLinkUp;
                Conference.OnLinkDown -= LogLinkDown;
                Conference = null;

                VideoStream.OnLinkInit -= AddRemoteVideoControl;
                VideoStream.OnLinkDown -= RemoveRemoteVideoControl;
                VideoStream = null;

                AudioStream = null;
                
                callback(error);
            });
        }

        public void UseNextVideoDevice()
        {
            LocalMedia.LocalMediaStream.UseNextVideoDevice();
        }
    }
}