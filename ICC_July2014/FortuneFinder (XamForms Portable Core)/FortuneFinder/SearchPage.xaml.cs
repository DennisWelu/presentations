using System;
using System.Collections.Generic;
using System.ComponentModel;
using FortuneFinder.Core;
using Xamarin.Forms;

namespace FortuneFinder
{	
	public partial class SearchPage : ContentPage
	{	
        SearchViewModel ViewModel { get; set; }

		public SearchPage ()
		{
			InitializeComponent ();

            ViewModel = new SearchViewModel(new FortuneService());
            ViewModel.PropertyChanged += (sender, e) => Device.BeginInvokeOnMainThread(() => HandlePropertyChanged(e));

            searchBar.Placeholder = "Enter fortune keyword";

            searchBar.TextChanged += (sender, e) => ViewModel.SearchText = searchBar.Text;
            searchBar.SearchButtonPressed += (sender, e) => ViewModel.Search();

            resultsSlider.ValueChanged += (sender, e) => ViewModel.ResultIndex = (int)resultsSlider.Value;

            UpdateUI();
		}

        void HandlePropertyChanged(PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            fortuneLabel.Text = ViewModel.Fortune;
            resultsLabel.Text = ViewModel.ResultText;
            resultsSlider.Minimum = 0;
            resultsSlider.Maximum = Math.Max(resultsSlider.Minimum+1, ViewModel.ResultCount - 1);
            //resultsSlider.Value = ViewModel.ResultIndex;
        }
	}
}
