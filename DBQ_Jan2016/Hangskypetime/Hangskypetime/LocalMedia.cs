using System;
using FM.IceLink.WebRTC;

#if __IOS__
using NativeView = UIKit.UIView;
#elif __ANDROID__
using NativeView = Android.Views.ViewGroup;
using LayoutManager = FM.IceLink.WebRTC.AndroidLayoutManager;
#endif

namespace Hangskypetime
{
    class LocalMedia
    {
        // We're going to need both audio and video
        // for this example. We can constrain the
        // video slightly for performance benefits.
        private bool Audio = true;
        private bool Video = true;
        private int  VideoWidth = 640;
        private int  VideoHeight = 480;
        private int  VideoFrameRate = 15;

        public LocalMediaStream LocalMediaStream { get; private set; }
        public LayoutManager LayoutManager { get; private set; }

        public void Start(object videoView, Action<string> callback)
        {
            /*
             * Demonstration of how to record local audio/video to disk while the media stream is active.
             * 
            OpusCodec opus = new OpusCodec();
            Vp8Codec vp8 = new Vp8Codec();

            opus.Initialize(CodecUsage.Encoder, "opus", 48000, 2);
            vp8.Initialize(CodecUsage.Encoder, "VP8");

            var oggPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "audio.ogg");
            var ivfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "video.ivf");

            var audioCaptureProvider = new OggAudioCaptureProvider(oggPath, opus);
            var videoCaptureProvider = new IvfVideoCaptureProvider(ivfPath, vp8);
            */

            UserMedia.GetMedia(new GetMediaArgs(Audio, Video)
            {
                //VideoCaptureProvider = new ScreenCaptureProvider(), // uncomment this line to do in-app screen capture
                VideoWidth = VideoWidth,           // optional
                VideoHeight = VideoHeight,         // optional
                VideoFrameRate = VideoFrameRate,   // optional
                DefaultVideoPreviewScale = LayoutScale.Contain, // optional
                DefaultVideoScale = LayoutScale.Contain,        // optional
                OnFailure = (e) =>
                {
                    callback(string.Format("Could not get media. {0}", e.Exception.Message));
                },
                OnSuccess = (e) =>
                {
                    // We have successfully acquired access to the local
                    // audio/video device! Grab a reference to the media.
                    // Internally, it maintains access to the local audio
                    // and video feeds coming from the device hardware.
                    LocalMediaStream = e.LocalStream;

                    // This is our local video control, a UIView (iOS) or
                    // and NSView (Mac). It is constantly updated with our
                    // live video feed since we requested video above. Add
                    // it directly to the UI or use the IceLink layout manager,
                    // which we do below.
                    var localVideoControl = e.LocalVideoControl;

                    // Create an IceLink layout manager, which makes the task
                    // of arranging video controls easy. Give it a reference
                    // to a UIView that can be filled with video feeds.
                    LayoutManager = new LayoutManager((NativeView)videoView);

                    // Position and display the local video control on-screen
                    // by passing it to the layout manager created above.
                    LayoutManager.SetLocalVideoControl(localVideoControl);

                    callback(null);
                }
            });
        }

        public void Stop(Action<string> callback)
        {
            // Clear out the layout manager.
            if (LayoutManager != null)
            {
                LayoutManager.UnsetLocalVideoControl();
                LayoutManager.RemoveRemoteVideoControls();
                LayoutManager = null;
            }

            // Stop the local media stream.
            if (LocalMediaStream != null)
            {
                LocalMediaStream.Stop();
                LocalMediaStream = null;
            }

            callback(null);
        }
    }
}