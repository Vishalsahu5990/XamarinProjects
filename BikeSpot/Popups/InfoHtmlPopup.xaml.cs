using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class InfoHtmlPopup : PopupPage
	{
		string sourceHtml = string.Empty;
		string _title = string.Empty;
		public InfoHtmlPopup(string source,string title)
		{
			InitializeComponent();
			sourceHtml = source;
			_title = title;
			lblTitle.Text = _title;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			var html = new HtmlWebViewSource
			{
				Html = sourceHtml
			};
			_webview.Source = html;
		}
		async void cross_Tapped(object sender, EventArgs e)
		{

			await Navigation.PopPopupAsync();
		}
	}
}
