using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ChangePasswordPage : ContentPage
	{
		public ChangePasswordPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			txtOldPassword.Focused += TxtOldPassword_Focused;
			txtNewPassword.Focused += TxtNewPassword_Focused;
			txtConfirmPassword.Focused += TxtConfirmPassword_Focused;
			btnBack.Clicked += BtnBack_Clicked;
			btnSubmit.Clicked += BtnSubmit_Clicked;

			if (StaticMethods.IsIpad())
				SetupIpadUI();
		}
		private void SetupIpadUI()
		{
			_slMainLayout.Padding = new Thickness(70);
			lblChangePassword.FontSize = 30;
			lblChangePassword.Margin = 20;
			_slOldPassword.HeightRequest = 70;
			_slNewpassword.HeightRequest = 70;
			_slConfirmPassword.HeightRequest = 70;
			btnBack.HeightRequest = 70;
			btnSubmit.HeightRequest = 70;
			btnBack.FontSize = 25;
			btnSubmit.FontSize = 25;
			_slLeftOldPassword.WidthRequest = 70;
			_slLeftNewPassword.WidthRequest = 70;
			_slLeftConfirmPassword.WidthRequest = 70;

			imgLogo.Margin = new Thickness(80, 0, 80, 0);
		}
		void TxtOldPassword_Focused(object sender, FocusEventArgs e)
		{
			txtOldPassword.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtNewPassword_Focused(object sender, FocusEventArgs e)
		{
			txtNewPassword.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtConfirmPassword_Focused(object sender, FocusEventArgs e)
		{
			txtConfirmPassword.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		async void BtnBack_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void BtnSubmit_Clicked(object sender, EventArgs e)
		{
			if (IsValidate())
			{
				ChangePassword();
			}
		}
		private bool IsValidate()
		{

			if (string.IsNullOrEmpty(txtOldPassword.Text))
			{
				txtOldPassword.PlaceholderColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(txtNewPassword.Text))
			{
				txtNewPassword.PlaceholderColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(txtConfirmPassword.Text))
			{
				txtConfirmPassword.PlaceholderColor = Color.Red;
				return false;
			}
			else if (txtNewPassword.Text != txtConfirmPassword.Text)
			{
				txtNewPassword.TextColor = Color.Red;
				txtConfirmPassword.TextColor = Color.Red;
				StaticMethods.ShowToast("Password does not match");
				return false;
			}
			else
			{
				return true;
			}

		}
		private async Task ChangePassword()
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						um = WebService.ChangePassword(txtOldPassword.Text, txtNewPassword.Text);


					}).ContinueWith(async
					t =>
					{
						if (um.result == "success")
						{
							StaticMethods.ShowToast(um.responseMessage);
							App.Current.MainPage = new NavigationPage(new MainPage());
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
