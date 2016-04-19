using System;
using Android.Views;
using FM.IceLink.WebRTC;
using Hangskypetime;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

// This ExportRenderer command tells Xamarin.Forms to use this renderer
// instead of the built-in one for this page
[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]

namespace Hangskypetime
{
    public class VideoViewRenderer : ViewRenderer<VideoView, ViewGroup>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var videoView = new Android.Widget.RelativeLayout(Context);
            videoView.SetBackgroundColor(Android.Graphics.Color.Gray);
            SetNativeControl(videoView);

            e.NewElement.NativeContainer = videoView;

            // Android's video providers need a context in order to create surface views for video
            // rendering, so we need to supply one before we start up the local media.
            DefaultProviders.AndroidContext = videoView.Context;
        }
    }
}
