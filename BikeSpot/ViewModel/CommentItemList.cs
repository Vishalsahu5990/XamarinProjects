using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace BikeSpot
{
public class CommentItemList : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<CommentModel.Comment> _items;

		public ObservableCollection<CommentModel.Comment> Items
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

public CommentItemList(CommentModel itemList)
	{
			var list = itemList.comments;
			Items = new ObservableCollection<CommentModel.Comment>();
			foreach (CommentModel.Comment itm in list)
		{
			Items.Add(itm);
		}
	}
}
}
