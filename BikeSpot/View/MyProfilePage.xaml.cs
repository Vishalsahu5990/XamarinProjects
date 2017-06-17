using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class MyProfilePage : ContentPage
	{
		ProfileModel _profileModel = null;
		public MyProfilePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepaireUI();
			//StaticDataModel._CurrentContext.IsPresented = false;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			btnSell.Clicked += BtnSell_Clicked;
			btnRent.Clicked += BtnRent_Clicked;
			btnProfile.Clicked += BtnProfile_Clicked;
			GetProfile().Wait(); 

			flowlistview.FlowItemsSource = Enumerable.Range(0,10).ToList();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			btnSell.Clicked -= BtnSell_Clicked;
			btnRent.Clicked -= BtnRent_Clicked;
			btnProfile.Clicked -= BtnProfile_Clicked;

		}
		private void PrepaireUI()
		{
			try
			{
				_rlTopBackgroud.HeightRequest = App.ScreenHeight /2.4;
				flowlistview.FlowColumnMinWidth = App.ScreenWidth / 2-50;

				imgProfile.HeightRequest = App.ScreenWidth / 5;
				imgProfile.WidthRequest = App.ScreenWidth / 5;

				bxSell.IsVisible = false;
				bxRent.IsVisible = false;
				slSell.IsVisible = false;
				slRent.IsVisible = false;
				bxProfile.IsVisible = true;
				_slProfile.IsVisible = true;
			}
			catch (Exception ex)
			{

			}
		}
		async void backTapped(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync(true);
		}
		void optionMenuTapped(object sender, EventArgs e)
		{

		}

		void BtnSell_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			bxProfile.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
			_slProfile.IsVisible = false;
		}

		void BtnRent_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			bxProfile.IsVisible = false;
			slSell.IsVisible = false;
			slRent.IsVisible = true;
			_slProfile.IsVisible = false;
		}
		void BtnProfile_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = false;
			bxProfile.IsVisible = true;
			slSell.IsVisible = false;
			slRent.IsVisible = false;
			_slProfile.IsVisible = true;
		}
		public void SellTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			bxProfile.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
			_slProfile.IsVisible = false;
		}
		public void RentTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			slSell.IsVisible = false;
			slRent.IsVisible = true;
			_slProfile.IsVisible = false;
		}
		public void ProfileTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			slSell.IsVisible = false;
			slRent.IsVisible = false;
			_slProfile.IsVisible = true;
		}
private async Task GetProfile()
{

	StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

_profileModel = WebService.GetProfile(StaticDataModel.userId);


			}).ContinueWith(async
					t =>
			{
				if (_profileModel != null)
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						lblUserName.Text = _profileModel.data[0].name;
						lblEmail.Text = _profileModel.data[0].email;
						lblReviews.Text = _profileModel.data[0].total_reviews + " Reviews";
						if (!string.IsNullOrEmpty(_profileModel.data[0].ratings))
						{
							
							var val = Math.Round(Convert.ToDouble(_profileModel.data[0].ratings),3);
							lblRating.Text = val.ToString();
						}

					});
				}


				StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}

	}
}
