using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class UserProfilePage : ContentPage
	{
		ProfileModel _profileModel = null;
		MyProfileDataModel.Data _profileDataModel = null;
		CategoryModel _categoryModel = null;
		int _userId = 0;
		double itemWidth = 140;
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


		}
		private void PrepaireUI()
		{
			try
			{
				flowlistview.FlowColumnMinWidth = App.ScreenWidth / 2 - 50;
				imgProfile.HeightRequest = App.ScreenWidth / 5;
				imgProfile.WidthRequest = App.ScreenWidth / 5;
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
			//slSell.IsVisible = true;
			//slRent.IsVisible = false;
			Device.BeginInvokeOnMainThread(() =>
			{

				GetProfileData("sell").Wait();

			});
		}

		void BtnRent_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			//slSell.IsVisible = false;
			//slRent.IsVisible = true;
			Device.BeginInvokeOnMainThread(() =>
			{

				GetProfileData("rent").Wait();

			});
		}

		public void SellTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			//slSell.IsVisible = true;
			//slRent.IsVisible = false;
			Device.BeginInvokeOnMainThread(() =>
			{

				GetProfileData("sell").Wait();

			});
		}
		public void RentTapped(object sender, EventArgs e)
		{
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			//slSell.IsVisible = false;
			//slRent.IsVisible = true;
			Device.BeginInvokeOnMainThread(() =>
			{

				GetProfileData("rent").Wait();

			});
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
		private async Task GetProfile()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(

					() =>
					{

                _profileModel = WebService.GetProfile(_userId);


					}).ContinueWith(async
					t =>
					{
                try
                        {
							if (_profileModel != null)
							{
								Device.BeginInvokeOnMainThread(async () =>
								{
									lblUserName.Text = _profileModel.data[0].name;
									lblEmail.Text = _profileModel.data[0].website_url;
									lblReviews.Text = _profileModel.data[0].total_reviews + " Reviews";


									if (!string.IsNullOrEmpty(_profileModel.data[0].profile_pic))
										imgProfile.Source = Constants.ProfilePicUrl + _profileModel.data[0].profile_pic;
									else
										imgProfile.Source = "dummyprofile.png";

									if (!string.IsNullOrEmpty(_profileModel.data[0].ratings))
									{

										var val = Math.Round(Convert.ToDouble(_profileModel.data[0].ratings), 3);
										lblRating.Text = val.ToString();
									}
									var model = StaticMethods.GetLocalSavedData();
									if (model != null)
									{
										if (model.is_retailer == "1")
										{
											imgRibbon.IsVisible = true;
										}
									}
									GetProfileData("rent").Wait();
								});
							}

						}
                        catch (Exception ex)
                        {

                        }
                finally
                {

							Device.BeginInvokeOnMainThread(() =>
					{

						GetProfileData("sell").Wait();

					});
                }
						

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetProfileData(string filterType)
		{
			string imgData = string.Empty;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(

					() =>
					{

						_profileDataModel = WebService.GetProductByUserId();


					}).ContinueWith(async
					t =>
					{
						if (_profileDataModel != null)
						{
							Device.BeginInvokeOnMainThread(async () =>
							{

								if (filterType == "rent")
								{
									var _listProduct = _profileDataModel.rent;
									for (int i = 0; i < _listProduct.Count; i++)
									{


										if (!string.IsNullOrEmpty(_listProduct[i].product_image))
										{
											imgData = _listProduct[i].product_image;

										}
										_listProduct[i].width = itemWidth - 15;
										_listProduct[i].imageHeight = itemWidth - 50;
										var array = imgData.Split(',');
										if (array != null)
										{
											_listProduct[i].product_image = Constants.ImageUrl + array[0];
										}
									}
									flowlistview.FlowItemsSource = _listProduct;

								}
								else if (filterType == "sell")
								{
									var _listProduct = _profileDataModel.sell;
									for (int i = 0; i < _listProduct.Count; i++)
									{


										if (!string.IsNullOrEmpty(_listProduct[i].product_image))
										{
											imgData = _listProduct[i].product_image;

										}
										_listProduct[i].width = itemWidth - 15;
										_listProduct[i].imageHeight = itemWidth - 50;
										var array = imgData.Split(',');
										if (array != null)
										{
											_listProduct[i].product_image = Constants.ImageUrl + array[0];
										}

									}
									flowlistview.FlowItemsSource = _listProduct;


								}


							});






							StaticMethods.DismissLoader();
						}
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
