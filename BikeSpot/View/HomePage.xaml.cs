using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xam.FormsPlugin.Abstractions;
using System.Threading.Tasks;

namespace BikeSpot
{
	public partial class HomePage : ContentPage
	{
		public ICommand OpenMenuCommand { get; private set; }
		List<Product> _listProduct = null;
		ProductItemsList _items = null;
		double itemWidth = 140;
		bool flag = false;
		private int _lastItemAppearedIdx;
		public HomePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			flag = false;
			Icon = "menu.png";
			Title = "menu";

			PrepareUI();


		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if (!flag)
				GetProducts().Wait();
			btnLoadMore.Clicked += BtnLoadMore_Clicked;
			flowlistview1.FlowItemAppearing += Flowlistview1_FlowItemAppearing;

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			btnLoadMore.Clicked -= BtnLoadMore_Clicked;
			flowlistview1.FlowItemAppearing -= Flowlistview1_FlowItemAppearing;
		}

		public HomePage(List<Product> listProduct)
		{
			InitializeComponent();
			flag = true;
			_listProduct = listProduct;

			Icon = "menu.png";
			Title = "menu";
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();


		}

		void Flowlistview1_FlowItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			var currentIdx = _items.Items.IndexOf(e.Item as Product);

			if (currentIdx > _lastItemAppearedIdx)
			{
				System.Diagnostics.Debug.WriteLine("Up");
				_items.Items[0].isEnableListview = true;
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Down");
			}

			_lastItemAppearedIdx = _items.Items.IndexOf(e.Item as Product);
		}

		async void Handle_FlowItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var item = e.Item as Product;
			if (item.listing_type == "advertisement") 
			{
				_items.Items[0].isEnableListview = false;
				if (!string.IsNullOrEmpty(item.redirect_url))
					Device.OpenUri(new Uri(item.redirect_url));
			}
			else
			{
				await Navigation.PushAsync(new ProductDetailsPage(item));
			}
		}



		private void PrepareUI()
		{
			try
			{
				flowlistview1.FlowColumnMinWidth = App.ScreenWidth;
				itemWidth = App.ScreenWidth / 2;


				if (StaticMethods.IsIpad())
				{
					_slFooter.HeightRequest = 115;

				}


				btnLoadMore.IsVisible = false;
			}
			catch (Exception ex)
			{

			}
		}
		async void menu_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				StaticDataModel._CurrentContext.MenuTapped.Execute(StaticDataModel._CurrentContext.MenuTapped);

			}
			catch (Exception ex)
			{


			}
		}
		async void filter_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PushAsync(new FilterPage());

			}
			catch (Exception ex)
			{


			}
		}
		async void add_bike_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PushAsync(new AddProductPage());

			}
			catch (Exception ex)
			{


			}
		}
		async void profile_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PushModalAsync(new MyProfilePage());

			}
			catch (Exception ex)
			{


			}
		}
		private async Task GetProducts()
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						_listProduct = WebService.GetAllProductList();


					}).ContinueWith(async
					t =>
					{
						try
						{
							string imgData = string.Empty;
							if (_listProduct != null)
							{
								for (int i = 0; i < _listProduct.Count; i++)
								{
									_listProduct[i].columnWidth = itemWidth;

									for (int j = 0; j < _listProduct[i].list.Count; j++)
									{
										if (_listProduct[i].list.Count <= 2)
										{
											_listProduct[i].listviewHeight = itemWidth - 10;
										}
										else if (_listProduct[i].list.Count == 4)
										{
											_listProduct[i].listviewHeight = (itemWidth * 2) - 20;
										}
										else
										{
											_listProduct[i].listviewHeight = (itemWidth * 3) - 30;
										}

										if (_listProduct[i].list[j].listing_type == "product")
										{
											if (_listProduct[i].list[j].add_to_top == "1")
											{
												_listProduct[i].list[j].isTopEnable = true;
												_listProduct[i].list[j].borderColor = "Red";
											}
											else
											{
												_listProduct[i].list[j].isTopEnable = false;
												_listProduct[i].list[j].borderColor = "Silver";
											}
											if (!string.IsNullOrEmpty(_listProduct[i].list[j].product_image))
											{
												imgData = _listProduct[i].list[j].product_image;

											}
											_listProduct[i].list[j].width = itemWidth - 15;
											_listProduct[i].list[j].imageHeight = itemWidth - 50;
											var array = imgData.Split(',');
											if (array != null)
											{
												_listProduct[i].list[j].product_image = Constants.ImageUrl + array[0];
											}
										}
										else
										{
											_listProduct[i].list[j].product_image = _listProduct[i].list[j].advertisement_img;
											_listProduct[i].list[j].imageHeight = itemWidth - 20;
											_listProduct[i].list[j].width = itemWidth - 15;
											_listProduct[i].list[j].borderColor = "Silver";
										}
									}
								}
								_items = new ProductItemsList(_listProduct);
								flowlistview1.FlowItemsSource = _items.Items;
						         _items.Items[0].isEnableListview = true;
							}
						}
						catch (Exception ex)
						{

						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}

		async void BtnLoadMore_Clicked(object sender, EventArgs e)
		{
			GetProducts().Wait();
		}


	}
}
