using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class TestPage : ContentPage
	{
		ProductItemsList _items = null;
		List<Product> _listProduct = null;
		double itemWidth = 140;
		public TestPage()
		{
			InitializeComponent();
//flowlistviewViewCellGrid.FlowColumnMinWidth = App.ScreenWidth;
			itemWidth = App.ScreenWidth / 2;


		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			OnAppearing();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			GetProducts().Wait();
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
									_listProduct[i].listviewHeight = itemWidth-10;
										}
										else if (_listProduct[i].list.Count == 4)
										{ 
									_listProduct[i].listviewHeight = (itemWidth*2)-20;
								}
								else 
										{ 
									_listProduct[i].listviewHeight = (itemWidth*3)-30;
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
											_listProduct[i].list[j].imageHeight = itemWidth-20;
									         _listProduct[i].list[j].width = itemWidth - 15;
									     _listProduct[i].list[j].borderColor = "Silver";
										}
									}
								}
								_items = new ProductItemsList(_listProduct);
flowlistviewViewCellGrid.ItemsSource = _items.Items;
							}
						}
						catch (Exception ex)
						{

						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
