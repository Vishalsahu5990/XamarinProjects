using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace BikeSpot
{
public class ProductItemsList : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Product> _items;

	public ObservableCollection<Product> Items
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

public ProductItemsList(List<Product> itemList)
	{
		Items = new ObservableCollection<Product>();
		foreach (Product itm in itemList)
		{
			Items.Add(itm);
		}
	}
}
}

