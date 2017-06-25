using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

namespace BikeSpot
{
	public partial class CommentReplyPopup : PopupPage
	{
		public CommentReplyPopup()
		{
			InitializeComponent();
		}
		async void submit_Clicked(object sender, System.EventArgs e)
		{
			var msg = txtMsg.Text;
			MessagingCenter.Send<object,string>(this, "CommentReply", msg);
			await Navigation.PopPopupAsync();
		}
	}
}
