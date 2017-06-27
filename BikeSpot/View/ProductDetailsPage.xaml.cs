using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;


namespace BikeSpot
{
	public partial class ProductDetailsPage : ContentPage
	{
		bool isFavourite = false;
		Product _productModel = null;
		Product _productDetailsModel = null;
		public static int staticcustomPins;
		public ProductDetailsPage(Product productModel)
		{
			_productModel = productModel;
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		public ProductDetailsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
             GetProductsDetails().Wait();
		}
		private async void PrepareUI()
		{ 
		try
			{
				imgTopBackgroud.HeightRequest = (App.ScreenHeight / 3) - 20;
				 if (StaticMethods.IsIpad())
				{
					_slFooter.HeightRequest = 115;
					btnOnlineStore.HeightRequest = 50;
					btnOnlineStore.WidthRequest = 200;
				}
			}
			catch (Exception ex)
			{

			}
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
		async void saveuser_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (isFavourite)
				{
					var result = await DisplayAlert("Alert!", "Do you want to remove this profile?", "YES", "CANCEL");
					if (result)
					{
						AddRemoveSaveUser(_productDetailsModel.user_id, "remove_save_users", "User has been removed successfully!");
					}
				}
				else
				{ 
var result = await DisplayAlert("Alert!", "Do you want to save this profile?", "YES", "CANCEL");
					if (result)
					{
                        AddRemoveSaveUser(_productDetailsModel.user_id, "save_users", "User has been saved successfully!");
					
					}
				}

			}
			catch (Exception ex)
			{


			}
		}
		async void profile_Tapped(object sender, System.EventArgs e)
		{
			try
			{ 
				await Navigation.PushModalAsync(new UserProfilePage(Convert.ToInt32( _productModel.user_id)));

			}
			catch (Exception ex)
			{


			}
		}
async void comment_Tapped(object sender, System.EventArgs e)
{
	try
	{
				await Navigation.PushAsync(new CommentsPage(_productModel));

	}
	catch (Exception ex)
	{


	}
		}
		async void makeoffer_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (_productModel.user_id == StaticDataModel.userId.ToString())
				{
					await DisplayAlert("Alert!", "Its your post.", "OK");
				}
				else
				{
					await Navigation.PushModalAsync(new Payment_A_Page(_productModel));
				}

			}
			catch (Exception ex)
			{


			}
		}
async void private_Tapped(object sender, System.EventArgs e)
{
	try
	{
		try
			{
				if (_productModel.user_id == StaticDataModel.userId.ToString())
				{
					await DisplayAlert("Alert!", "Its your post.", "OK");
				}
				else
				{
						await Navigation.PushModalAsync(new Payment_A_Page(_productModel,"private_chat"));
				}

			}
			catch (Exception ex)
			{


			}

	}
	catch (Exception ex)
	{


	}
		}
		async void fb_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				//MessagingCenter.Send<ImageSource>(imgProduct.Source, "Share");


			}
			catch (Exception ex)
			{


			}
		}
async void twitter_Tapped(object sender, System.EventArgs e)
{
	try
	{
		//MessagingCenter.Send<ImageSource>(imgProduct.Source, "Share");


	}
	catch (Exception ex)
	{


	}
		}
async void insta_Tapped(object sender, System.EventArgs e)
{
	try
	{
		//MessagingCenter.Send<ImageSource>(imgProduct.Source, "Share");


	}
	catch (Exception ex)
	{


	}
		}
async void pintrest_Tapped(object sender, System.EventArgs e)
{
	try
	{
		//MessagingCenter.Send<ImageSource>(imgProduct.Source, "Share");


	}
	catch (Exception ex)
	{


	}
		}
async void linkedin_Tapped(object sender, System.EventArgs e)
{
	try
	{
		//MessagingCenter.Send<ImageSource>(imgProduct.Source, "Share");


	}
	catch (Exception ex)
	{


	}
		}
		private async Task GetProductsDetails()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_productDetailsModel = WebService.GetProductDetailsbyId(Convert.ToInt32(_productModel.product_id));


					}).ContinueWith(async
					t =>
					{
						try
						{
							if (_productDetailsModel != null)
							{
								Device.BeginInvokeOnMainThread(async () =>
								{
									var imageList = new List<Model>();
									var data = _productDetailsModel.product_image;
									if (!string.IsNullOrEmpty(_productDetailsModel.product_image))
									{
										
										var imageArray = data.Split(',');
										for (int i = 0; i < imageArray.Length; i++)
										{
											imageList.Add(new Model
											{
												image = Constants.ImageUrl + imageArray[i]
											});
										}
									}
									Carousel.ItemsSource = imageList;

								//imgProduct.Source = Constants.ImageUrl + _productDetailsModel.product_image; ;
								lblUserName.Text = _productDetailsModel.name;
									lblProductName.Text = _productDetailsModel.product_name;
									lblSize.Text = _productDetailsModel.framesize;
									lblGender.Text = _productDetailsModel.gender;
									lblTypeofBike.Text = _productDetailsModel.type_of_bike;
									lblCondition.Text = _productDetailsModel.condition;
									lblPrice.Text = "€"+_productDetailsModel.price;
									lblProductDesc.Text = _productDetailsModel.product_description;
									if (!string.IsNullOrEmpty(_productDetailsModel.lat))
										AddBarMarkerOnMap(Convert.ToDouble(_productDetailsModel.lat), Convert.ToDouble(_productDetailsModel.@long));
									else

										AddBarMarkerOnMap(25.023176, 39.189978);

							if (_productDetailsModel.user_id != StaticDataModel.userId.ToString())
									{
										imgHeart.IsVisible = true;

										CheckForSavedUser(_productDetailsModel.user_id);
									}
									else
									{
										imgHeart.IsVisible = false;
									}
									
								});

							}

						}
						catch (Exception ex)
						{

						}
						finally { 
				StaticMethods.DismissLoader();
				}
						

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task CheckForSavedUser(string userid)
		{
			SavedUserModel saveduserModel = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						saveduserModel = WebService.CheckSavedUser(userid);


					}).ContinueWith(async
					t =>
					{
						if (saveduserModel != null)
						{
							if (saveduserModel.data[0].user_is_favorite)
							{
								isFavourite = true;
								imgHeart.Source = "h.png";
							}
							else
							{
								isFavourite = false;
						imgHeart.Source = "h_unlike";
							}
						}
						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
private async Task AddRemoveSaveUser(string userid,string method,string Responsemessage)
{
			string ret = string.Empty;
	StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				ret = WebService.SaveUnsaveUser(userid,method);


			}).ContinueWith(async
					t =>
			{
				if (ret == "success")
				{
					StaticMethods.ShowToast(Responsemessage);
					if (method == "save_users")
					{
						isFavourite = true;
						imgHeart.Source = "h.png";
					}
					else
					{
						isFavourite = false;
						imgHeart.Source = "h_unlike.png";
					}
				}
				else
				{
					StaticMethods.ShowToast("Unable to make changes, Please try again after some time.");
				}
				StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
		private void AddBarMarkerOnMap(double lat, double lng)
		{
			_map.Pins.Clear();
			try
			{
				var pin = new CustomPin
				{
					Pin = new Pin
					{
						Type = PinType.Place,
						Position = new Xamarin.Forms.Maps.Position(lat, lng),
						Label = "Path",
						Address = Guid.NewGuid().ToString(),
					},
					Id = "Xamarin",

					//Url = BarDetails.url

				};
				staticcustomPins = 0;
				_map.CustomPins = new List<CustomPin> { pin };
				_map.Pins.Add(pin.Pin);
				_map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(lat, lng), Distance.FromMiles(2)));

			}
			catch (Exception ex)
			{


			}
		}
	}
}
