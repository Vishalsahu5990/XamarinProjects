using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace BikeSpot
{
	public partial class CommentsPage : ContentPage
	{
		public static	int _comment_id = 0;
		CommentItemList items = null;
		Product _product = null;
		CommentModel _listComments = null;
		string _userName = string.Empty;
		bool _isProductAdmin = false;
		public CommentsPage(Product product)
		{
			_product = product;
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);



			PrepareUI();

		}
		public CommentsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			txtMsg.Focused += TxtMsg_Focused;
			txtMsg.Unfocused += TxtMsg_Unfocused;
			btnSend.Clicked+= BtnSend_Clicked;
            GetProductComments().Wait();

				MessagingCenter.Subscribe<object, string>(this, "CommentReply", (sender, msg) =>

   {
	Device.BeginInvokeOnMainThread(() =>
	 {
		 
					if (!string.IsNullOrEmpty(msg))
			{
			 if (_comment_id != 0)
			 {
				 CommentReply(msg).Wait();
			 }
			 else
			 {
				 StaticMethods.ShowToast("Unable to send message, Please try again later!");
			 }


			}

	 });
 });
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			txtMsg.Focused -= TxtMsg_Focused;
			txtMsg.Unfocused -= TxtMsg_Unfocused;
			btnSend.Clicked-= BtnSend_Clicked;
		}
		private void PrepareUI()
		{ 
		try
			{
				StaticDataModel._CurrentContext.IsGestureEnabled = false;
				flowlistview.FlowColumnMinWidth = App.ScreenWidth;
			flowlistview.HeightRequest = App.ScreenHeight / 3;
			imgProduct.HeightRequest = App.ScreenHeight / 4;

				imgProduct.Source=	_product.product_image;
				lblProductTitle.Text = _product.product_name;

				if (StaticMethods.IsIpad())
				{
					_slFooter.HeightRequest = 100;
					txtMsg.HeightRequest=80;
					btnSend.HeightRequest = 60;
					btnSend.WidthRequest = 60;
					btnSend.BorderRadius = 30; 
				}
				var model=StaticMethods.GetLocalSavedData();
				if (!string.IsNullOrEmpty(model.name))
					_userName = model.name;

				if (Convert.ToInt32( _product.user_id) == StaticDataModel.userId)
				{
					_isProductAdmin = true;
				}
			}
			catch (Exception ex)
			{

			}
		}

		async void Reply_Clicked(object sender, System.EventArgs e)
		{
			_comment_id = 0;
			var btn = (Xamarin.Forms.Button)sender;
var comment_id = btn.CommandParameter;
			if (comment_id != null)
				_comment_id =Convert.ToInt32( comment_id.ToString());
			await Navigation.PushPopupAsync(new CommentReplyPopup());
		}

		void TxtMsg_Focused(object sender, FocusEventArgs e)
		{
			//flowlistview.HeightRequest = 50;
		}

		void TxtMsg_Unfocused(object sender, FocusEventArgs e)
		{
			//flowlistview.HeightRequest = 300;
		}

		void BtnSend_Clicked(object sender, EventArgs e)
		{
			try
			{
				Device.BeginInvokeOnMainThread(() =>
			{ 
			if (!string.IsNullOrEmpty(txtMsg.Text))
			{
					if (items != null)
					{
						items.Items.Add(new CommentModel.Comment
						{
							name = _userName,
							description = txtMsg.Text
						});
					}
					else
						{
						_listComments = new CommentModel();
						var list = new List<CommentModel.Comment>();
						list.Add(new CommentModel.Comment
						{
							name = _userName,
							description = txtMsg.Text
						});
						_listComments.comments = list;
						items = new CommentItemList(_listComments);
						flowlistview.FlowItemsSource = items.Items;
						}
var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false);

				AddComment(txtMsg.Text);
txtMsg.Text = string.Empty;
			}
			
			});
			}
			catch (Exception ex)
			{

			}


		}

		async void menu_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PopAsync();

			}
			catch (Exception ex)
			{


			}
		}


		private async Task GetProductComments()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_listComments = WebService.GetCommentsList(Convert.ToInt32(_product.product_id));


					}).ContinueWith(async
					t =>
					{
						try
						{
					if (_listComments.comments != null)
							{

								
									if (_listComments.comments != null)
										lblProductTitle.Text = _listComments.comments[0].product_name;
									items = new CommentItemList(_listComments);

									for (int i = 0; i < items.Items.Count; i++)
									{
									if (items.Items[i].user_id == StaticDataModel.userId.ToString())
										_isProductAdmin = false;
							else
								_isProductAdmin = true;
		
												items.Items[i].isProductAdminn = _isProductAdmin;
										var height = items.Items[i].comment_reply.Count * 60 ;
										if (items.Items[i].comment_reply.Count > 0)
										{
											items.Items[i].isVisibleListView = true;
											items.Items[i].ListViewHeight = height;
											for (int j = 0; j < items.Items[i].comment_reply.Count; j++)
											{


												items.Items[i].comment_reply[j].profile_pic = Constants.ProfilePicUrl + items.Items[i].comment_reply[j].profile_pic;
											}
										}
										else
										{
											items.Items[i].isVisibleListView = false;
										}

										items.Items[i].product_image = Constants.ImageUrl + items.Items[i].product_image;
									}
									flowlistview.FlowItemsSource = items.Items;
									if (items.Items.Count > 0)
									{
										var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
										flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false);
									}
								
							}


						
						}
						catch (Exception ex)
						{
						
						}
						finally
						{ 
				StaticMethods.DismissLoader();
				}

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
private async Task AddComment(string msg)
{
			string ret = string.Empty;
	
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				ret = WebService.AddComment(Convert.ToInt32(_product.product_id),msg);


			}).ContinueWith(async
					t =>
			{
				if (ret=="success")
				{
					
				}




			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
private async Task CommentReply(string msg)
{
	string ret = string.Empty;

	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				ret = WebService.CommentReply( msg,_comment_id);


			}).ContinueWith(async
					t =>
			{
				if (ret == "success")
				{
					GetProductComments().Wait();
				}




			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
	}
}
