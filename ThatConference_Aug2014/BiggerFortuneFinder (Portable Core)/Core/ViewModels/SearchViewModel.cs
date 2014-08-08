using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using FortuneFinder.Core.ViewModels;

namespace FortuneFinder.Core
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        IFortuneService FortuneService { get; set; }
        Fortunes Results { get; set; }

        public SearchViewModel(IFortuneService fortuneService)
        {
            FortuneService = fortuneService;
            Results = new Fortunes();

            // React to SearchText changes only when they've paused for a couple seconds
            this.ObservableForProperty(vm => vm.SearchText)
                .Throttle(TimeSpan.FromSeconds(2))
                .Subscribe(text => Search());
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName] string propName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        string _searchText = string.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    RaisePropertyChanged();
                }
            }
        }

        string _resultText = string.Empty;
        public string ResultText
        {
            get { return _resultText; }
            set
            {
                if (value != _resultText)
                {
                    _resultText = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int ResultCount
        {
            get { return Results.Count; }
        }

        int _resultIndex = 0;
        public int ResultIndex
        {
            get { return _resultIndex; }
            set
            {
                if (value != _resultIndex)
                {
                    _resultIndex = value;
                    Fortune = Results.Any() ? Results[value] : string.Empty;
                    RaisePropertyChanged();
                }
            }
        }

        string _fortune = string.Empty;
        public string Fortune
        {
            get { return _fortune; }
            set
            {
                if (value != _fortune)
                {
                    _fortune = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void Search()
        {
            Results = FortuneService.GetFortunes(SearchText);
            ResultIndex = 0;
            ResultText = string.Format("{0} Results", Results.Count);
        }
    }
}
