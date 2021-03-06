﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ForgotPasswordPage : ContentPage
	{
		public ForgotPasswordPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			btnBack.Clicked += BtnBack_Clicked;
			btnSubmit.Clicked += BtnSubmit_Clicked;
			txtEmail.Focused += TxtEmail_Focused;
			txtEmail.TextChanged += TxtEmail_TextChanged;
			if (StaticMethods.IsIpad())
				SetupIpadUI();
		}
		private void SetupIpadUI()
		{
			_slMainLayout.Padding = new Thickness(70);
			lblForgotPassword.FontSize = 30;
			lblForgotPassword.Margin = 20;
			_slEmail.HeightRequest = 70;
			btnBack.HeightRequest = 70;
			btnSubmit.HeightRequest = 70;
			btnBack.FontSize = 25;
			btnSubmit.FontSize = 25; 
			_slLeftEmail.WidthRequest = 70;  

			imgLogo.Margin = new Thickness(80, 0, 80, 0);
		}
		void TxtEmail_Focused(object sender, FocusEventArgs e)
		{
			txtEmail.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEmail.Text))
			{
				if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
				{
					txtEmail.TextColor = Color.Red;

				}
				else
				{
					txtEmail.TextColor = Color.Black;
				}
			}
		}

		async void BtnBack_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		async void BtnSubmit_Clicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEmail.Text))
			{
				if (!string.IsNullOrEmpty(txtEmail.Text))
				{
					if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
					{
						txtEmail.TextColor = Color.Red;
						StaticMethods.ShowToast("Invalid email.");

					}
					else
					{
						ForgotPassword().Wait();
					}
				}
			}
			else
			{
				txtEmail.PlaceholderColor = Color.Red;
			}
		}
		private async Task ForgotPassword()
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						um = WebService.ForgotPassword(txtEmail.Text);


					}).ContinueWith(async
					t =>
					{
						if (um.result == "success")
						{
							StaticMethods.ShowToast(um.responseMessage);
							await Navigation.PushAsync(new LoginPage());
						}
						else
						{
							StaticMethods.ShowToast(um.responseMessage);
						}

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
