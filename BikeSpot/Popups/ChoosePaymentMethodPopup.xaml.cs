using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ChoosePaymentMethodPopup : PopupPage
	{
		string _title = string.Empty;

		public ChoosePaymentMethodPopup(string title)
		{
			InitializeComponent();
			_title = title;
			lblTitle.Text = _title;
		}

		async void cross_Tapped(object sender, EventArgs e)
		{
			await Navigation.PopPopupAsync();
		}
	}
}
