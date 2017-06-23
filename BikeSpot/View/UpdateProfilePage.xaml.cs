using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class UpdateProfilePage : ContentPage
	{
		ProfileModel _profileModel = null;
		Plugin.Media.Abstractions.MediaFile profileData = null;
		public UpdateProfilePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			txtName.Focused += TxtName_Focused;
			txtEmail.Focused += TxtEmail_Focused;
			txtMobile.Focused += TxtMobile_Focused;
			txtWebUrl.Focused += TxtWebUrl_Focused;
			txtName.Unfocused += TxtName_Unfocused;
			txtEmail.Unfocused += TxtEmail_Unfocused;
			txtMobile.Unfocused += TxtMobile_Unfocused;
			txtWebUrl.Unfocused += TxtWebUrl_Unfocused;
			btnUpdate.Clicked += BtnUpdate_Clicked;
			GetProfile().Wait();

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			txtName.Focused -= TxtName_Focused;
			txtEmail.Focused -= TxtEmail_Focused;
			txtMobile.Focused -= TxtMobile_Focused;
			txtWebUrl.Focused -= TxtWebUrl_Focused;
			txtName.Unfocused -= TxtName_Unfocused;
			txtEmail.Unfocused -= TxtEmail_Unfocused;
			txtMobile.Unfocused -= TxtMobile_Unfocused;
			txtWebUrl.Unfocused -= TxtWebUrl_Unfocused;
			btnUpdate.Clicked -= BtnUpdate_Clicked;
		}

		void TxtName_Focused(object sender, FocusEventArgs e)
		{
			bxName.HeightRequest = 2;
			bxName.BackgroundColor = Color.Gray;
		}

		void TxtEmail_Focused(object sender, FocusEventArgs e)
		{
			bxEmail.HeightRequest = 2;
			bxEmail.BackgroundColor = Color.Gray;
		}

		void TxtMobile_Focused(object sender, FocusEventArgs e)
		{
			bxMobile.HeightRequest = 2;
			bxMobile.BackgroundColor = Color.Gray;
		}

		void TxtWebUrl_Focused(object sender, FocusEventArgs e)
		{
			bxWebUrl.HeightRequest = 2;
			bxWebUrl.BackgroundColor = Color.Gray;
		}

		void TxtName_Unfocused(object sender, FocusEventArgs e)
		{
			bxName.HeightRequest = 1;
			bxName.BackgroundColor = Color.Silver;
		}

		void TxtEmail_Unfocused(object sender, FocusEventArgs e)
		{
			bxEmail.HeightRequest = 1;
			bxEmail.BackgroundColor = Color.Silver;
		}

		void TxtMobile_Unfocused(object sender, FocusEventArgs e)
		{
			bxMobile.HeightRequest = 1;
			bxMobile.BackgroundColor = Color.Silver;
		}

		void TxtWebUrl_Unfocused(object sender, FocusEventArgs e)
		{
			bxWebUrl.HeightRequest = 1;
			bxWebUrl.BackgroundColor = Color.Silver;
		}

		async void back_Tapped(object sender, System.EventArgs e)
		{

			await Navigation.PopModalAsync();


		}
		async void camera_Tapped(object sender, System.EventArgs e)
		{

			TakeImage();
		}

		void BtnUpdate_Clicked(object sender, EventArgs e)
		{
			if (IsValidate())
			{
				var model = new ProfileModel.Datum();
				model.name = txtName.Text;
				model.email = txtEmail.Text;
				model.mobile_no = txtMobile.Text;
				model.website_url = txtWebUrl.Text;
				UpdateProfile(model);
			}
			else
			{
				DisplayAlert("Alert", "All fields are required!", "OK");
			}

		}
		private bool IsValidate()
		{
			if (string.IsNullOrEmpty(txtName.Text))
			{
				txtName.TextColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(txtEmail.Text))
			{
				txtEmail.TextColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(txtMobile.Text))
			{
				txtMobile.TextColor = Color.Red;
				return false;
			}
			else if (string.IsNullOrEmpty(txtWebUrl.Text))
			{
				txtWebUrl.TextColor = Color.Red;
				return false;
			}
			else
			{
				return true;
			}
		}
		private void PrepareUI()
		{
			try
			{
				var imageSize = App.ScreenWidth / 3;
				imgProfile.HeightRequest = imageSize;
				imgProfile.WidthRequest = imageSize;
				_rlTopBackgroud.HeightRequest = imageSize;
				_rlTopBackgroud.WidthRequest = imageSize;
			}
			catch (Exception ex)
			{

			}
		}
		private async void TakeImage()
		{
			var action = await DisplayActionSheet("Choose", "Cancel", null, "Camera", "Photo Library");
			Debug.WriteLine(action);
			if (action == "Camera")
			{
				Device.BeginInvokeOnMainThread(() =>
				   {
					   FromCamera();
				   });

			}
			else if (action == "Photo Library")
			{
				Device.BeginInvokeOnMainThread(() =>
				   {
					   FromLibrary();
				   });


			}
		}
		private async void FromCamera()
		{
			try
			{


				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
					return;
				}
				profileData = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
				});


				if (profileData == null)
					return;

				imgProfile.Source = ImageSource.FromStream(() =>
{
	var stream = profileData.GetStream();

	//file.Dispose();
	return stream;
});

				var bytearray = StaticMethods.StreamToByte(profileData.GetStream());
				var base64 = Convert.ToBase64String(bytearray);
				var extsn = Path.GetExtension(profileData.Path);
				var str = extsn.Split('.');
				extsn = str[1];
				UpdateProfilePic(base64, extsn);
			}
			catch (Exception ex)
			{

			}
		}
		private async void FromLibrary()
		{
			try
			{

				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
					return;
				}
				profileData = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
				{
					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
				});


				if (profileData == null)
					return;

				imgProfile.Source = ImageSource.FromStream(() =>
{
	var stream = profileData.GetStream();

	//file.Dispose();
	return stream;
});
				var bytearray = StaticMethods.StreamToByte(profileData.GetStream());
				var base64 = Convert.ToBase64String(bytearray);
				var extsn = Path.GetExtension(profileData.Path);
				var str = extsn.Split('.');
				extsn = str[1];
				UpdateProfilePic(base64, extsn);


			}
			catch (Exception ex)
			{

			}
		}
		private async Task GetProfile()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_profileModel = WebService.GetProfile(StaticDataModel.userId);


					}).ContinueWith(async
					t =>
					{
						try
						{
							if (_profileModel != null)
							{
								Device.BeginInvokeOnMainThread(async () =>
						{
							txtName.Text = _profileModel.data[0].name;
							txtEmail.Text = _profileModel.data[0].email;
							txtMobile.Text = _profileModel.data[0].contact_number;
							txtWebUrl.Text = _profileModel.data[0].website_url;
							imgProfile.Source = Constants.ProfilePicUrl + _profileModel.data[0].profile_pic;
							if (!string.IsNullOrEmpty(_profileModel.data[0].ratings))
							{

								var val = Math.Round(Convert.ToDouble(_profileModel.data[0].ratings), 3);
								//lblRating.Text = val.ToString();
							}

						});
							}
						}
						catch (Exception ex)
						{

						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task UpdateProfilePic(string profile_pic, string ext)
		{


			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						WebService.UpdateProfilePic(profile_pic, ext);


					}).ContinueWith(async
					t =>
					{





					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task UpdateProfile(ProfileModel.Datum model)
		{
			string ret = string.Empty;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						ret = WebService.UpdateProfile(model);


					}).ContinueWith(async
					t =>
					{

						if (ret == "success")
						{
							StaticMethods.ShowToast("Profile Updated successfully!");
							Navigation.PopModalAsync();
						}
						else
						{
							StaticMethods.ShowToast("Failed to update profile!");
						}



						StaticMethods.DismissLoader();
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
