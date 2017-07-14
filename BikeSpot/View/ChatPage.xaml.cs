using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ChatPage : ContentPage
	{
		
List<ChatModel> cm = new List<ChatModel>();
		public ChatPage()
		{
			
			InitializeComponent();
			//listview.HeightRequest = App.ScreenHeight - 150;

			LoadPreviousChat();
		}
private void LoadPreviousChat()
{
	try
	{
		for (int i = 0; i < 10; i++)
		{
			bool incm = false;
			bool outg = false;
			if (i % 2 == 0)
				incm = true;
			else
				outg = true;

			cm.Add(new ChatModel
			{
				Id = i,
				Message = "Hello..., Testing chat",
				Incoming = incm,
				Outgoing = outg

			});
		}

		var items = cm;
				listview.FlowItemsSource = items;
		//		var lastItem = listview.ItemsSource.<object>().Last();
		//listView.ScrollTo(lastItem, ScrollToPosition.End, false);
	}
	catch (Exception ex)
	{

	}
		}
		public void Send_Clicked(object sender,EventArgs e)
		{ 
		
		}
	}
}
