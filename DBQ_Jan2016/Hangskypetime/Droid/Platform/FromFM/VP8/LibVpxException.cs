using System;
using System.Runtime.InteropServices;

using Android.Runtime;

namespace Xamarin.Conference.WebRTC.VP8
{
    class LibVpxException : Exception
    {
        public LibVpxException(string msg)
            : base(msg)
        { }
    }
}