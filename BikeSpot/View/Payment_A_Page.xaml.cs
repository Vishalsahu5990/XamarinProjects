using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class Payment_A_Page : ContentPage
	{
		Product _productModel = null;
		ChatItemList items = null;
		ChatModel _chatModel = null;
		SaveChatUserModel _chatUserModel = null;
		int optionCount = 0;
		string fromPage = "";
		bool isFirstLoad = false;
		bool isFromLocal=false;
		public Payment_A_Page()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		public Payment_A_Page(Product productModel)
		{
			isFirstLoad = true;
			fromPage = "make_offer";
			InitializeComponent();
			_productModel = productModel;
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		public Payment_A_Page(Product productModel, string method)
		{
			isFirstLoad = true;
			fromPage = method;
			InitializeComponent();
			_productModel = productModel;
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}
		public Payment_A_Page(ChatUserModel.Datum userModel)
		{
			isFirstLoad = true;
			fromPage = "user_list";
			InitializeComponent();
			var model = userModel;
			var productModel = new Product();
			productModel.name = model.name;
			productModel.product_id = model.product_id;
			productModel.name = model.product_image;
			productModel.price = model.offer_price;
			productModel.user_id = model.user_id;
			productModel.product_name = model.product_name;
			var data = model.product_image;
			if (!string.IsNullOrEmpty(data))
			{

				var imageArray = data.Split(',');
				productModel.product_image = Constants.ImageUrl + imageArray[0];
			}
			_productModel = productModel;
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
		}

		void TxtMsg_Focused(object sender, FocusEventArgs e)
		{
			ChatEntry.keepOpen = true;
		}

		void TxtMsg_Unfocused(object sender, FocusEventArgs e)
		{
			ChatEntry.keepOpen = false;
		}

		void TxtMsg_Completed(object sender, EventArgs e)
		{
			isFromLocal = true;
			try
			{
				Device.BeginInvokeOnMainThread(() =>
			{
				if (fromPage == "private_chat")
				{
					if (_chatModel.data != null)
					{
                            SendProcess();	
					}
					else
					{
							SaveChatUser().Wait();
					
					}
				}
				else
				{
					SendProcess();
				}

			});
			}
			catch (Exception ex)
			{

			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			txtMsg.Focused+= TxtMsg_Focused;
			txtMsg.Unfocused+= TxtMsg_Unfocused;
			txtMsg.Completed+= TxtMsg_Completed;

			if (fromPage == "make_offer")
			{
				if (isFirstLoad)
					SaveChatUser();
			}
			else if (fromPage == "user_list")
			{
				if (isFirstLoad)
					GetAllChatMessages().Wait();
			}
			else if (fromPage == "private_chat")
			{ 
			if (isFirstLoad)
                    GetAllChatMessages().Wait(); 
			}

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
				txtMsg.Focused-= TxtMsg_Focused;
			txtMsg.Unfocused-= TxtMsg_Unfocused;
			txtMsg.Completed-= TxtMsg_Completed; 
			ChatEntry.keepOpen = false;

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
			isFromLocal = true;
			try
			{
				Device.BeginInvokeOnMainThread(() =>
			{
				if (fromPage == "private_chat")
				{
					if (_chatModel.data != null)
					{
                            SendProcess();	
					}
					else
					{
							SaveChatUser().Wait();
					
					}
				}
				else
				{
					SendProcess();
				}

			});
			}
			catch (Exception ex)
			{

			}



		}
		private void SendProcess()
		{ 
		try
			{
				if (!string.IsNullOrEmpty(txtMsg.Text))
				{
					var msgTime = StaticMethods.TimeAgo(DateTime.Now);
					if (items != null)
					{
						//var time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

						items.Items.Add(new ChatModel.Datum
						{
							name = _productModel.name,
							message = txtMsg.Text,
							Incoming = false,
							Outgoing = true,
							MessageTime = msgTime
						});
					}
					else
					{
						_chatModel = new ChatModel();
var list = new List<ChatModel.Datum>();
list.Add(new ChatModel.Datum
						{
							name = _productModel.name,
							message = txtMsg.Text,
							Incoming = false,
							Outgoing = true,
							MessageTime = msgTime
						});
						_chatModel.data = list;
						items = new ChatItemList(_chatModel.data);
flowlistview.FlowItemsSource = items.Items;
					}
					var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false);

					SendMessage(txtMsg.Text, Convert.ToDouble(_productModel.price)).Wait();
txtMsg.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{

			}
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
		private async Task SaveChatUser()
		{
			string ret = string.Empty;

			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_chatUserModel = WebService.SaveChatUser(Convert.ToInt32(_productModel.user_id), Convert.ToInt32(_productModel.product_id));


					}).ContinueWith(async
					t =>
					{
						try
						{
							if (_chatUserModel != null)
							{
								if (_chatUserModel.result == "success")
								{
									if (_chatUserModel.msg == string.Empty)
									{
										SendMessage("Offer posted price € " + _productModel.price, Convert.ToInt32(_productModel.price)).Wait();
									}
								}


							}
						}
						catch (Exception ex)
						{

						}
						finally
						{
							if (fromPage == "private_chat")
								SendProcess();
								else
									GetAllChatMessages().Wait();
						}

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task SendMessage(string msg, double offer_price)
		{
			string ret = string.Empty;

			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						ret = WebService.SaveChatMessage(Convert.ToInt32(_productModel.user_id), Convert.ToInt32(_productModel.product_id), msg, offer_price);


					}).ContinueWith(async
					t =>
					{
						try
						{
							if (!string.IsNullOrEmpty(ret))
							{


							}
						}
						catch (Exception ex)
						{

						}

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetAllChatMessages()
		{
			string ret = string.Empty;

			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_chatModel = WebService.GetAllChatMessage(Convert.ToInt32(_productModel.user_id), Convert.ToInt32(_productModel.product_id));


					}).ContinueWith(async
					t =>
					{
						try
						{
							if (_chatModel.data != null)
							{

								for (int i = 0; i < _chatModel.data.Count; i++)
								{
									if (!string.IsNullOrEmpty(_chatModel.data[i].profile_pic))
									{
										_chatModel.data[i].profile_pic = Constants.ProfilePicUrl + _chatModel.data[i].profile_pic;
									}
									else
									{
										_chatModel.data[i].profile_pic = "dummyprofile.png";
									}
									if (_chatModel.data[i].sender_id == StaticDataModel.userId.ToString())
									{
										_chatModel.data[i].Outgoing = true;
										_chatModel.data[i].Incoming = false;
									}
									else
									{
										_chatModel.data[i].Outgoing = false;
										_chatModel.data[i].Incoming = true;
									}
									if (!string.IsNullOrEmpty(_chatModel.data[i].created_at))
									{
										var t1 = Convert.ToDateTime(_chatModel.data[i].created_at);

										if (!isFromLocal)
										{
											t1 = DateTime.SpecifyKind(t1, DateTimeKind.Utc);
											t1 = t1.ToLocalTime();
										}
								_chatModel.data[i].MessageTime = StaticMethods.TimeAgo(Convert.ToDateTime(t1));

									}
								}

								items = new ChatItemList(_chatModel.data);
								flowlistview.FlowItemsSource = items.Items;
								var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
								flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false);
							}
						}
						catch (Exception ex)
						{

						}

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}


	}
}
