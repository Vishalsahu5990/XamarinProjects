using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.CrossPlacePicker;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class FilterPage : ContentPage
	{
		List<Product> _listProduct = null;
		List<string> _typeOfBikes = null;
		List<string> _condition = null;
		List<string> _retails_private = null;
		List<string> _buy_rent = null;
		double _lat = 0;
		double _long = 0;
List<string> _frameSize = null;
List<string> _gender = null;

		string gender = "male";
		int _min_price = 0;
		int _max_price = 0;
		int _max_distance = 0;
		int countMore = 0;
		int countAll = 1;
		int countRoadBikes = 0;
		int countMountainBikes = 0;
		int countHybridBikes = 0;
		int countTouringBikes = 0;
		int countCityBikes = 0;
		int countFoldingBikes = 0;
		int countElectricBikes = 0;
		int countSingleSpeedBikes = 0;
		int countChild = 0;
		int countPedlec = 0;
		int countEquipment = 0;
		int countBmx = 0;
		int countFatbike = 0;
		int countCruiser = 0;
		int countDutch = 0;
		int countTandem = 0;
		int countUnicycle = 0;
		int countNew = 0;
		int countLikeNew = 0;
		int countRidable = 0;
		int countUnridable = 0;
		int countRetailer = 0;
		int countPrivate = 0;
		int countBuy = 0;
		int countRent = 0;
int countS = 0;
int countM = 0;
int countL = 0;
int countXL = 0;
int countMan = 0;
int countWoman = 0;
int countUnisex = 0;

		public FilterPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			///Location initialization
			App.locator = Plugin.Geolocator.CrossGeolocator.Current;



			if (!App.locator.IsListening)
				App.locator.StartListeningAsync(1, 2);
			sliderPrice.Effects.Add(Effect.Resolve("BikeSpot.RangeSliderEffect"));
			sliderDistance.Effects.Add(Effect.Resolve("BikeSpot.RangeSliderEffect"));
			/// Preparing Click events

		}

		async	void location_Tapped(object sender, EventArgs e)
		{

			try
			{
				var result = await CrossPlacePicker.Current.Display();
				if (result != null)
				{
					lblLocation.Text = result.Address;
					_lat=	result.Coordinates.Latitude;
					_long=	result.Coordinates.Longitude;
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", ex.ToString(), "Oops");
			}
		}
		void BtnS_Clicked(object sender, EventArgs e)
		{
			if (countS % 2 == 0)
				btnS.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnS.BackgroundColor = Color.FromHex("#E5E6E7");
                countS++;


		}

		void BtnM_Clicked(object sender, EventArgs e)
		{
			if (countM % 2 == 0)
				btnM.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnM.BackgroundColor = Color.FromHex("#E5E6E7");
                countM++;

		}

		void BtnL_Clicked(object sender, EventArgs e)
		{
			if (countL % 2 == 0)
				btnL.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnL.BackgroundColor = Color.FromHex("#E5E6E7");
                countL++;

		}

		void BtnXl_Clicked(object sender, EventArgs e)
		{
			if (countXL % 2 == 0)
				btnXl.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnXl.BackgroundColor = Color.FromHex("#E5E6E7");
			countXL++;

		}

		void BtnMan_Clicked(object sender, EventArgs e)
		{
			if (countMan % 2 == 0)
				btnMan.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnMan.BackgroundColor = Color.FromHex("#E5E6E7");
			countMan++;
		}

		void BtnWomen_Clicked(object sender, EventArgs e)
		{
			if (countWoman % 2 == 0)
				btnWomen.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnWomen.BackgroundColor = Color.FromHex("#E5E6E7");
			countWoman++;
		}

		void BtnUnisex_Clicked(object sender, EventArgs e)
		{
			if (countUnisex % 2 == 0)
				btnUnisex.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnUnisex.BackgroundColor = Color.FromHex("#E5E6E7");
			countUnisex++;
		}

		void BtnUnicycle_Clicked(object sender, EventArgs e)
		{
			if (countUnicycle % 2 == 0)
				btnUnicycle.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnUnicycle.BackgroundColor = Color.FromHex("#E5E6E7");
			countUnicycle++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnTandem_Clicked(object sender, EventArgs e)
		{
			if (countTandem % 2 == 0)
				btnTandem.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnTandem.BackgroundColor = Color.FromHex("#E5E6E7");
			countTandem++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnDutch_Clicked(object sender, EventArgs e)
		{
			if (countDutch % 2 == 0)
				btnDutch.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnDutch.BackgroundColor = Color.FromHex("#E5E6E7");

			countDutch++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnCruiser_Clicked(object sender, EventArgs e)
		{
			if (countCruiser % 2 == 0)
				btnCruiser.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnCruiser.BackgroundColor = Color.FromHex("#E5E6E7");
			countCruiser++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnFatbike_Clicked(object sender, EventArgs e)
		{
			if (countFatbike % 2 == 0)
				btnFatbike.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnFatbike.BackgroundColor = Color.FromHex("#E5E6E7");
			countFatbike++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnBmx_Clicked(object sender, EventArgs e)
		{
			if (countBmx % 2 == 0)
				btnBmx.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnBmx.BackgroundColor = Color.FromHex("#E5E6E7");
			countBmx++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnEquipment_Clicked(object sender, EventArgs e)
		{
			if (countEquipment % 2 == 0)
				btnEquipment.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnEquipment.BackgroundColor = Color.FromHex("#E5E6E7");
			countEquipment++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnPedlec_Clicked(object sender, EventArgs e)
		{
			if (countPedlec % 2 == 0)
				btnPedlec.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnPedlec.BackgroundColor = Color.FromHex("#E5E6E7");
			countPedlec++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnChildsBike_Clicked(object sender, EventArgs e)
		{
			if (countChild % 2 == 0)
				btnChildsBike.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnChildsBike.BackgroundColor = Color.FromHex("#E5E6E7");
			countChild++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnAll_Clicked(object sender, EventArgs e)
		{
			if (countAll % 2 == 0)
			{
				btnAll.BackgroundColor = Color.FromHex("#20D1C8");

				//setting to default postion to other buttons
				btnRoadBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnMountainBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnHybridBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnTouringBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnCityBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnFoldingBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnElectricBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnSingleSpeedBikes.BackgroundColor = Color.FromHex("#E5E6E7");
				btnChildsBike.BackgroundColor = Color.FromHex("#E5E6E7");
				btnPedlec.BackgroundColor = Color.FromHex("#E5E6E7");
				btnEquipment.BackgroundColor = Color.FromHex("#E5E6E7");
				btnBmx.BackgroundColor = Color.FromHex("#E5E6E7");
				btnFatbike.BackgroundColor = Color.FromHex("#E5E6E7");
				btnCruiser.BackgroundColor = Color.FromHex("#E5E6E7");
				btnDutch.BackgroundColor = Color.FromHex("#E5E6E7");
				btnTandem.BackgroundColor = Color.FromHex("#E5E6E7");
				btnUnicycle.BackgroundColor = Color.FromHex("#E5E6E7");

				countRoadBikes = 0;
				countMountainBikes = 0;
				countHybridBikes = 0;
				countTouringBikes = 0;
				countCityBikes = 0;
				countFoldingBikes = 0;
				countElectricBikes = 0;
				countSingleSpeedBikes = 0;
				countChild = 0;
				countPedlec = 0;
				countEquipment = 0;
				countBmx = 0;
				countFatbike = 0;
				countCruiser = 0;
				countDutch = 0;
				countTandem = 0;
				countUnicycle = 0;

			}
			else
			{
				btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			}

			countAll++;
		}

		async	void BtnMore_Clicked(object sender, EventArgs e)
		{
			if (countMore % 2 == 0)
			{
				gvMoreTypes.IsVisible = true;
				btnMore.Text = "Less";
			}
			else
			{
				gvMoreTypes.IsVisible = false;
				btnMore.Text = "More...";
			}
			countMore++;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			App.locator.PositionChanged -= position_Changed;

			btnRoadBikes.Clicked -= BtnRoadBikes_Clicked;
			btnMountainBikes.Clicked -= BtnMountainBikes_Clicked;
			btnHybridBikes.Clicked -= BtnHybridBikes_Clicked;
			btnTouringBikes.Clicked -= BtnTouringBikes_Clicked;
			btnCityBikes.Clicked -= BtnCityBikes_Clicked;
			btnFoldingBikes.Clicked -= BtnFoldingBikes_Clicked;
			btnElectricBikes.Clicked -= BtnElectricBikes_Clicked;
			btnSingleSpeedBikes.Clicked -= BtnSingleSpeedBikes_Clicked;
			btnAll.Clicked -= BtnAll_Clicked;
			btnChildsBike.Clicked -= BtnChildsBike_Clicked;
			btnPedlec.Clicked -= BtnPedlec_Clicked;
			btnEquipment.Clicked -= BtnEquipment_Clicked;
			btnBmx.Clicked -= BtnBmx_Clicked;
			btnFatbike.Clicked -= BtnFatbike_Clicked;
			btnCruiser.Clicked -= BtnCruiser_Clicked;
			btnDutch.Clicked -= BtnDutch_Clicked;
			btnTandem.Clicked -= BtnTandem_Clicked;
			btnUnicycle.Clicked -= BtnUnicycle_Clicked;


			btnNew.Clicked -= BtnNew_Clicked;
			btnLikeNew.Clicked -= BtnLikeNew_Clicked;
			btnRidable.Clicked -= BtnRidable_Clicked;
			btnUnridable.Clicked -= BtnUnridable_Clicked;

			btnRetailer.Clicked -= BtnRetailer_Clicked;
			btnPrivate.Clicked -= BtnPrivate_Clicked;

			btnBuy.Clicked -= BtnBuy_Clicked;
			btnRent.Clicked -= BtnRent_Clicked;

			btnMore.Clicked -= BtnMore_Clicked;


			sliderPrice.UpperValueChanged -= SliderPrice_UpperValueChanged;
			sliderPrice.LowerValueChanged -= SliderPrice_LowerValueChanged;
			sliderDistance.UpperValueChanged -= SliderDistance_UpperValueChanged;
			sliderDistance.LowerValueChanged -= SliderDistance_LowerValueChanged;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			App.locator.PositionChanged += position_Changed;
			btnRoadBikes.Clicked += BtnRoadBikes_Clicked;
			btnMountainBikes.Clicked += BtnMountainBikes_Clicked;
			btnHybridBikes.Clicked += BtnHybridBikes_Clicked;
			btnTouringBikes.Clicked += BtnTouringBikes_Clicked;
			btnCityBikes.Clicked += BtnCityBikes_Clicked;
			btnFoldingBikes.Clicked += BtnFoldingBikes_Clicked;
			btnElectricBikes.Clicked += BtnElectricBikes_Clicked;
			btnSingleSpeedBikes.Clicked += BtnSingleSpeedBikes_Clicked;
			btnAll.Clicked += BtnAll_Clicked;
			btnChildsBike.Clicked += BtnChildsBike_Clicked;
			btnPedlec.Clicked += BtnPedlec_Clicked;
			btnEquipment.Clicked += BtnEquipment_Clicked;
			btnBmx.Clicked += BtnBmx_Clicked;
			btnFatbike.Clicked += BtnFatbike_Clicked;
			btnCruiser.Clicked += BtnCruiser_Clicked;
			btnDutch.Clicked += BtnDutch_Clicked;
			btnTandem.Clicked += BtnTandem_Clicked;
			btnUnicycle.Clicked += BtnUnicycle_Clicked;

			btnS.Clicked+= BtnS_Clicked;
			btnM.Clicked+= BtnM_Clicked;
			btnL.Clicked+= BtnL_Clicked;
			btnXl.Clicked+= BtnXl_Clicked;

			btnMan.Clicked+= BtnMan_Clicked;
			btnWomen.Clicked+= BtnWomen_Clicked;
			btnUnisex.Clicked+= BtnUnisex_Clicked;


			btnNew.Clicked += BtnNew_Clicked;
			btnLikeNew.Clicked += BtnLikeNew_Clicked;
			btnRidable.Clicked += BtnRidable_Clicked;
			btnUnridable.Clicked += BtnUnridable_Clicked;

			btnRetailer.Clicked += BtnRetailer_Clicked;
			btnPrivate.Clicked += BtnPrivate_Clicked;

			btnBuy.Clicked += BtnBuy_Clicked;
			btnRent.Clicked += BtnRent_Clicked;

			btnMore.Clicked += BtnMore_Clicked;


			sliderPrice.UpperValueChanged += SliderPrice_UpperValueChanged;
			sliderPrice.LowerValueChanged += SliderPrice_LowerValueChanged;
			sliderDistance.UpperValueChanged += SliderDistance_UpperValueChanged;
			sliderDistance.LowerValueChanged += SliderDistance_LowerValueChanged;
			btnAll.BackgroundColor = Color.FromHex("#20D1C8");
			_typeOfBikes = null;

			StaticDataModel._CurrentContext.IsGestureEnabled = false;

			if (StaticMethods.IsIpad())
				PrepareUI();
		}
		private void PrepareUI()
		{ 
		try
			{
				btnAll.HeightRequest = 80;
				btnRoadBikes.HeightRequest = 80;
				btnMountainBikes.HeightRequest = 80;
				btnHybridBikes.HeightRequest = 80;
				btnTouringBikes.HeightRequest = 80;
				btnCityBikes.HeightRequest = 80;
				btnFoldingBikes.HeightRequest = 80;
				btnElectricBikes.HeightRequest = 80;
				btnSingleSpeedBikes.HeightRequest = 80;
				btnChildsBike.HeightRequest = 80;
				btnPedlec.HeightRequest = 80;
				btnEquipment.HeightRequest = 80;
				btnBmx.HeightRequest = 80;
				btnFatbike.HeightRequest = 80;
				btnCruiser.HeightRequest = 80;
				btnDutch.HeightRequest = 80;
				btnTandem.HeightRequest = 80;
				btnUnicycle.HeightRequest = 80;
				btnNew.HeightRequest = 80;
				btnLikeNew.HeightRequest = 80;
				btnRidable.HeightRequest = 80;
				btnUnridable.HeightRequest = 80;
				btnRetailer.HeightRequest = 80;
				btnPrivate.HeightRequest = 80;
				btnBuy.HeightRequest = 80;
				btnRent.HeightRequest = 80;
				slAddress.HeightRequest = 80;
				btnS.HeightRequest = 80;
				btnL.HeightRequest = 80;
				btnM.HeightRequest = 80;
				btnXl.HeightRequest = 80;
				btnMan.HeightRequest = 80;
				btnWomen.HeightRequest = 80;
				btnUnisex.HeightRequest = 80;


			}
			catch (Exception ex)
			{

			}
		}
		void SliderPrice_UpperValueChanged(object sender, EventArgs e)
		{
			lblEndPriceRange.Text = ((RangeSlider)sender).UpperValue.ToString();
		}

		void SliderPrice_LowerValueChanged(object sender, EventArgs e)
		{
			lblStartPriceRange.Text = ((RangeSlider)sender).LowerValue.ToString();

		}

		void SliderDistance_UpperValueChanged(object sender, EventArgs e)
		{
			lblEndDistanceRange.Text = ((RangeSlider)sender).UpperValue.ToString();
		}

		void SliderDistance_LowerValueChanged(object sender, EventArgs e)
		{
			lblStartDistanceRange.Text = ((RangeSlider)sender).LowerValue.ToString();

		}

		void BtnRoadBikes_Clicked(object sender, EventArgs e)
		{


			if (countRoadBikes % 2 == 0)
				btnRoadBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnRoadBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countRoadBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnMountainBikes_Clicked(object sender, EventArgs e)
		{

			if (countMountainBikes % 2 == 0)
				btnMountainBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnMountainBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countMountainBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnHybridBikes_Clicked(object sender, EventArgs e)
		{

			if (countHybridBikes % 2 == 0)
				btnHybridBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnHybridBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countHybridBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnTouringBikes_Clicked(object sender, EventArgs e)
		{

			if (countTouringBikes % 2 == 0)
				btnTouringBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnTouringBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countTouringBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnCityBikes_Clicked(object sender, EventArgs e)
		{

			if (countCityBikes % 2 == 0)
				btnCityBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnCityBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countCityBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnFoldingBikes_Clicked(object sender, EventArgs e)
		{

			if (countFoldingBikes % 2 == 0)
				btnFoldingBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnFoldingBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countFoldingBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnElectricBikes_Clicked(object sender, EventArgs e)
		{

			if (countElectricBikes % 2 == 0)
				btnElectricBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnElectricBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countElectricBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnSingleSpeedBikes_Clicked(object sender, EventArgs e)
		{

			if (countSingleSpeedBikes % 2 == 0)
				btnSingleSpeedBikes.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnSingleSpeedBikes.BackgroundColor = Color.FromHex("#E5E6E7");
			countSingleSpeedBikes++;

			btnAll.BackgroundColor = Color.FromHex("#E5E6E7");
			countAll = 0;
		}

		void BtnNew_Clicked(object sender, EventArgs e)
		{
			if (countNew % 2 == 0)
				btnNew.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnNew.BackgroundColor = Color.FromHex("#E5E6E7");
			countNew++;


		}

		void BtnLikeNew_Clicked(object sender, EventArgs e)
		{
			if (countLikeNew % 2 == 0)
				btnLikeNew.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnLikeNew.BackgroundColor = Color.FromHex("#E5E6E7");
			countLikeNew++;
		}

		void BtnRidable_Clicked(object sender, EventArgs e)
		{
			if (countRidable % 2 == 0)
				btnRidable.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnRidable.BackgroundColor = Color.FromHex("#E5E6E7");
			countRidable++;
		}

		void BtnUnridable_Clicked(object sender, EventArgs e)
		{
			if (countUnridable % 2 == 0)
				btnUnridable.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnUnridable.BackgroundColor = Color.FromHex("#E5E6E7");
			countUnridable++;
		}

		void BtnRetailer_Clicked(object sender, EventArgs e)
		{
			if (countRetailer % 2 == 0)
				btnRetailer.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnRetailer.BackgroundColor = Color.FromHex("#E5E6E7");
			countRetailer++;
		}

		void BtnPrivate_Clicked(object sender, EventArgs e)
		{
			if (countPrivate % 2 == 0)
				btnPrivate.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnPrivate.BackgroundColor = Color.FromHex("#E5E6E7");
			countPrivate++;
		}

		void BtnBuy_Clicked(object sender, EventArgs e)
		{
			if (countBuy % 2 == 0)
				btnBuy.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnBuy.BackgroundColor = Color.FromHex("#E5E6E7");
			countBuy++;
		}

		void BtnRent_Clicked(object sender, EventArgs e)
		{
			if (countRent % 2 == 0)
				btnRent.BackgroundColor = Color.FromHex("#20D1C8");
			else
				btnRent.BackgroundColor = Color.FromHex("#E5E6E7");
			countRent++;
		}

		async void back_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PopAsync();

			}
			catch (Exception ex)
			{


			}
		}
		async void Done_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				if (countAll % 2 != 0)
				{
					_typeOfBikes = new List<string>();
					_typeOfBikes.Add("all");
				}
				else
				{
					PrepareTypeofBike();
				}

				GetProducts().Wait();
		}
			catch (Exception ex)
			{


			}
		}
		// Callback function for when GPS location changes
		void position_Changed(object obj, PositionEventArgs e)
		{

			updateGPSDataList(e.Position);
		}

		// Update GPS location displays and database
		private async void updateGPSDataList(Plugin.Geolocator.Abstractions.Position position)
		{
			Debug.WriteLine("Position changed: " + position.Latitude);
			Debug.WriteLine("Position changed: " + position.Longitude);
			StaticDataModel.Lattitude = position.Latitude;
			StaticDataModel.Longitude = position.Longitude;


		}
		private void PrepareTypeofBike()
		{
			try
			{
				_typeOfBikes = new List<string>();
				_condition = new List<string>();
				_retails_private = new List<string>();
				_buy_rent = new List<string>();
				_gender = new List<string>();
				_frameSize = new List<string>();

				if (countRoadBikes % 2 != 0)
				{
					if (countRoadBikes > 0)
						_typeOfBikes.Add("Road Bikes");
				}
				if (countMountainBikes % 2 != 0)
				{
					if (countMountainBikes > 0)
						_typeOfBikes.Add("Mountain Bikes");
				}
				if (countHybridBikes % 2 != 0)
				{
					if (countHybridBikes > 0)
						_typeOfBikes.Add("Hybrid Bikes");
				}
				if (countTouringBikes % 2 != 0)
				{
					if (countTouringBikes > 0)
						_typeOfBikes.Add("Touring Bikes");
				}
				if (countCityBikes % 2 != 0)
				{
					if (countCityBikes > 0)
						_typeOfBikes.Add("City Bikes");
				}
				if (countFoldingBikes % 2 != 0)
				{
					if (countFoldingBikes > 0)
						_typeOfBikes.Add("Folding Bikes");
				}
				if (countElectricBikes % 2 != 0)
				{
					if (countElectricBikes > 0)
						_typeOfBikes.Add("Electric Bikes");
				}
				if (countSingleSpeedBikes % 2 != 0)
				{
					if (countSingleSpeedBikes > 0)
						_typeOfBikes.Add("Singlespeed Bikes");
				}

				//More...

				if (countChild % 2 != 0)
				{
					if (countChild > 0)
						_typeOfBikes.Add("Child Bike");
				}
				if (countPedlec % 2 != 0)
				{
					if (countPedlec > 0)
						_typeOfBikes.Add("Pedlec");
				}
				if (countEquipment % 2 != 0)
				{
					if (countEquipment > 0)
						_typeOfBikes.Add("Equipment");
				}
				if (countBmx % 2 != 0)
				{
					if (countBmx > 0)
						_typeOfBikes.Add("BMX");
				}
				if (countFatbike % 2 != 0)
				{
					if (countFatbike > 0)
						_typeOfBikes.Add("Fatbike");
				}
				if (countCruiser % 2 != 0)
				{
					if (countCruiser > 0)
						_typeOfBikes.Add("Cruiser");
				}
				if (countDutch % 2 != 0)
				{
					if (countDutch > 0)
						_typeOfBikes.Add("Dutch");
				}
				if (countTandem % 2 != 0)
				{
					if (countTandem > 0)
						_typeOfBikes.Add("Tandem");
				}
				if (countUnicycle % 2 != 0)
				{
					if (countUnicycle > 0)
						_typeOfBikes.Add("Unicycle");
				}



				if (countNew % 2 != 0)
				{
					if (countNew > 0)
						_condition.Add("New");
				}
				if (countLikeNew % 2 != 0)
				{
					if (countLikeNew > 0)
						_condition.Add("Excellent");
				}
				if (countRidable % 2 != 0)
				{
					if (countRidable > 0)
						_condition.Add("Good");
				}
				if (countUnridable % 2 != 0)
				{
					if (countUnridable > 0)
						_condition.Add("Fair");
				}


				if (countRetailer % 2 != 0)
				{
					if (countRetailer > 0)
						_retails_private.Add("Retailer");
				}

				if (countPrivate % 2 != 0)
				{
					if (countPrivate > 0)
						_retails_private.Add("Private");
				}



				if (countBuy % 2 != 0)
				{
					if (countBuy > 0)
						_buy_rent.Add("0");
				}

				if (countRent % 2 != 0)
				{
					if (countRent > 0)
						_buy_rent.Add("1");
				}

				if (countS % 2 != 0)
				{
					if (countS > 0)
						_frameSize.Add("S");
				}
				if (countM % 2 != 0)
				{
					if (countM > 0)
						_frameSize.Add("M");
				}
				if (countL % 2 != 0)
				{
					if (countL > 0)
						_frameSize.Add("L");
				}
				if (countXL % 2 != 0)
				{
					if (countXL > 0)
						_frameSize.Add("XL");
				}

				if (countMan % 2 != 0)
				{
					if (countMan > 0)
						_gender.Add("Man");
				}
				if (countWoman % 2 != 0)
				{
					if (countWoman > 0)
						_gender.Add("Women");
				}
				if (countUnisex % 2 != 0)
				{
					if (countUnisex > 0)
						_gender.Add("Unisex");
				}
				_min_price = Convert.ToInt32(lblStartPriceRange.Text);
				_max_price = Convert.ToInt32(lblEndPriceRange.Text);
				_max_distance = Convert.ToInt32(lblEndDistanceRange.Text.Replace(" KM", ""));

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

						//_listProduct = WebService.FilterProducts(_typeOfBikes,_condition,_buy_rent,
						//                                         Convert.ToInt32(lblStartPriceRange),
						//                                         Convert.ToInt32(lblEndPriceRange),
						//                                         Convert.ToInt32(lblEndDistanceRange),
						//                                         StaticDataModel.Lattitude,
						//                                         StaticDataModel.Longitude);
						_listProduct = WebService.FilterProducts(_typeOfBikes, _condition, _buy_rent,
																 _min_price,
																 _max_price,
																 _max_distance,
				                                                 _gender,
				                                                 _frameSize,
				                                                 _lat,
				                                                 _long
																 );


					}).ContinueWith(async
					t =>
					{
						Device.BeginInvokeOnMainThread(async () =>
						{
							if (_listProduct != null)
							{
								if (_listProduct.Count > 0)
									await Navigation.PushAsync(new HomePage(_listProduct));
								else
									StaticMethods.ShowToast("No products found!");

							}
							else
							{
								StaticMethods.ShowToast("No products found!");
							}
						});


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
