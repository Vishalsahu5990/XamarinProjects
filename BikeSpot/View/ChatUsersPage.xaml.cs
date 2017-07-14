using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ChatUsersPage : ContentPage
	{
bool isFirstLoad = false;
		ChatUserModel _chatUserModel = null;
		public ChatUsersPage()
		{
			isFirstLoad = true;
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			flowlistview.FlowColumnMinWidth = App.ScreenWidth;
			//StaticDataModel._CurrentContext.IsGestureEnabled = false;
		}
		protected override void OnAppearing()
		{
			isFirstLoad = true;
			base.OnAppearing();
			GetAllChatUsers().Wait();
			flowlistview.FlowItemTapped+= Flowlistview_FlowItemTapped;

		}
protected override void OnDisappearing()
{
			isFirstLoad = false;
			StaticDataModel.IsFromNavigationMenu = false;
	}
		async void back_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				//if(!StaticDataModel.IsFromNavigationMenu)
    //           await Navigation.PopAsync();
				//else
                await Navigation.PopModalAsync();

			}
			catch (Exception ex)
			{

              
			}
		}

		 void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (isFirstLoad)
			{
				isFirstLoad = false;
				var item = e.Item as ChatUserModel.Datum;
				if (item != null)
				Navigation.PushModalAsync(new Payment_A_Page(item));
				else
					StaticMethods.ShowToast("Unable to get detais. Please try again later!");
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
							if (!ReferenceEquals(_chatUserModel.data, null))
							{
								for (int i = 0; i < _chatUserModel.data.Count; i++)
								{
									_chatUserModel.data[i].name = _chatUserModel.data[i].name + "-" + _chatUserModel.data[i].product_name;
									var date_time = _chatUserModel.data[i].last_chating_time;
									if (!string.IsNullOrEmpty(date_time))
									{
										var time = Convert.ToDateTime(date_time);
										var _time = time.ToString("hh:mm tt");
										_chatUserModel.data[i].last_chating_time = _time;
									}
								}
								flowlistview.FlowItemsSource = _chatUserModel.data;
							}
							else
							{
								
								Device.BeginInvokeOnMainThread(() =>
								{
									StaticMethods.ShowToast("No User model data");
								});
							}

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
