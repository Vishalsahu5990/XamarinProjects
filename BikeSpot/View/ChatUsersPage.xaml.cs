using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ChatUsersPage : ContentPage
	{
		public ChatUsersPage()
		{
			InitializeComponent();

			listview.FlowColumnMinWidth = App.ScreenWidth;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			listview.FlowItemsSource = Enumerable.Range(0,20).ToList();
		}
	}
}
