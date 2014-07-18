using System;
using System.ComponentModel;
using FortuneFinder.Core;
using Microsoft.Phone.Controls;

namespace FortuneFinder.WP
{
    public partial class SearchPage : PhoneApplicationPage
    {
        SearchViewModel ViewModel { get; set; }

        public SearchPage()
        {
            InitializeComponent();

            ViewModel = new SearchViewModel(new FortuneService());
            ViewModel.PropertyChanged += (sender, e) => Dispatcher.BeginInvoke(() => HandlePropertyChanged(e));

            searchTextBox.TextChanged += (sender, e) => ViewModel.SearchText = searchTextBox.Text;
            searchButton.Click += (sender, e) => ViewModel.Search();

            resultsSlider.ValueChanged += (sender, e) =>
            {
                resultsSlider.Value = (float)Math.Round(resultsSlider.Value, 0);
                ViewModel.ResultIndex = (int)resultsSlider.Value;
            };

            UpdateUI();
        }

        void HandlePropertyChanged(PropertyChangedEventArgs e)
        {
            UpdateUI();
        }

        void UpdateUI()
        {
            fortuneTextBlock.Text = ViewModel.Fortune;
            resultsTextBlock.Text = ViewModel.ResultText;
            resultsSlider.Minimum = 0;
            resultsSlider.Maximum = ViewModel.ResultCount - 1;
            resultsSlider.Value = ViewModel.ResultIndex;
        }
    }
}