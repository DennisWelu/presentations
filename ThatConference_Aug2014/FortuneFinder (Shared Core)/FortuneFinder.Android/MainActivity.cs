using System;

using Android.App;
using Android.OS;
using Android.Widget;

namespace FortuneFinder.Android
{
    [Activity(Label = "Fortune Finder", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Title = "Fortune Finder";

            // Get our button from the layout resource and attach an event to it
            var button = FindViewById<Button>(Resource.Id.button1);
			
            button.Click += (sender, e) => 
            {
                StartActivity(typeof(SearchActivity));
            };
        }
    }
}


