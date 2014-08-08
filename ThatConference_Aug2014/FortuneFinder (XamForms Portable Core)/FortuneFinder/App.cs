using System;
using Xamarin.Forms;

namespace FortuneFinder
{
    public class App
    {
        public static Page GetMainPage()
        {	
            var button = new Button
            {
                Text = "Open Cookie Jar",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var contentPage = new ContentPage
            { 
                Content = button,
                Title = "Fortune Finder"
            };

            button.Clicked += (sender, e) => contentPage.Navigation.PushAsync(new SearchPage());

            return new NavigationPage(contentPage);
        }
    }
}

