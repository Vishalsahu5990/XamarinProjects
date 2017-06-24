using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class CommentsPage : ContentPage
	{
		CommentItemList items = null;
		Product _product = null;
		CommentModel _listComments = null;
		public CommentsPage(Product product)
		{
			_product = product;
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			txtMsg.Focused += TxtMsg_Focused;
			txtMsg.Unfocused += TxtMsg_Unfocused;
			btnSend.Clicked+= BtnSend_Clicked;

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
            GetProductComments().Wait();
		}
		private void PrepareUI()
		{ 
		try
			{
				flowlistview.FlowColumnMinWidth = App.ScreenWidth;
			flowlistview.HeightRequest = App.ScreenHeight / 3;
			imgProduct.HeightRequest = App.ScreenHeight / 4;

				imgProduct.Source=	_product.product_image;

				if (StaticMethods.IsIpad())
				{
					_slFooter.HeightRequest = 100;
					txtMsg.HeightRequest=80;
					btnSend.HeightRequest = 60;
					btnSend.WidthRequest = 60;
					btnSend.BorderRadius = 30; 
				}
			}
			catch (Exception ex)
			{

			}
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
			if (!string.IsNullOrEmpty(txtMsg.Text))
			{
				//items.Items.Add(new CommentModel 
				//{
				//	name="test",
				//	description=txtMsg.Text
				//});
var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
Device.BeginInvokeOnMainThread(() => flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false));
				AddComment();				
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

		void submit_Clicked(object sender, System.EventArgs e)
		{

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
						if (_listComments != null)
						{
					
							Device.BeginInvokeOnMainThread(async () =>
							{
						lblProductTitle.Text = _listComments.comments[0].product_name;
								items = new CommentItemList(_listComments);

								for (int i = 0; i < items.Items.Count; i++)
								{
									if (items.Items[i].comment_reply.Count > 0)
									{
										for (int j = 0; j < items.Items[i].comment_reply.Count; i++)
										{
											items.Items[i].comment_reply[j].profile_pic = Constants.ProfilePicUrl + items.Items[i].comment_reply[j].profile_pic;
								}
							}
		
											items.Items[i].product_image = Constants.ImageUrl + items.Items[i].product_image;
								}
								flowlistview.FlowItemsSource = items.Items;
								if (items.Items.Count > 0)
								{
									var lastItem = flowlistview.FlowItemsSource.OfType<object>().Last();
									flowlistview.ScrollTo(lastItem, ScrollToPosition.End, false);
								}
							});
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
private async Task AddComment()
{
			string ret = string.Empty;
	
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				ret = WebService.AddComment(Convert.ToInt32(_product.product_id),txtMsg.Text);


			}).ContinueWith(async
					t =>
			{
				if (ret=="success")
				{
					
				}




			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
	}
}
