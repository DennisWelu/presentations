using System;
using System.ComponentModel;

using MonoTouch.UIKit;
using FortuneFinder.Core;

namespace FortuneFinder
{
    public partial class SearchViewController : UIViewController
    {
        SearchViewModel ViewModel { get; set; }

        public SearchViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new SearchViewModel(new FortuneService());
            ViewModel.PropertyChanged += (sender, e) => InvokeOnMainThread(() => HandlePropertyChanged(e));
        }

        void HandlePropertyChanged(PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            searchBar.Placeholder = "Enter fortune keyword";

            searchBar.TextChanged += (sender, e) => ViewModel.SearchText = searchBar.Text;
            searchBar.SearchButtonClicked += (sender, e) => ViewModel.Search();

            resultsSlider.ValueChanged += (sender, e) =>
            {
                resultsSlider.SetValue((float)Math.Round(resultsSlider.Value, 0), true);
                ViewModel.ResultIndex = (int)resultsSlider.Value;
            };

            UpdateUI();
        }

        void UpdateUI()
        {
            fortuneLabel.Text = ViewModel.Fortune;
            resultsLabel.Text = ViewModel.ResultText;
            resultsSlider.MinValue = 0;
            resultsSlider.MaxValue = ViewModel.ResultCount - 1;
            resultsSlider.Value = ViewModel.ResultIndex;
        }
    }
}
