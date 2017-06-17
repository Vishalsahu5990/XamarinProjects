using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class UserProfilePage : ContentPage
	{ ProfileModel _profileModel = null;
		CategoryModel _categoryModel = null;
		int  _userId=0;
		public UserProfilePage(int userId) 
		{
			InitializeComponent(); 
			_userId = userId;
			NavigationPage.SetHasNavigationBar(this, false);
			PrepaireUI();
			btnSell.Clicked += BtnSell_Clicked;
			btnRent.Clicked += BtnRent_Clicked;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			btnSell.Clicked += BtnSell_Clicked; 
			btnRent.Clicked += BtnRent_Clicked;
			GetProfile().Wait();

			flowlistview.FlowItemsSource = Enumerable.Range(0, 10).ToList();
		}
		private void PrepaireUI()
		{
			try
			{
					flowlistview.FlowColumnMinWidth = App.ScreenWidth / 2-50;
				bxSell.IsVisible = true;
				bxRent.IsVisible = false;
				slSell.IsVisible = true;
				slRent.IsVisible = false;
			}
			catch (Exception ex)
			{

			}
		}
async void back_Tapped(object sender, System.EventArgs e)
{
	try
	{
				await Navigation.PopModalAsync(true);

	}
	catch (Exception ex)
	{


	}
		}
		void BtnSell_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
		}

		void BtnRent_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			slSell.IsVisible = false;
			slRent.IsVisible = true;
		}

		public void SellTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
		}
		public void RentTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			slSell.IsVisible = false;
			slRent.IsVisible = true;
		}
		async void fb_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				


			}
			catch (Exception ex)
			{


			}
		}
		async void twitter_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				


			}
			catch (Exception ex)
			{


			}
		}
		async void insta_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				


			}
			catch (Exception ex)
			{


			}
		}
		async void pintrest_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				


			}
			catch (Exception ex)
			{


			}
		}
		async void linkedin_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				


			}
			catch (Exception ex)
			{


			}
		}
private async Task GetProfile()
{

	StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				_profileModel = WebService.GetProfile(_userId);


			}).ContinueWith(async
					t =>
			{
				if (_profileModel.data != null)
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						lblUserName.Text = _profileModel.data[0].name;
						lblEmail.Text = _profileModel.data[0].email;
						lblReviews.Text = _profileModel.data[0].total_reviews + " Reviews";
						if (!string.IsNullOrEmpty(_profileModel.data[0].ratings))
						{

							var val = Math.Round(Convert.ToDouble(_profileModel.data[0].ratings), 3);
							lblRating.Text = val.ToString();
						}

					});
				}


				StaticMethods.DismissLoader(); 

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
private async Task GetRentSellProducts()
{

	StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				_categoryModel = WebService.GetCategoriesProductbyUserId(_userId);


			}).ContinueWith(async
					t =>
			{
				if (_categoryModel.data.sell != null)
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						

					});
				}


				StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
	}
}
