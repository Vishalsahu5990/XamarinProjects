using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ChatUsersPage : ContentPage
	{
		ChatUserModel _chatUserModel = null;
		public ChatUsersPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			flowlistview.FlowColumnMinWidth = App.ScreenWidth;
			StaticDataModel._CurrentContext.IsGestureEnabled = false;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			GetAllChatUsers().Wait();

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
		private async Task GetAllChatUsers()
		{
			string ret = string.Empty;

			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_chatUserModel = WebService.GetChatUserList();


					}).ContinueWith(async
					t =>
					{
						try
						{
							Device.BeginInvokeOnMainThread(() =>
					{
						if (_chatUserModel != null)
						{
							for (int i = 0; i < _chatUserModel.data.Count; i++)
							{
								_chatUserModel.data[i].name = _chatUserModel.data[i].name + "-" + _chatUserModel.data[i].product_name;
							}
							flowlistview.FlowItemsSource = _chatUserModel.data;
						}
					});
						}
						catch (Exception ex)
						{

						}




					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
