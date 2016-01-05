using System;
using Xamarin.Forms;

namespace Hangskypetime
{
    public partial class SessionPage : ContentPage
    {
        public SessionPage()
        {
            InitializeComponent();
            BindingContext = new SessionViewModel(App.Instance, this);
        }
    }
}
