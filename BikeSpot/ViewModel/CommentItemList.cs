using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace BikeSpot
{
public class CommentItemList : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<CommentModel> _items;

	public ObservableCollection<CommentModel> Items
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

public CommentItemList(List<CommentModel> itemList)
	{
		Items = new ObservableCollection<CommentModel>();
		foreach (CommentModel itm in itemList)
		{
			Items.Add(itm);
		}
	}
}
}
