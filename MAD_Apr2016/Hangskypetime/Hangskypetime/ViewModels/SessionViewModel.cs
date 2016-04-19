using System;
using Xamarin.Forms;

namespace Hangskypetime
{
    public class SessionViewModel : NotifyingObject
    {
        App App { get; }
        IPopupService Popups { get; }
        INavigation Navigation { get; }
        bool SignallingStarted { get; set; }

        public SessionViewModel(App app, Page page)
        {
            App = app;
            Popups = new PopupService(page);
            Navigation = page.Navigation;
            page.Appearing += HandlePageAppearing;
            CreateCommand = new Command(CreateSession);
            JoinCommand = new Command(JoinSession);
        }

        public string CreateSessionText
        {
            get { return Get(() => CreateSessionText, () => new FM.Randomizer().Next(100000, 999999).ToString()); }
            set { Set(() => CreateSessionText, value); }
        }

        public string JoinSessionText
        {
            get { return Get(() => JoinSessionText, string.Empty); }
            set { Set(() => JoinSessionText, value); }
        }

        public Command CreateCommand { get; private set; }
        public Command JoinCommand { get; private set; }

        private void HandlePageAppearing(object sender, EventArgs e)
        {
            if (!SignallingStarted)
                StartSignalling();
        }

        private void CreateSession(object o)
        {
            Navigation.PushAsync(new VideoPage(CreateSessionText));
        }

        private void JoinSession(object o)
        {
            if (JoinSessionText.Length == 6)
                Navigation.PushAsync(new VideoPage(JoinSessionText));
            else
                Popups.MakeToast("Session ID must be 6 digits long.");
        }

        private void StartSignalling()
        {
            App.StartSignalling((error) =>
            {
                if (error != null)
                {
                    Popups.MakeToast(error);
                }
            });
            SignallingStarted = true;
        }
    }
}
