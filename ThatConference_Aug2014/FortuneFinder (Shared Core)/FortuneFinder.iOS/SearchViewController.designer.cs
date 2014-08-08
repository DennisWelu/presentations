// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace FortuneFinder
{
	[Register ("SearchViewController")]
	partial class SearchViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel fortuneLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel resultsLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISlider resultsSlider { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISearchBar searchBar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (fortuneLabel != null) {
				fortuneLabel.Dispose ();
				fortuneLabel = null;
			}
			if (resultsLabel != null) {
				resultsLabel.Dispose ();
				resultsLabel = null;
			}
			if (resultsSlider != null) {
				resultsSlider.Dispose ();
				resultsSlider = null;
			}
			if (searchBar != null) {
				searchBar.Dispose ();
				searchBar = null;
			}
		}
	}
}
