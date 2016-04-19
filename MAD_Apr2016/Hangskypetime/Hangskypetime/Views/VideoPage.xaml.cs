using System;
using Xamarin.Forms;

namespace Hangskypetime
{
    public partial class VideoPage : ContentPage
    {
        public VideoPage(string sessionId)
        {
            InitializeComponent();
            BindingContext = new VideoViewModel(App.Instance, this, sessionId, VideoView);
        }
    }
}
