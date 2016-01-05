using System;
using Xamarin.Forms;

namespace Hangskypetime
{
    public interface IPopupService
    {
        void MakeToast(string message);
    }

    public class PopupService : IPopupService
    {
        Page Page { get; }

        public PopupService(Page page)
        {
            Page = page;
        }

        public void MakeToast(string message)
        {
            Device.BeginInvokeOnMainThread(() => 
                #if __IOS__
                ToastIOS.Toast.MakeText(message, ToastIOS.Toast.LENGTH_LONG).Show()
                #elif __ANDROID__
                Android.Widget.Toast.MakeText(Android.App.Application.Context, message, Android.Widget.ToastLength.Long).Show()
                #endif
            );
        }
    }
}
