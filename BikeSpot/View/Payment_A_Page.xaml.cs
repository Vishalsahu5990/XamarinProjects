using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BikeSpot
{
	public partial class Payment_A_Page : ContentPage
	{
		public Payment_A_Page()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

		}
async void back_Tapped(object sender, System.EventArgs e)
{
	try
	{
		await Navigation.PopAsync();

	}
	catch (Exception ex)
	{


	}
		}
	}
}
