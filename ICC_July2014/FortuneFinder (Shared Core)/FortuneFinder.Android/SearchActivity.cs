using System.ComponentModel;
using Android.App;
using Android.OS;
using Android.Widget;
using FortuneFinder.Core;

namespace FortuneFinder.Android
{
    [Activity(Label = "SearchActivity")]			
    public class SearchActivity : Activity
    {
        SearchViewModel ViewModel { get; set; }
        TextView _resultsTextView;
        SeekBar _resultsSeekBar;
        TextView _fortuneTextView;

        public SearchActivity()
        {
            ViewModel = new SearchViewModel(new FortuneService());
            ViewModel.PropertyChanged += (sender, e) => RunOnUiThread(() => HandlePropertyChanged(e));
        }

        void HandlePropertyChanged(PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Search);

            Title = "Search";

            _resultsTextView = FindViewById<TextView>(Resource.Id.resultsTextView);
            _fortuneTextView = FindViewById<TextView>(Resource.Id.fortuneTextView);
            _resultsSeekBar = FindViewById<SeekBar>(Resource.Id.resultsSeekBar);
            var searchView = FindViewById<SearchView>(Resource.Id.searchView);

            searchView.SetQueryHint("Enter fortune keyword");
            searchView.QueryTextChange += (sender, e) => ViewModel.SearchText = searchView.Query;
            searchView.QueryTextSubmit += (sender, e) => ViewModel.Search();

            _resultsSeekBar.ProgressChanged += (sender, e) => ViewModel.ResultIndex = _resultsSeekBar.Progress;

            UpdateUI();
        }

        void UpdateUI()
        {
            _fortuneTextView.Text = ViewModel.Fortune;
            _resultsTextView.Text = ViewModel.ResultText;
            _resultsSeekBar.Max = ViewModel.ResultCount - 1;
            _resultsSeekBar.Progress = ViewModel.ResultIndex;
        }
    }
}
