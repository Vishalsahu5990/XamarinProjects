using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BikeSpot
{
	public partial class Payment_A_Page : ContentPage
	{
		Product _productModel = null;
		int optionCount = 0;
		public Payment_A_Page()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		public Payment_A_Page(Product productModel)
		{
			InitializeComponent();
			_productModel = productModel;
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
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
		async void option_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					if (optionCount % 2 == 0)
					{
						imgTopBackgroud.IsVisible = true;
						flowlistview.HeightRequest = App.ScreenWidth / 1.3;
					}
					else
					{
						imgTopBackgroud.IsVisible = false;
						flowlistview.HeightRequest = App.ScreenHeight - 130;
					}

					optionCount++;
				});

			}
			catch (Exception ex)
			{


			}
		}
		public void Send_Clicked(object sender, EventArgs e)
		{

		}

		private void PrepareUI()
		{
			try
			{
				lblPrice.Text = _productModel.price;
				StaticDataModel._CurrentContext.IsGestureEnabled = false;
				imgProduct.Source = _productModel.product_image;
				lblTitle.Text = _productModel.product_name;
				imgTopBackgroud.HeightRequest = App.ScreenHeight / 3;
				var totalWidth = App.ScreenWidth;
				var buttonSize = totalWidth / 4;
				var boxWidth = buttonSize / 4;

				btnPaid.WidthRequest = buttonSize;
				btnAccepted.WidthRequest = buttonSize;
				btnMakeOffer1.WidthRequest = buttonSize;

				bx1.WidthRequest = boxWidth;
				bx2.WidthRequest = boxWidth;
				bx3.WidthRequest = boxWidth;
				bx4.WidthRequest = boxWidth;
				//Chat intialization
				flowlistview.HeightRequest = App.ScreenHeight - 140;

			}
			catch (Exception ex)
			{

			}
		}
	}
}
