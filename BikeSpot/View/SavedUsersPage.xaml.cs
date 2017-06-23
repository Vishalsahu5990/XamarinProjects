using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSpot
{
	public partial class SavedUsersPage : ContentPage
	{
		public SavedUsersPage()
		{
			InitializeComponent();
			flowlistview.FlowColumnMinWidth = App.ScreenWidth;
		}
		async void backTapped(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync(true);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			GetAllSavedUsers().Wait();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
private async Task GetAllSavedUsers()
{
			SavedUserModel model = null;
	StaticMethods.ShowLoader();
	Task.Factory.StartNew(

			() =>
			{

				model = WebService.GetSavedUser();


			}).ContinueWith(async
					t =>
			{
				if (model != null)
				{
					Device.BeginInvokeOnMainThread(async () =>
					{
						for (int i = 0; i < model.data.Count; i++)
						{
							if (!string.IsNullOrEmpty(model.data[i].profile_pic))
								model.data[i].profile_pic = Constants.ProfilePicUrl + model.data[i].profile_pic;
							else
								model.data[i].profile_pic = "dummyprofile.png";
						}
						flowlistview.FlowItemsSource = model.data;
					});
				}


				StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
	}
}
