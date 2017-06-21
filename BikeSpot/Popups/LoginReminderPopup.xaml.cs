using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class LoginReminderPopup : PopupPage
	{
		public LoginReminderPopup()
		{
			InitializeComponent();
		}
		async void close_Tapped(object sender, EventArgs e)
		{

			await Navigation.PopPopupAsync();
		}
		async void cancel_clicked(object sender, EventArgs e)
		{

			await Navigation.PopPopupAsync();
		}
		async void ok_clicked(object sender, EventArgs e)
		{

			await Navigation.PopPopupAsync();
			MessagingCenter.Send<object, string>(this, "PopCurrentPage", "");
		}
	}
}

