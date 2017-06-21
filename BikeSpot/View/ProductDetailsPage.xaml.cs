using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BikeSpot
{
	public partial class ProductDetailsPage : ContentPage
	{
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
				await Navigation.PushAsync(new Payment_A_Page());

			}
			catch (Exception ex)
			{


			}
		}
async void private_Tapped(object sender, System.EventArgs e)
{
	try
	{
		await Navigation.PushAsync(new ChatUsersPage());

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
									lblTypeofBike.Text = _productDetailsModel.type_of_bike;
									lblCondition.Text = _productDetailsModel.condition;
									lblPrice.Text = _productDetailsModel.price;
									lblProductDesc.Text = _productDetailsModel.product_description;
									if (!string.IsNullOrEmpty(_productDetailsModel.lat))
										AddBarMarkerOnMap(Convert.ToDouble(_productDetailsModel.lat), Convert.ToDouble(_productDetailsModel.@long));
									else

										AddBarMarkerOnMap(25.023176, 39.189978);

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
