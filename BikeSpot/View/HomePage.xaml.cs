using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xam.FormsPlugin.Abstractions;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using DLToolkit.Forms.Controls;

namespace BikeSpot
{
	public partial class HomePage : ContentPage
	{
		public ICommand OpenMenuCommand { get; private set; }
		public ICommand OpenProfileCommand { get; private set; }
		public ICommand OpenFAQModalCommand { get; private set; }

		public ICommand ItemTappedCommand { get; set; }
        int countSearch = 0;

		private Product _lastTappedItem;
		public Product LastTappedItem
		{
			get
			{
				return _lastTappedItem;
			}
			set
			{
				_lastTappedItem = value;
			}
		}

		List<Product> _listProduct = null;
		List<Product> _listProductCollection = null;
		ProductItemsList _items = null;
		double itemWidth = 140;
		bool flag = false;
		private int _lastItemAppearedIdx;
		bool isFirstLoad = false;
        bool alreadyLoaded = false;
		public HomePage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			OpenProfileCommand = new Command(() => profile_Tapped(this, new EventArgs()));
			OpenFAQModalCommand = new Command(() => ShowQuestionsAndAnswersPopup());

			flag = false;
			Icon = "menu.png";
			Title = "menu";

			PrepareUI();
			isFirstLoad = true;
            alreadyLoaded = true;


		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
            alreadyLoaded = true;

			flowlistviewViewCellGrid1.FlowColumnMinWidth = App.ScreenWidth / 2;

			flowlistviewViewCellGrid1.FlowItemTapped += (sender, e) =>
				{
					Handle_FlowItemTapped(sender, e);
				};
            searchBar.SearchButtonPressed+= SearchBar_SearchButtonPressed;


			if ((ReferenceEquals(_listProduct, null)) && (isFirstLoad || !flag))
			{
				GetProducts().Wait();
			}
			else if (!ReferenceEquals(_listProduct, null))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					if (flag)
					{
						TransformProducts();
						_items = new ProductItemsList(_listProduct);
						flowlistviewViewCellGrid1.FlowItemsSource = _items.Items;
					}

				});
			}

			btnLoadMore.Clicked += BtnLoadMore_Clicked;
			//flowlistview1.FlowItemAppearing += Flowlistview1_FlowItemAppearing;

			MessagingCenter.Subscribe<object, string>(this, "PopCurrentPage", (sender, msg) =>

   {
	   Device.BeginInvokeOnMainThread(() =>
		{
			App.Current.MainPage = new NavigationPage(new RegistrationPage());


		});
   });

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			isFirstLoad = false;
			btnLoadMore.Clicked -= BtnLoadMore_Clicked;
			//flowlistview1.FlowItemAppearing -= Flowlistview1_FlowItemAppearing;
		}

		public HomePage(List<Product> listProduct)
		{
			InitializeComponent();
			flag = true;
			_listProduct = listProduct;

			ItemTappedCommand = new Command(() => ActOnProductTapped(null));

			Icon = "menu.png";
			Title = "menu";
			NavigationPage.SetHasNavigationBar(this, false);
			PrepareUI();
			isFirstLoad = true;

		}

		void Flowlistview1_FlowItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			//var currentIdx = _items.Items.IndexOf(e.Item as Product);

			//if (currentIdx > _lastItemAppearedIdx)
			//{
			//	System.Diagnostics.Debug.WriteLine("Up");

			//	for (int i = 0; i<_items.Items.Count;i++)
			//		_items.Items[i].isEnableListview = true;
			//}
			//else
			//{
			//	System.Diagnostics.Debug.WriteLine("Down");

			//}

			//_lastItemAppearedIdx = _items.Items.IndexOf(e.Item as Product);
		}

		async void Handle_FlowItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			if (StaticDataModel.IsAnonymousLogin)
			{
				await Navigation.PushPopupAsync(new LoginReminderPopup());
			}
			else
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
					
                    if (alreadyLoaded)
					{
                        alreadyLoaded = false;
						await Navigation.PushAsync(new ProductDetailsPage(item));
					}
				}
			}
		}



		private void PrepareUI()
		{
			try
			{
				//flowlistview1.FlowColumnMinWidth = App.ScreenWidth;
				itemWidth = App.ScreenWidth / 2;


				if (StaticMethods.IsIpad())
				{
					//_slFooter.HeightRequest = 115;

				}


				btnLoadMore.IsVisible = false;
			}
			catch (Exception ex)
			{

			}
		}

		async void spot_Tapped(object sender, System.EventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>

			{
				GetProducts().Wait();
			});


		}
		async void menu_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (StaticDataModel.IsAnonymousLogin)
				{
					await Navigation.PushPopupAsync(new LoginReminderPopup());
				}
				else
				{
					StaticDataModel._CurrentContext.MenuTapped.Execute(StaticDataModel._CurrentContext.MenuTapped);
				}

			}
			catch (Exception ex)
			{


			}
		}


		async void search_Tapped(object sender, System.EventArgs e)
		{
			try
			{
                Device.BeginInvokeOnMainThread(async () =>
                {

                    if (countSearch % 2 == 0)
                    {
                        searchBar.IsVisible = true;
                        searchBar.Focus();
                    }
                    else
                    {

                        searchBar.IsVisible = false;
                        searchBar.Unfocus();
                    }
                    countSearch++;

                });
              
              
			}
			catch (Exception ex)
			{


			}

		}

        void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => 
            {
                var text = searchBar.Text;
                searchBar.IsVisible = false;
                SearchProducts(text);
				
				
            });
        }

        async void filter_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (StaticDataModel.IsAnonymousLogin)
				{
					await Navigation.PushPopupAsync(new LoginReminderPopup());
				}
				else
				{
					await Navigation.PushAsync(new FilterPage());
				}

			}
			catch (Exception ex)
			{


			}
		}
		async void add_bike_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (StaticDataModel.IsAnonymousLogin)
				{
					await Navigation.PushPopupAsync(new LoginReminderPopup());
				}
				else
				{
					await Navigation.PushAsync(new AddProductPage());
				}

			}
			catch (Exception ex)
			{


			}
		}
		async void profile_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (StaticDataModel.IsAnonymousLogin)
				{

				}
				else
				{
					await Navigation.PushModalAsync(new MyProfilePage());
				}

			}
			catch (Exception ex)
			{


			}
		}


		async void ShowQuestionsAndAnswersPopup()
		{
			await Navigation.PushPopupAsync(new QuestionsAndAnswersPopup("FAQ"));
		}

		private void ActOnProductTapped(object param)
		{
			long h = 0;
		}


		public List<Product> list1;
		public bool list1SectionIsVisible = false;
		public List<Product> list2;
		public bool list2SectionIsVisible = false;
		public List<Product> list3;
		public bool list3SectionIsVisible = false;
		public List<Product> list4;
		public bool list4SectionIsVisible = false;
		public List<Product> list5;
		public bool list5SectionIsVisible = false;
		public List<Product> list6;
		public bool list6SectionIsVisible = false;
		private void RebaseProductCollectionsForIOS()
		{
			if (!ReferenceEquals(_listProduct, null))
			{
				list1 = null;
				list2 = null;
				list3 = null;
				list4 = null;
				list5 = null;
				list6 = null;

				double columnWidth = 0;
				double listviewHeight = 0;
				for (int i = 0; i < _listProduct.Count; i++)
				{
					columnWidth = _listProduct[i].columnWidth;
					listviewHeight = _listProduct[i].listviewHeight;
					if (ReferenceEquals(list1, null))
					{
						list1 = new List<Product>();
						list1SectionIsVisible = true;
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list1.Add(_listProduct[i].list[j]);
						}
					}
					else if (ReferenceEquals(list2, null))
					{
						list2 = new List<Product>();
						list2SectionIsVisible = true;
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list2.Add(_listProduct[i].list[j]);
						}
					}
					else if (ReferenceEquals(list3, null))
					{
						list3 = new List<Product>();
						list3SectionIsVisible = true;
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list3.Add(_listProduct[i].list[j]);
						}
					}
					else if (ReferenceEquals(list4, null))
					{
						list4 = new List<Product>();
						list4SectionIsVisible = true;
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list4.Add(_listProduct[i].list[j]);
						}
					}
					else if (ReferenceEquals(list5, null))
					{
						list5 = new List<Product>();
						list5SectionIsVisible = true;
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list5.Add(_listProduct[i].list[j]);
						}
					}
					else if (ReferenceEquals(list6, null))
					{
						list6 = new List<Product>();
						list6SectionIsVisible = true;
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list6.Add(_listProduct[i].list[j]);
						}
					}
					else
					{
						for (int j = 0; j < _listProduct[i].list.Count; j++)
						{
							list6.Add(_listProduct[i].list[j]);
						}
					}
				}

				if (!ReferenceEquals(list1, null))
				{

					if (list1.Count <= 2)
					{
						listviewHeight = itemWidth - 10;
					}
					else if (list1.Count == 4)
					{
						listviewHeight = (itemWidth * 2);
					}
					else
					{
						listviewHeight = (itemWidth * 3) - 30;
					}
					flowlistviewViewCellGrid1.FlowColumnMinWidth = columnWidth;
					flowlistviewViewCellGrid1.HeightRequest = listviewHeight;
					flowlistviewViewCellGrid1.FlowItemsSource = list1;
					flowlistviewViewCellGrid1.IsVisible = true;
					//_firstAd.IsVisible = true;
					flowlistviewViewCellGrid1.IsEnabled = true;
				}

				//TODO
				if (!ReferenceEquals(list2, null))
				{
					if (list2.Count <= 2)
					{
						listviewHeight = itemWidth - 10;
					}
					else if (list2.Count == 4)
					{
						listviewHeight = (itemWidth * 2);
					}
					else
					{
						listviewHeight = (itemWidth * 3) - 30;
					}
					flowlistviewViewCellGrid2.FlowColumnMinWidth = columnWidth;
					flowlistviewViewCellGrid2.HeightRequest = listviewHeight;
					flowlistviewViewCellGrid2.FlowItemsSource = list2;
					flowlistviewViewCellGrid2.IsVisible = true;
					//_secondAd.IsVisible = true;
					flowlistviewViewCellGrid2.IsEnabled = true;
				}
				if (!ReferenceEquals(list3, null))
				{
					if (list3.Count <= 2)
					{
						listviewHeight = itemWidth - 10;
					}
					else if (list3.Count == 4)
					{
						listviewHeight = (itemWidth * 2);
					}
					else
					{
						listviewHeight = (itemWidth * 3) - 30;
					}
					flowlistviewViewCellGrid3.FlowColumnMinWidth = columnWidth;
					flowlistviewViewCellGrid3.HeightRequest = listviewHeight;
					flowlistviewViewCellGrid3.FlowItemsSource = list3;
					flowlistviewViewCellGrid3.IsVisible = true;
					//_thirdAd.IsVisible = true;
					flowlistviewViewCellGrid3.IsEnabled = true;
				}
				if (!ReferenceEquals(list4, null))
				{
					if (list4.Count <= 2)
					{
						listviewHeight = itemWidth - 10;
					}
					else if (list4.Count == 4)
					{
						listviewHeight = (itemWidth * 2);
					}
					else
					{
						listviewHeight = (itemWidth * 3) - 30;
					}
					flowlistviewViewCellGrid4.FlowColumnMinWidth = columnWidth;
					flowlistviewViewCellGrid4.HeightRequest = listviewHeight;
					flowlistviewViewCellGrid4.FlowItemsSource = list4;
					flowlistviewViewCellGrid4.IsVisible = true;
					//_fourthyAd.IsVisible = true;
					flowlistviewViewCellGrid4.IsEnabled = true;
				}
				if (!ReferenceEquals(list5, null))
				{
					if (list5.Count <= 2)
					{
						listviewHeight = itemWidth - 10;
					}
					else if (list5.Count == 4)
					{
						listviewHeight = (itemWidth * 2);
					}
					else
					{
						listviewHeight = (itemWidth * 3) - 30;
					}
					flowlistviewViewCellGrid5.FlowColumnMinWidth = columnWidth;
					flowlistviewViewCellGrid5.HeightRequest = listviewHeight;
					flowlistviewViewCellGrid5.FlowItemsSource = list5;
					flowlistviewViewCellGrid5.IsVisible = true;
					//_fiftyAd.IsVisible = true;
					flowlistviewViewCellGrid5.IsEnabled = true;
				}
				if (!ReferenceEquals(list6, null))
				{
					if (list6.Count <= 2)
					{
						listviewHeight = itemWidth - 10;
					}
					else if (list6.Count == 4)
					{
						listviewHeight = (itemWidth * 2);
					}
					else
					{
						listviewHeight = (itemWidth * 3) - 30;
					}
					flowlistviewViewCellGrid6.FlowColumnMinWidth = columnWidth;
					flowlistviewViewCellGrid6.HeightRequest = listviewHeight;
					flowlistviewViewCellGrid6.FlowItemsSource = list6;
					flowlistviewViewCellGrid6.IsVisible = true;
					//_sixtyAd.IsVisible = true;
					flowlistviewViewCellGrid6.IsEnabled = true;
				}

			}
		}

		private void RebaseProductsForIOS()
		{
			List<Product> products = new List<Product>();
			for (int i = 0; i < _listProduct.Count; i++)
			{
				products.Concat(_listProduct[i].list).ToList();
				for (int j = 0; j < _listProduct[i].list.Count; j++)
				{
					products.Add(_listProduct[i].list[j]);
				}
			}
			_listProduct = products;
			flowlistviewViewCellGrid2.IsVisible = false;
			flowlistviewViewCellGrid3.IsVisible = false;
			flowlistviewViewCellGrid4.IsVisible = false;
			flowlistviewViewCellGrid5.IsVisible = false;
			flowlistviewViewCellGrid6.IsVisible = false;
			flowlistviewViewCellGrid1.FlowItemsSource = _listProduct;

		}

		private void TransformProducts()
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
							_listProduct[i].listviewHeight = (itemWidth * 2);
						}
						else
						{

							_listProduct[i].listviewHeight = (itemWidth * 3) - 30;
						}
						_listProduct[i].ProductTappedCommand = new Command((obj) => ActOnProductTapped(obj));
						_listProduct[i].list[j].ProductTappedCommand = new Command((obj) => ActOnProductTapped(obj));
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
							_listProduct[i].list[j].price = "€ " + _listProduct[i].list[j].price;
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
				RebaseProductsForIOS();
				//RebaseProductCollectionsForIOS();
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
						long h = 0;

					}).ContinueWith(async
					t =>
					{
						try
						{
							TransformProducts();
							_items = new ProductItemsList(_listProduct);
							//flowlistview1.FlowItemsSource = _items.Items;
							//_items.Items[0].isEnableListview = true;

						}
						catch (Exception ex)
						{

						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task SearchProducts( string searchText)
		{

			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

                _listProduct = WebService.SearchProduct(searchText);
						long h = 0;

					}).ContinueWith(async
                    t =>
                    {
                    try
                    {
                        if (!ReferenceEquals(_listProduct, null))
                        {
                        if(_listProduct.Count>0)
                        {

									TransformProducts();
									_items = new ProductItemsList(_listProduct);
                        }
								else
								{
									DisplayAlert("Alert!", "No bike found!", "Ok");


								} 
                           
                        }
                        else
                        {
                        DisplayAlert("Alert!","No bike found!","Ok");
                           

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
