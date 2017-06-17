using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BikeSpot
{
	public partial class BikeWizardStartPage : ContentPage
	{
		public BikeWizardStartPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnStart.Clicked += BtnStart_Clicked;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnStart.Clicked -= BtnStart_Clicked;
		}
		async void back_Tapped(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
		async void BtnStart_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new BikeWizardSelectBikePage());
		}
		private void PrepareUI()
		{ 
		try
			{
				if (StaticMethods.IsIpad())
				{
					lblQuestion.FontSize = 30;
					btnStart.HeightRequest = 70;
					btnStart.FontSize = 30;
					btnStart.Margin = new Thickness(20,60,20,0);
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
