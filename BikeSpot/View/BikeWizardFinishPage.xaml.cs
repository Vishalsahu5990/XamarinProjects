using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class BikeWizardFinishPage : ContentPage
	{
        List<Product> _listProduct = null;
		WizardResultModel _model = null;
		public BikeWizardFinishPage()
		{
			InitializeComponent();
			imgBike.HeightRequest = App.ScreenHeight / 3;

			PrepareUI();
		}
		public BikeWizardFinishPage(WizardResultModel model)
		{
			InitializeComponent();
			_model = model;
			imgBike.HeightRequest = App.ScreenHeight / 3;

			PrepareUI();

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnFinish.Clicked += BtnFinish_Clicked;
			btnShowBikes.Clicked += BtnShowBikes_Clicked;

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnFinish.Clicked -= BtnFinish_Clicked;
			btnShowBikes.Clicked -= BtnShowBikes_Clicked;

		}
		async void back_Tapped(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		void BtnShowBikes_Clicked(object sender, EventArgs e)
		{
			GetProducts().Wait();
		}

		void BtnFinish_Clicked(object sender, EventArgs e)
		{
			GetProducts().Wait();
			//App.Current.MainPage = new NavigationPage(new MainPage());
		}

		private void PrepareUI()
		{
			try
			{
				imgBike.Source = Constants.ImageUrl + _model.data[0].attachment;
				lblBikeType.Text = _model.data[0].type_of_bike;
				lblDescription.Text = _model.data[0].description;
				if (StaticMethods.IsIpad())
				{

					btnShowBikes.HeightRequest = 70;
					btnFinish.HeightRequest = 70;
					btnShowBikes.FontSize = 30;
					btnFinish.FontSize = 30;
					_slButton.Margin = new Thickness(0, 30, 0, 0);

				}
			}
			catch (Exception ex)
			{

			}
		}
private async Task GetProducts()
{

	StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				//_listProduct = WebService.FilterProducts(_typeOfBikes,_condition,_buy_rent,
				//                                         Convert.ToInt32(lblStartPriceRange),
				//                                         Convert.ToInt32(lblEndPriceRange),
				//                                         Convert.ToInt32(lblEndDistanceRange),
				//                                         StaticDataModel.Lattitude,
				//                                         StaticDataModel.Longitude);
				var list = new List<string>{ _model.data[0].type_of_bike};
				_listProduct = WebService.FilterProducts(list, null, null,
														 0,
														 0,
														 0,
														 "",
														 StaticDataModel.Lattitude,
														 StaticDataModel.Longitude);


			}).ContinueWith(async
					t =>
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					if (_listProduct != null)
					{
						if (_listProduct.Count > 0)
							await Navigation.PushAsync(new HomePage(_listProduct));
						else
							StaticMethods.ShowToast("No products found!");

					}
					else
					{
						StaticMethods.ShowToast("No products found!");
					}
				});


				StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
	}
}
