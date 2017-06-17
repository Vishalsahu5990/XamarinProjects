using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class SelectTypeOfBikePopup : PopupPage
	{
		public static  event EventHandler<int> ItemSelected;
		public ICommand NotifyDataChangedCommand { get; private set; }
		List<TypeofBikeModel> _list = null;
		public List<WrappedSelection<TypeofBikeModel>> _WrappedItems;
		public WrappedSelection<TypeofBikeModel> o;
		public static List<string> list = null;
		int _fromTab = 0;
		public SelectTypeOfBikePopup(int fromTab)
		{
			_fromTab = fromTab;
			InitializeComponent();
			flowlistview.FlowItemTapped += Flowlistview_FlowItemTapped;


		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
            PrepareStaticList();
		}
		private void PrepareStaticList()
		{

			_list = new List<TypeofBikeModel>();
			_list.Add(new TypeofBikeModel { id = 1, name = "Road Bikes" });
			_list.Add(new TypeofBikeModel { id = 2, name = "Mountain Bikes" });
			_list.Add(new TypeofBikeModel { id = 3, name = "Hybrid Bikes" });
			_list.Add(new TypeofBikeModel { id = 4, name = "Touring Bikes" });
			_list.Add(new TypeofBikeModel { id = 5, name = "City Bikes" });
			_list.Add(new TypeofBikeModel { id = 6, name = "Folding Bikes" });
			_list.Add(new TypeofBikeModel { id = 7, name = "Electric Bikes" });
			_list.Add(new TypeofBikeModel { id = 8, name = "Singlespeed Bikes" });



			_WrappedItems = _list.Select(item => new WrappedSelection<TypeofBikeModel>() { Item = item, IsSelected = false }).ToList();

			flowlistview.FlowItemsSource = _WrappedItems;
		}
		void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
		{
			try
			{
				///To clear previous selection
				foreach (var wi in _WrappedItems)
				{
					wi.IsSelected = false;
					wi.IsVisible = true;
				}


				o = (WrappedSelection<TypeofBikeModel>)e.Item;
				//var o = (WrappedSelection<List< AddtoPlaylistModel>>)e.Item;

				o.IsSelected = !o.IsSelected;
				o.IsVisible = !o.IsVisible;
			}
			catch (Exception ex)
			{


			}
		}
		async void close_Tapped(object sender, EventArgs e)
		{
			list = null;
			await Navigation.PopPopupAsync();
		}
		async void cancel_clicked(object sender, EventArgs e)
		{
			list = null;
			await Navigation.PopPopupAsync();
		}
		async void ok_clicked(object sender, EventArgs e)
		{
			list = new List<string>();
			foreach (var wi in _WrappedItems)
			{
				if (wi.IsSelected)
				{
					list.Add(wi.Item.name);
				}
			}

			ItemSelected(this,_fromTab);
			await Navigation.PopPopupAsync();
			}
		}
	}
