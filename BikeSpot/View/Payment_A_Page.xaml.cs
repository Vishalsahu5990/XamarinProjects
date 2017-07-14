using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayPal.Forms;
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
            productModel.product_owner_id = model.product_owner_id;
            productModel.user_id = model.user_id;
			productModel.product_name = model.product_name;
			var data = model.product_image;
			if (!string.IsNullOrEmpty(data))
			{
				var imageArray = data.Split(',');
				productModel.product_image = Constants.ImageUrl + imageArray[0];
			}
			if (_productModel != null)
			{
				_productModel = productModel;
			}
			else
			{
			_productModel = new Product(); 
			_productModel = productModel; 
			} 
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

		async void BtnOnline_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Message","It will work in next version.","OK");
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
			btnYes.Clicked += BtnYes_Clicked;
			btnNo.Clicked += BtnNo_Clicked;
			btnAccept.Clicked += BtnAccept_Clicked;
			btnRefuse.Clicked += BtnRefuse_Clicked;
			btnOk.Clicked += BtnOk_Clicked;
			btnCash.Clicked += BtnCash_Clicked;
			btnOnline.Clicked += BtnOnline_Clicked;
			btnMakeOffer.Clicked += BtnMakeOffer_Clicked;

			txtMsg.Focused += (sender, e) =>
			{
				//flowlistviewOverlay.IsVisible = true;
			};
			txtMsg.Unfocused += (sender, e) =>
			{
				//flowlistviewOverlay.IsVisible = false;
			};

			TapGestureRecognizer flowlistviewTapGesture = new TapGestureRecognizer();
			flowlistviewTapGesture.Command = new Command(() =>
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					txtMsg.Unfocus();
                    ChatEntry.keepOpen = false;
				});
			});
			flowlistview.GestureRecognizers.Add(flowlistviewTapGesture);
			stackContent.GestureRecognizers.Add(flowlistviewTapGesture);
			//stackScrollContent.GestureRecognizers.Add(flowlistviewTapGesture);
			//flowlistviewOverlay.GestureRecognizers.Add(flowlistviewTapGesture);

            flowlistview.FlowItemTapped += (sender, e) =>
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					txtMsg.Unfocus();
                    ChatEntry.keepOpen = false;
				});
			};

			if (fromPage == "make_offer")
			{
				if (isFirstLoad)
				{
					await SaveChatUser();
				}
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

			MessagingCenter.Subscribe<object, ChatModel.Datum>(this, "NotificationRecieved", (sender, model) =>
			{
				Device.BeginInvokeOnMainThread(() =>
                 {

                    if (!ReferenceEquals(model,null))
                    {
                        if(string.Equals( model.type,"chat"))
                        {
                            model.Incoming = true;
                            if(!string.IsNullOrEmpty(model.profile_pic))
                             model.profile_pic = Constants.ProfilePicUrl + model.profile_pic;
							 var t1 = Convert.ToDateTime(model.created_at);
                             if (!isFromLocal)
							 {
								 t1 = DateTime.SpecifyKind(t1, DateTimeKind.Utc);
								 t1 = t1.ToLocalTime();
							 }
							 model.MessageTime = StaticMethods.TimeAgo(Convert.ToDateTime(t1));
							 if (items != null)
							 {
								 //var time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

                                items.Items.Add(model);
							 }
							 else
							 {
								 _chatModel = new ChatModel();
								 var list = new List<ChatModel.Datum>();
                                list.Add(model);
								 _chatModel.data = list;
								 items = new ChatItemList(_chatModel.data);
								 flowlistview.FlowItemsSource = items.Items;
							 }
							 var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
							 flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false);

							

						 }
                        else
                        {
                             offer_status = model.status;
							 UpdateOfferStatus().Wait();

                        }
                         lblPrice.Text = model.offer_price;
                    }

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
				Device.BeginInvokeOnMainThread(() =>
				{
					txtMsg.Unfocus();
				});
				await Navigation.PopModalAsync();

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
								var height = App.ScreenHeight / 1.4;
								flowlistview.HeightRequest = height - 100;

								_slChatLayout.HeightRequest = height - 100;
								_sclChatContent.HeightRequest = height;
								
							}
							else
							{
								imgTopBackgroud.IsVisible = false;

								var height = App.ScreenHeight / 3.4;
								_slChatLayout.HeightRequest = App.ScreenHeight - height;
								flowlistview.HeightRequest = App.ScreenHeight - height;
								_sclChatContent.HeightRequest = height;

								
							}

							optionCount++;
						}
					}
					else
					{
						if (optionCount % 2 == 0)
						{
							imgTopBackgroud.IsVisible = true;
							//flowlistview.HeightRequest = App.ScreenWidth / 1.3;
							var height = App.ScreenHeight / 1.4;
							_slChatLayout.HeightRequest = height - 100;
							_sclChatContent.HeightRequest = height;
							flowlistview.HeightRequest = height - 100;
						}
						else
						{

							imgTopBackgroud.IsVisible = false;
							//flowlistview.HeightRequest = App.ScreenHeight - 130;
							var height = App.ScreenHeight - 60;

							_slChatLayout.HeightRequest = height - 100;
							_sclChatContent.HeightRequest = height;
							flowlistview.HeightRequest = App.ScreenHeight - height;

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
                flowlistview.FlowColumnMinWidth = App.ScreenWidth;
				string lblPriceText = lblPrice.Text;
				if (!ReferenceEquals(_productModel.price, null) && _productModel.price != string.Empty)
				{
					lblPriceText = _productModel.price.Replace("€", string.Empty);
					lblPriceText = lblPriceText.Replace(" ", string.Empty);
				}
				lblPrice.Text = lblPriceText;
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
				//flowlistview.HeightRequest = App.ScreenHeight - 140;
				var height = App.ScreenHeight - 60;

				_slChatLayout.HeightRequest = height - 100;
				_sclChatContent.HeightRequest = height;

				flowlistview.HeightRequest = height-100; 

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
									if (_chatUserModel.msg == string.Empty)
									{
										_productModel.price = _productModel.price.Replace(" ", string.Empty);
										_productModel.price = _productModel.price.Replace("€", string.Empty);
										SendMessage("Offer posted price € " + _productModel.price, Convert.ToInt32(_productModel.price)).Wait();
									}
								}
								else
								{
									offer_status = "0";
									if (_chatUserModel.msg == string.Empty)
									{
										_productModel.price = _productModel.price.Replace(" ", string.Empty);
										_productModel.price = _productModel.price.Replace("€", string.Empty);
                                SendMessage("Offer posted price € " + _productModel.price, Convert.ToDouble(_productModel.price)).Wait();
									}
								}
                        		SetOfferStatus(offer_status);
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

			try
			{
				string lblPriceText = lblPrice.Text;
				lblPriceText = lblPriceText.Replace(" ", string.Empty);
				lblPriceText = lblPriceText.Replace("€", string.Empty);
				_offerstatusModel = WebService.SaveOfferStatus(Convert.ToInt32(_productModel.user_id), Convert.ToInt32(_productModel.product_id), Convert.ToDouble(lblPriceText), offer_status);
				
                if (string.Equals(_offerstatusModel.result,"success"))
				{
					SetOfferStatus(offer_status);

				}
                else
                {
                    StaticMethods.ShowToast(_offerstatusModel.responseMessage);
                }
			}
			catch (Exception ex)
			{
			}

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

				if (Convert.ToString(StaticDataModel.userId).Equals(_productModel.product_owner_id))
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
							lblStatusMessage.Text="Offer is refuse " + lblPrice.Text + "€.";
							slLabelToastMessages.IsVisible = true; 
                  

                    break;
                case "3":
							slLabelToastMessages.IsVisible = true;
							lblStatusMessage.Text="Offer accepted " + lblPrice.Text + "€.";
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
							imgTopBackgroud.IsVisible = true;
							slMakeOfferPriceView.IsVisible = true;

							break;
						case "1": // Make offer
							lblStatusMessage.Text= "Your Offer is pending " + lblPrice.Text + "€.";
							slLabelToastMessages.IsVisible = true;
							break;
						case "2": // Offer refuse
							lblStatusMessage.Text = "Your Offer is refuse " + lblPrice.Text + "€.";
							slLabelToastMessages.IsVisible = true;
							slOK.IsVisible = true;
							break;
						case "3": // Accepted
							slMakeOfferPriceView.IsVisible = false;
							sLPayment.IsVisible = true;
							break;
						case "4": // Cash or online
							
							sLPayment.IsVisible = false;
							lblStatusMessage.Text= "Pending transaction " + lblPrice.Text + "€.";
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
							imgTopBackgroud.IsVisible = true;
							slMakeOfferPriceView.IsVisible = true;
							break;
					}
				}

			}
			catch (Exception ex)
			{

			}
		}
        private void PaymentProcess()
        {

            try
            {
                CrossPayPalManager.Current.RequestFuturePayments();
            }
            catch (Exception ex)
            {

            }
        }
       
	}
}

/*
   * init paypal library
   
public void initLibrary()
{
	PayPal pp = PayPal.getInstance();
	if (pp == null)
	{  // Test to see if the library is already initialized

		// This main initialization call takes your Context, AppID, and target server
		pp = PayPal.initWithAppID(this, "APP-80W284485P519543T", PayPal.ENV_SANDBOX);
		// Required settings:
		// Set the language for the library
		pp.setLanguage("en_US";
		// Some Optional settings:
		// Sets who pays any transaction fees. Value is:
		// FEEPAYER_SENDER, FEEPAYER_PRIMARYRECEIVER, FEEPAYER_EACHRECEIVER, and FEEPAYER_SECONDARYONLY
		pp.setFeesPayer(PayPal.FEEPAYER_EACHRECEIVER);
		// true = transaction requires shipping
		pp.setShippingEnabled(false);
	}
}

     * Paypal payment method
     *
     * @param amount
     * @param currency
     * @param sellerId
    
    public void onBuyPressed(String amount, String currency, String sellerId)
{
	// Create a basic PayPal payment
	PayPalPayment payment = new PayPalPayment();
	// Set the currency type
	payment.setCurrencyType(currency);
	// Set the recipient for the payment (can be a phone number)
	payment.setRecipient(sellerId);
	// Set the payment amount, excluding tax and shipping costs
	payment.setSubtotal(new BigDecimal(amount));
	// Set the payment type--his can be PAYMENT_TYPE_GOODS,
	// PAYMENT_TYPE_SERVICE, PAYMENT_TYPE_PERSONAL, or PAYMENT_TYPE_NONE
	payment.setPaymentType(PayPal.PAYMENT_TYPE_GOODS);

	PayPal payPal = PayPal.getInstance();
	if (payPal != null)
	{
		// Use checkout to create our Intent.
		Intent checkoutIntent = payPal
				.checkout(payment, this /*, new ResultDelegate());
		// Use the android's startActivityForResult() and pass in our
		// Intent.
		// This will start the library.
		startActivityForResult(checkoutIntent, 1011);
		// Set the tax amount
		//        invoice.setTax(new BigDecimal(_taxAmount));

	}
	else
	{
		Utility.showMsg(ChatScreen.this, getString(R.string.alert), getString(R.string.wrong));
	}
}
*/