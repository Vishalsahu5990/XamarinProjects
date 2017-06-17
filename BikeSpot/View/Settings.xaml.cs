using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BikeSpot
{
	public partial class Settings : ContentPage
	{
		public Settings()
		{
			InitializeComponent();
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
				StaticDataModel._CurrentContext.MenuTapped.Execute(StaticDataModel._CurrentContext.MenuTapped);

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
				App.Current.MainPage = new NavigationPage(new LoginPage());

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
