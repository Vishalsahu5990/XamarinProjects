using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class MyProfilePage : ContentPage
	{
		ProfileModel _profileModel = null;
		MyProfileDataModel.Data _profileDataModel = null;
		double itemWidth = 140;
		string filterType = string.Empty;
		public MyProfilePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepaireUI();

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			btnSell.Clicked += BtnSell_Clicked;
			btnRent.Clicked += BtnRent_Clicked;
			btnProfile.Clicked += BtnProfile_Clicked;

			btnOfferingRent.Clicked += BtnOfferingRent_Clicked;
			btnOfferingSell.Clicked += BtnOfferingSell_Clicked;
			btnOfferingSold.Clicked += BtnOfferingSold_Clicked;

			btnBuyingRent.Clicked += BtnBuyingRent_Clicked;
			btnBuyingSell.Clicked += BtnBuyingSell_Clicked;

			flowlistview.FlowItemTapped += Flowlistview_FlowItemTapped;

			//Getting Profile data
			GetProfile().Wait();


		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			btnSell.Clicked -= BtnSell_Clicked;
			btnRent.Clicked -= BtnRent_Clicked;
			btnProfile.Clicked -= BtnProfile_Clicked;
			flowlistview.FlowItemTapped -= Flowlistview_FlowItemTapped;

		}
		private void PrepaireUI()
		{
			try
			{
				_rlTopBackgroud.HeightRequest = App.ScreenHeight / 2.4;
				flowlistview.FlowColumnMinWidth = App.ScreenWidth / 2 - 50;
				flowlistviewBuying.FlowColumnMinWidth = App.ScreenWidth / 2 - 50;

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

		async void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as MyProfileDataModel.Sell;
			var productModel = new Product();
			productModel.product_id = item.product_id;
			productModel.product_name = item.product_name;
			productModel.gender = item.gender;
			productModel.address = item.address;
			productModel.lat = item.lat;
			productModel.@long = item.@long;
			productModel.type_of_bike = item.type_of_bike;
			productModel.product_image = item.product_image;
			productModel.type = item.type;
			productModel.condition = item.condition;

			productModel.currency = item.currency;
			productModel.price = item.price.ToString();
			productModel.rent_time = item.rent_time;
			productModel.framesize = item.framesize;
			productModel.add_to_top = item.add_to_top;


			var currentPage = ((Application.Current.MainPage as MasterDetailPage).Detail as NavigationPage).CurrentPage;
			if (!(currentPage is ProductDetailsPage))
			{
				await Navigation.PushModalAsync(new ProductDetailsPage(productModel, true));
			}

		}

		async void Delete_Clicked(object sender, System.EventArgs e)
		{
			var buton = (Xamarin.Forms.Button)sender;
			var id = buton.CommandParameter.ToString();
			if (!string.IsNullOrEmpty(id))
			{
				var result = await DisplayAlert("Alert!", "Do you want to delete this?", "Yes", "No");
				if (result)
					DeleteProduct(id).Wait();
			}

		}

		void BtnBuyingRent_Clicked(object sender, EventArgs e)
		{
			filterType = "rent";
			Device.BeginInvokeOnMainThread(() => 
			{
				btnBuyingRent.BackgroundColor = Color.FromHex("#1FD7D7");
				btnBuyingSell.BackgroundColor = Color.FromHex("#D5D6D7");
				GetBuyingProfileData("rent").Wait();

			});

		}

		void BtnBuyingSell_Clicked(object sender, EventArgs e)
		{
			filterType = "sold";
			Device.BeginInvokeOnMainThread(() =>
			{
				btnBuyingRent.BackgroundColor = Color.FromHex("#D5D6D7");
				btnBuyingSell.BackgroundColor = Color.FromHex("#1FD7D7");
				GetBuyingProfileData("sold").Wait();

			});

		}

		void BtnOfferingRent_Clicked(object sender, EventArgs e)
		{
			filterType = "rent";
			Device.BeginInvokeOnMainThread(() =>
		{
			btnOfferingRent.BackgroundColor = Color.FromHex("#1FD7D7");
			btnOfferingSell.BackgroundColor = Color.FromHex("#D5D6D7");
			btnOfferingSold.BackgroundColor = Color.FromHex("#D5D6D7");
			GetProfileData("rent").Wait();

		});
		}

		void BtnOfferingSell_Clicked(object sender, EventArgs e)
		{
			filterType = "sell";
			Device.BeginInvokeOnMainThread(() =>
		{
			btnOfferingRent.BackgroundColor = Color.FromHex("#D5D6D7");
			btnOfferingSell.BackgroundColor = Color.FromHex("#1FD7D7");
			btnOfferingSold.BackgroundColor = Color.FromHex("#D5D6D7");
			GetProfileData("sell").Wait();

		});
		}

		void BtnOfferingSold_Clicked(object sender, EventArgs e)
		{
			filterType = "sold";
			Device.BeginInvokeOnMainThread(() =>
		{
			btnOfferingRent.BackgroundColor = Color.FromHex("#D5D6D7");
			btnOfferingSell.BackgroundColor = Color.FromHex("#D5D6D7");
			btnOfferingSold.BackgroundColor = Color.FromHex("#1FD7D7");
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
		async void yourReviewsTapped(object sender, EventArgs e)
		{
            await Navigation.PushModalAsync(new MyReviewsPage());
			//await  DisplayAlert("Message","It will work in next version.","OK");
		}
		async void upgradeAccountTapped(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() =>
				{
					DisplayAlert("", "Please contact office@bikespot.at for becoming a retailer", "OK");
				});
			//await DisplayAlert("Message","It will work in next version.","OK");
		}
		async void paymentMethodsTapped(object sender, EventArgs e)
		{
			//await DisplayAlert("Message", "It will work in next version.", "OK");
            await Navigation.PushModalAsync(new AddCardDetailsPage());
			//await Navigation.PushPopupAsync(new ChoosePaymentMethodPopup("Payment Method"));
		}
		async void settingsTapped(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new Settings(true));
		}
		async void BtnSell_Clicked(object sender, EventArgs e)
		{
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			bxProfile.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
			_slProfile.IsVisible = false;
			filterType = "rent";
			Device.BeginInvokeOnMainThread(() =>
			{
				btnOfferingRent.BackgroundColor = Color.FromHex("#1FD7D7");
				btnOfferingSell.BackgroundColor = Color.FromHex("#D5D6D7");
				btnOfferingSold.BackgroundColor = Color.FromHex("#D5D6D7");
				GetProfileData("rent").Wait();

			});
		}

		void BtnRent_Clicked(object sender, EventArgs e)
		{
			filterType = "rent";
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			bxProfile.IsVisible = false;
			slSell.IsVisible = false;
			slRent.IsVisible = true;
			_slProfile.IsVisible = false;
			Device.BeginInvokeOnMainThread(() =>
			{
				btnBuyingRent.BackgroundColor = Color.FromHex("#1FD7D7");
				btnBuyingSell.BackgroundColor = Color.FromHex("#D5D6D7");
				GetBuyingProfileData("rent").Wait();

			});


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
			filterType = "rent";
			bxSell.IsVisible = true;
			bxRent.IsVisible = false;
			bxProfile.IsVisible = false;
			slSell.IsVisible = true;
			slRent.IsVisible = false;
			_slProfile.IsVisible = false;
			Device.BeginInvokeOnMainThread(() =>
		{
			btnOfferingRent.BackgroundColor = Color.FromHex("#1FD7D7");
			btnOfferingSell.BackgroundColor = Color.FromHex("#D5D6D7");
			btnOfferingSold.BackgroundColor = Color.FromHex("#D5D6D7");
			GetProfileData("rent").Wait();

		});
		}
		public void RentTapped(object sender, EventArgs e)
		{
			filterType = "rent";
			bxSell.IsVisible = false;
			bxRent.IsVisible = true;
			slSell.IsVisible = false;
			slRent.IsVisible = true;
			_slProfile.IsVisible = false;
			Device.BeginInvokeOnMainThread(() =>
			{
				btnBuyingRent.BackgroundColor = Color.FromHex("#1FD7D7");
				btnBuyingSell.BackgroundColor = Color.FromHex("#D5D6D7");
				GetBuyingProfileData("rent").Wait();

			});


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

                                lblMyReviews.Text=_profileModel.data[0].get_review + " Reviews";

								lblMail.Text = _profileModel.data[0].email;
								lblContactNo.Text = _profileModel.data[0].contact_number;
								lblSavedUser.Text = _profileModel.data[0].saved_users + " Saved Users";

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
                                _listProduct[i].price = "€ " + _listProduct[i].price;
									}
									flowlistview.FlowItemsSource = _listProduct;
									lblItemsCount.Text = _listProduct.Count + " ITEMS";
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
										_listProduct[i].price = "€ " + _listProduct[i].price;
									}
									flowlistview.FlowItemsSource = _listProduct;
									lblItemsCount.Text = _listProduct.Count + " ITEMS";

								}
								else if (filterType == "sold")
								{
									var _listProduct = _profileDataModel.sold;
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
										_listProduct[i].price = "€ " + _listProduct[i].price;
									}
									flowlistview.FlowItemsSource = _listProduct;
									lblItemsCount.Text = _listProduct.Count + " ITEMS";

								}

							});






							StaticMethods.DismissLoader();
						}
						else
						{
							flowlistview.FlowItemsSource = null;
						}
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetBuyingProfileData(string filterType)
		{
			string imgData = string.Empty;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(

					() =>
					{

						_profileDataModel = WebService.GetBuyingProductByUserId();


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
										_listProduct[i].price = "€ " + _listProduct[i].price;
									}
									flowlistviewBuying.FlowItemsSource = _listProduct;
									lblBuyingItemsCount.Text = _listProduct.Count + " ITEMS";
								}

								else if (filterType == "sold")
								{
									var _listProduct = _profileDataModel.sold;
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
										_listProduct[i].price = "€ " + _listProduct[i].price;
									}
									flowlistviewBuying.FlowItemsSource = _listProduct;
									lblBuyingItemsCount.Text = _listProduct.Count + " ITEMS";

								}

							});






							StaticMethods.DismissLoader();
						}
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task DeleteProduct(string product_id)
		{
			string ret = string.Empty;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(async () =>
			{
				ret = WebService.DeleteProductByProductId(product_id);
			}).ContinueWith
				((arg) =>
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					if (!string.IsNullOrEmpty(ret))
					{
						StaticMethods.ShowToast("Product has been deleted successfully");
						GetProfileData(filterType).Wait();
					}
					else
					{
						StaticMethods.ShowToast("Failed to delete product!, Please try again later.");
					}

				});


			});
			StaticMethods.DismissLoader();
		}
	}
}
