using System;
using Xamarin.Forms;

#if __IOS__
using NativeView = UIKit.UIView;
#elif __ANDROID__
using NativeView = Android.Views.ViewGroup;
#endif

namespace Hangskypetime
{
    public interface IVideoView
    {
        NativeView NativeContainer { get; }
    }

    public class VideoView : View, IVideoView
    {
        public NativeView NativeContainer { get; set; }
    }
}
