using System;
using Hangskypetime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

// This ExportRenderer command tells Xamarin.Forms to use this renderer
// instead of the built-in one for this page
[assembly: ExportRenderer(typeof(VideoView), typeof(VideoViewRenderer))]

namespace Hangskypetime
{
    public class VideoViewRenderer : ViewRenderer<VideoView, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var videoView = new UIView(NativeView.Bounds) { BackgroundColor = UIColor.DarkGray};
            SetNativeControl(videoView);

            e.NewElement.NativeContainer = videoView;
        }
    }
}
