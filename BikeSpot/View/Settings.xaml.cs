using System;
using System.Collections.Generic;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class Settings : ContentPage
	{
		bool _flag = false;
		public Settings()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
public Settings(bool flag)
{
	_flag = flag;
		InitializeComponent();
			imgBack.Source = "back.png";
	NavigationPage.SetHasNavigationBar(this, false);
	PrepareUI();
		}
		private void PrepareUI()
		{ 
			try 
			{
				if (StaticMethods.IsIpad())
				{
					slNotification.HeightRequest = 70;
					slChangePassword.HeightRequest = 70;
					slChangeEmail.HeightRequest = 70;
					slEmailus.HeightRequest = 70;
					slWebsite.HeightRequest = 70;
					slCall.HeightRequest = 70;
					slReview.HeightRequest = 70;
					slHelp.HeightRequest = 70;
					slLogout.HeightRequest = 70;
				}

			}
			catch (Exception ex)
			{

			}
		}
		async void menu_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (!_flag)
					StaticDataModel._CurrentContext.MenuTapped.Execute(StaticDataModel._CurrentContext.MenuTapped);
				else
					Navigation.PopModalAsync();

			}
			catch (Exception ex)
			{


			}
		}
		async void help_Tapped(object sender, System.EventArgs e)
		{
			try
			{
Device.BeginInvokeOnMainThread(() =>

				{
var model = StaticMethods.GetLocalSavedData();
				if (model != null)
				{
						if (model.help_url != null)
							Device.OpenUri(new Uri(model.help_url));
					
				}

				});

			}
			catch (Exception ex)
			{


			}
		}
		async void call_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				Device.BeginInvokeOnMainThread(() =>

				{
var model = StaticMethods.GetLocalSavedData();
				if (model != null)
				{
					if (model.contact_number != null)
					DependencyService.Get<IiOSMethods>().Call(model.contact_number);
					else
						StaticMethods.ShowToast("Contact no not availale!");
				}

				});
			}
			catch (Exception ex)
			{


			}
		}
		async void emailus_Tapped(object sender, System.EventArgs e)
		{
			try
			{


			}
			catch (Exception ex)
			{


			}
		}
		async void review_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				Navigation.PushModalAsync(new ReviewRatingsPage());

			}
			catch (Exception ex)
			{


			}
		}
		async void logout_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				var result = await DisplayAlert("Alert!", "Are you sure, you want to Logout?", "YES", "CANCEL");
				if (result)
				{
					CrossSecureStorage.Current.DeleteKey("userId");
					App.Current.MainPage = new NavigationPage(new LoginPage());
				}

			}
			catch (Exception ex)
			{


			}
		}
		async void changeEmail_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				Navigation.PushModalAsync(new ChangeEmailPage());

			}
			catch (Exception ex)
			{


			}
		}
		async void changePassword_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				Navigation.PushModalAsync(new ChangePasswordPage());

			}
			catch (Exception ex)
			{


			}
		}



	}
}
