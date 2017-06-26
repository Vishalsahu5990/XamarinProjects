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
		OfferStatusModel _offerstatusModel = null;
		int optionCount = 0;
		string fromPage = "";
		bool isFirstLoad = false;
		bool isFromLocal = false;
		string offer_status = string.Empty;
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
			offer_status = userModel.status;
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
			SetOfferStatus(offer_status);
		}

		void BtnYes_Clicked(object sender, EventArgs e)
		{
			offer_status = "5";
			UpdateOfferStatus().Wait();
		}

		async void BtnNo_Clicked(object sender, EventArgs e)
		{
			var result = await DisplayAlert("", "Do you really want to cancel the payment?", "YES", "NO");
			if (result)
			{
				offer_status = "0";
				UpdateOfferStatus().Wait();
			}
			else
			{ 
			
			}
		}

		void BtnMakeOffer_Clicked(object sender, EventArgs e)
		{
			offer_status = "1";
			UpdateOfferStatus().Wait();
		}

		void BtnAccept_Clicked(object sender, EventArgs e)
		{
			offer_status = "3";
            UpdateOfferStatus().Wait();
		}

		void BtnRefuse_Clicked(object sender, EventArgs e)
		{
			offer_status = "2";
            UpdateOfferStatus().Wait();
		}

		void BtnOk_Clicked(object sender, EventArgs e)
		{
			offer_status = "0";
            UpdateOfferStatus().Wait();
		}

		void BtnCash_Clicked(object sender, EventArgs e)
		{
			offer_status = "4";
            UpdateOfferStatus().Wait();
		}

		void BtnOnline_Clicked(object sender, EventArgs e)
		{
			// uncomment this section when payment success 
			//offer_status = "4";
       //         UpdateOfferStatus().Wait(); 
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
			txtMsg.Focused += TxtMsg_Focused;
			txtMsg.Unfocused += TxtMsg_Unfocused;
			txtMsg.Completed += TxtMsg_Completed;
			btnYes.Clicked+= BtnYes_Clicked;
			btnNo.Clicked+= BtnNo_Clicked;
			btnAccept.Clicked+= BtnAccept_Clicked;
			btnRefuse.Clicked+= BtnRefuse_Clicked;
			btnOk.Clicked+= BtnOk_Clicked;
			btnCash.Clicked+= BtnCash_Clicked;
			btnOnline.Clicked+= BtnOnline_Clicked;
			btnMakeOffer.Clicked+= BtnMakeOffer_Clicked;

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

			MessagingCenter.Subscribe<object, string>(this, "NotificationRecieved", (sender, msg) =>
			{
				Device.BeginInvokeOnMainThread(() =>
				 {



				 });
			});

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			txtMsg.Focused -= TxtMsg_Focused;
			txtMsg.Unfocused -= TxtMsg_Unfocused;
			txtMsg.Completed -= TxtMsg_Completed;

			btnYes.Clicked-= BtnYes_Clicked;
			btnNo.Clicked-= BtnNo_Clicked;
			btnAccept.Clicked-= BtnAccept_Clicked;
			btnRefuse.Clicked-= BtnRefuse_Clicked;
			btnOk.Clicked-= BtnOk_Clicked;
			btnCash.Clicked-= BtnCash_Clicked;
			btnOnline.Clicked-= BtnOnline_Clicked;
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
					if (StaticDataModel.userId.ToString().Equals(_productModel.user_id))
					{
						if (offer_status.Equals("0"))
						{
							StaticMethods.ShowToast("No Offer available.");
						}
						else
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
						}
					}
					else
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
					}

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

				//Circle 
				var width = App.ScreenWidth / 4;
				var radious = width / 2;

				btnCash.HeightRequest = width;
				btnCash.WidthRequest = width;
				btnCash.BorderRadius = (int)radious;

				btnprice.HeightRequest = width;
				btnprice.WidthRequest = width;
				btnprice.BorderRadius = (int)radious;

				btnOnline.HeightRequest = width;
				btnOnline.WidthRequest = width;
				btnOnline.BorderRadius = (int)radious;
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
									offer_status = _chatUserModel.offer_status.ToString();
									SetOfferStatus(offer_status);
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
private async Task UpdateOfferStatus()
{
	

	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				_offerstatusModel = WebService.SaveOfferStatus(Convert.ToInt32(_productModel.user_id), Convert.ToInt32(_productModel.product_id),Convert.ToDouble( lblPrice.Text),offer_status);


			}).ContinueWith(async
					t =>
			{
				try
				{
					if (_offerstatusModel!=null)
					{
						SetOfferStatus(offer_status);

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
		private void SetOfferStatus(string status)
		{
				//Restet all top views
				slMakeOfferPriceView.IsVisible = false;
				slLabelToastMessages.IsVisible = false;
				slAcceptRefuse.IsVisible = false;
				slYesNo.IsVisible = false;
				slOK.IsVisible = false;
				sLPayment.IsVisible = false;
			try
			{

				if (Convert.ToString(StaticDataModel.userId).Equals(_productModel.user_id))
				{
					switch (status)
					{
						case "0":
							offer_status = "0";
							lblStatusMessage.Text = "No Offer Available";
							slLabelToastMessages.IsVisible = true;
							break;
						case "1":
							slLabelToastMessages.IsVisible = false;
							slAcceptRefuse.IsVisible = true;
							break;
                case "2":
							lblStatusMessage.Text="Offer is refuse " + lblPrice.Text + ".";
							slLabelToastMessages.IsVisible = true; 
                  

                    break;
                case "3":
							slLabelToastMessages.IsVisible = true;
							lblStatusMessage.Text="Offer accepted " + lblPrice.Text + ".";
							btnAccepted.BackgroundColor = Color.FromHex("#36C2B5");			
                    break;
                case "4":
                    slLabelToastMessages.IsVisible = true;
					lblStatusMessage.Text="Did you recieve your money?";
					slYesNo.IsVisible = true;
					btnAccepted.BackgroundColor = Color.FromHex("#36C2B5");	
                    break;
                case "5"://paid
							slMakeOfferPriceView.IsVisible = false;
							slLabelToastMessages.IsVisible = false;
							btnPaid.BackgroundColor = Color.FromHex("#36C2B5");
							btnMakeOffer1.BackgroundColor=Color.FromHex("#36C2B5");
							btnAccepted.BackgroundColor=Color.FromHex("#36C2B5");
							////Yes, call webservice change status
							/// No, Popup>
///  Yes, call webservice change status
							/// No,Popup dispose 
							/// 


                    break;
                case "6": // No
							lblStatusMessage.Text = "Payment not received.";
							slLabelToastMessages.IsVisible = true;	
                  
                    break;
					}
				}
				else
				{
					switch (status)
					{
						case "0":  // offer not made yet (default)
							offer_status = "0";
							slMakeOfferPriceView.IsVisible = true;

							break;
						case "1": // Make offer
							lblStatusMessage.Text= "Your Offer is pending " + lblPrice.Text + ".";
							slLabelToastMessages.IsVisible = true;
							break;
						case "2": // Offer refuse
							lblStatusMessage.Text = "Your Offer is refuse " + lblPrice.Text + ".";
							slLabelToastMessages.IsVisible = true;
							slOK.IsVisible = true;
							break;
						case "3": // Accepted
							slMakeOfferPriceView.IsVisible = false;
							sLPayment.IsVisible = true;
							break;
						case "4": // Cash or online
							
							sLPayment.IsVisible = false;
							lblStatusMessage.Text= "Pending transaction " + lblPrice.Text + ".";
							slLabelToastMessages.IsVisible = true;
							break;
						case "5": // Paid
							slMakeOfferPriceView.IsVisible = false;
							slLabelToastMessages.IsVisible = false;
							btnPaid.BackgroundColor = Color.FromHex("#36C2B5");
							btnMakeOffer1.BackgroundColor=Color.FromHex("#36C2B5");
							btnAccepted.BackgroundColor=Color.FromHex("#36C2B5");
							///open review popup on payment success 
							break;
						case "6": // No
							///
							offer_status = "0";
							slMakeOfferPriceView.IsVisible = true;
							break;
					}
				}

			}
			catch (Exception ex)
			{

			}
		}


	}
}



//String myId = PreferenceConnector.readString(ChatScreen.this, PreferenceConnector.USER_ID, "";
//        if (adminId.equals(myId)) {
//            switch (status) {
//                case "0":
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("No Offer available.";
//                    break;
//                case "1":
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setOfferReceivedStatus(true);
//                    break;
//                case "2":
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Offer is refuse " + offerPrice + ".";
//                    break;
//                case "3":
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Offer accepted " + offerPrice + ".";
//                    break;
//                case "4":
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setReceiveMoneyStatus(true);
//                    break;
//                case "5"://paid
//                    activityBinding.priceTxt.setEnabled(false);
//                    break;
//                case "6": // No
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Payment not received.";
//                    break;
//            }
//        } else {
//            switch (status) {
//                case "0":  // offer not made yet (default)
//                    activityBinding.priceTxt.setEnabled(true);
//                    activityBinding.getStatus().setMakeOfferStatus(true);
//                    break;
//                case "1": // Make offer
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Your Offer is pending " + offerPrice + ".";
//                    break;
//                case "2": // Offer refuse
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Your Offer is refuse " + offerPrice + ".";
//                    break;
//                case "3": // Accepted
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setPaymentOptionStatus(true);
//                    break;
//                case "4": // Cash or online
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Pending transaction " + offerPrice + ".";
//                    break;
//                case "5": // Paid
//                    activityBinding.priceTxt.setEnabled(false);
//                    break;
//                case "6": // No
//                    activityBinding.priceTxt.setEnabled(false);
//                    activityBinding.getStatus().setShowStatusTextView(true);
//activityBinding.getStatus().setStatus("Payment not received.";
//                    break;
//            }
//        }

					