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
		MyProfileDataModel.Data _profileDataModel = null;
double itemWidth = 140;
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

			btnOfferingRent.Clicked+= BtnOfferingRent_Clicked;
			btnOfferingSell.Clicked+= BtnOfferingSell_Clicked;
			btnOfferingSold.Clicked+= BtnOfferingSold_Clicked;
			GetProfile().Wait();


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
				_rlTopBackgroud.HeightRequest = App.ScreenHeight / 2.4;
				flowlistview.FlowColumnMinWidth = App.ScreenWidth / 2 - 50;

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

		void BtnOfferingRent_Clicked(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
		{
				btnOfferingRent.BackgroundColor =Color.FromHex("#1FD7D7");
				btnOfferingSell.BackgroundColor =Color.FromHex("#D5D6D7");
				btnOfferingSold.BackgroundColor =Color.FromHex("#D5D6D7");
				GetProfileData("rent").Wait();
			
		});
		}

		void BtnOfferingSell_Clicked(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
		{
				btnOfferingRent.BackgroundColor =Color.FromHex("#D5D6D7");
				btnOfferingSell.BackgroundColor =Color.FromHex("#1FD7D7");
				btnOfferingSold.BackgroundColor =Color.FromHex("#D5D6D7");
				GetProfileData("sell").Wait();
			
		});
		}

		void BtnOfferingSold_Clicked(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
		{
					btnOfferingRent.BackgroundColor =Color.FromHex("#D5D6D7");
				btnOfferingSell.BackgroundColor =Color.FromHex("#D5D6D7");
				btnOfferingSold.BackgroundColor =Color.FromHex("#1FD7D7");
				GetProfileData("sold").Wait();
			
		});
		}

		async void backTapped(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync(true);
		}
		async void optionMenuTapped(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new UpdateProfilePage());
		}
async void savedusersTapped(object sender, EventArgs e)
{
			await Navigation.PushModalAsync(new SavedUsersPage());
		}
		async void BtnSell_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			bxProfile.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
			_slProfile.IsVisible = false;
			Device.BeginInvokeOnMainThread(() =>
			{
				btnOfferingRent.BackgroundColor =Color.FromHex("#1FD7D7");
				btnOfferingSell.BackgroundColor =Color.FromHex("#D5D6D7");
				btnOfferingSold.BackgroundColor =Color.FromHex("#D5D6D7");
					GetProfileData("rent").Wait();

			});
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
			Device.BeginInvokeOnMainThread(() =>
		{
				btnOfferingRent.BackgroundColor =Color.FromHex("#1FD7D7");
				btnOfferingSell.BackgroundColor =Color.FromHex("#D5D6D7");
				btnOfferingSold.BackgroundColor =Color.FromHex("#D5D6D7");
				GetProfileData("rent").Wait();
			
		});
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
								lblEmail.Text = _profileModel.data[0].website_url;
								lblReviews.Text = _profileModel.data[0].total_reviews + " Reviews";

								lblMail.Text = _profileModel.data[0].email;
								lblContactNo.Text = _profileModel.data[0].contact_number;
								lblSavedUser.Text = _profileModel.data[0].saved_users+" Saved Users";

								if (!string.IsNullOrEmpty(_profileModel.data[0].profile_pic))
									imgProfile.Source = Constants.ProfilePicUrl + _profileModel.data[0].profile_pic;

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
							lblItemsCount.Text = _listProduct.Count+" ITEMS";
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
								flowlistview.FlowItemsSource = _listProduct;
								lblItemsCount.Text = _listProduct.Count+" ITEMS";
									}

								}
						else if (filterType == "sold")
								{
									var _listProduct = _profileDataModel.sell;
									for (int i = 0; i<_listProduct.Count; i++)
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
								flowlistview.FlowItemsSource = _listProduct;
								lblItemsCount.Text = _listProduct.Count+" ITEMS";
									}

								}
								
							});

						
						



							StaticMethods.DismissLoader();
						}
					                                       }, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
