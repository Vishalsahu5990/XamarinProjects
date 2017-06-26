using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace BikeSpot
{
	public class ChatItemList : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<ChatModel.Datum> _items;

		public ObservableCollection<ChatModel.Datum> Items
		{
			get { return _items; }
			set { _items = value; OnPropertyChanged("Items"); }
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		public ChatItemList(List<ChatModel.Datum> itemList)
		{
			var list = itemList;
			Items = new ObservableCollection<ChatModel.Datum>();
			foreach (ChatModel.Datum itm in list)
			{
				Items.Add(itm);
			}
		}
	}
}
