using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using NControl.Controls;
using Plugin.CrossPlacePicker;
using Plugin.CrossPlacePicker.Abstractions;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace BikeSpot
{
	public interface IPlacePicker
	{
		Task<Place> PickAsync();
	}

	public class Place
	{
		public string Name
		{
			get;
			set;
		}
	}
		public partial class AddProductPage : ContentPage
		{

			int fromImageView = 1;
			int imageFillCounter1 = 0;
			int imageFillCounter2 = 0;
			int imageFillCounter3 = 0;
			int imageFillCounter4 = 0;

			Plugin.Media.Abstractions.MediaFile profileData = null;

			Plugin.Media.Abstractions.MediaFile _file1 = null;
			Plugin.Media.Abstractions.MediaFile _file2 = null;
			Plugin.Media.Abstractions.MediaFile _file3 = null;
			Plugin.Media.Abstractions.MediaFile _file4 = null;
			public bool _isItemSelected = false;
			public bool _isItemSelectedRent = false;
			int countTopSell = 0;
			int countTopRent = 0;
			bool switchsellValue = false;
			bool switchrentValue = false;
			string gender = "male";
			double headerSize = 0;
			double cellSize = 0;
			public AddProductPage()
			{
				InitializeComponent();
				CrossMedia.Current.Initialize().Wait();
				NavigationPage.SetHasNavigationBar(this, false);


				btnSell.Clicked += BtnSell_Clicked;
				btnRent.Clicked += BtnRent_Clicked;
				pickerTypeofBike_sell.SelectedIndexChanged += PickerTypeofBike_Sell_SelectedIndexChanged;
				pickerInr_sell.SelectedIndexChanged += PickerInr_Sell_SelectedIndexChanged;
				pickerPerDayHour.SelectedIndexChanged += PickerPerDayHour_SelectedIndexChanged;
				pickerCondition_sell.SelectedIndexChanged += PickerCondition_Sell_SelectedIndexChanged;

				pickerTypeofBike_rent.SelectedIndexChanged += PickerTypeofBike_Rent_SelectedIndexChanged;
				pickerCondition_rent.SelectedIndexChanged += PickerCondition_Rent_SelectedIndexChanged;
				pickerInr_rent.SelectedIndexChanged += PickerInr_Rent_SelectedIndexChanged;

				btnSellit.Clicked += BtnSellit_Clicked;
				btnRentit.Clicked += BtnRentit_Clicked;

				switch_sell.Toggled += Switch_Sell_Toggled;
				switch_rent.Toggled += Switch_Rent_Toggled;


				

				btnMale_sell.Clicked += BtnMale_Sell_Clicked;
				btnfeMale_sell.Clicked += BtnfeMale_Sell_Clicked;
				btnother_sell.Clicked += Btnother_Sell_Clicked;

				
				PrepaireUI();



				SelectTypeOfBikePopup.ItemSelected += (sender, e) =>
				{
					Device.BeginInvokeOnMainThread(async () =>
												   {

													   if (e == 0)
													   {
														   if (SelectTypeOfBikePopup.list != null)
														   {


															   lblTypeofbike_sekk.Text = string.Join(",", SelectTypeOfBikePopup.list.ToArray());
															   lblTypeofbike_sekk.TextColor = Color.Black;


														   }
													   }
													   else
													   {
														   if (SelectTypeOfBikePopup.list != null)
														   {


															   lblTypeofbike_rent.Text = string.Join(",", SelectTypeOfBikePopup.list.ToArray());
															   lblTypeofbike_rent.TextColor = Color.Black;


														   }
													   }
												   }
												  );
				};

			}
			public void img1Tapped(object sender, EventArgs e)
			{
				fromImageView = 1;
				if (imageFillCounter1 == 1)
					ChooseAction();
				else
					TakeImage();
			}
			public void img2Tapped(object sender, EventArgs e)
			{
				fromImageView = 2;
				if (imageFillCounter2 == 2)
					ChooseAction();
				else
					TakeImage();
			}
			public void img3Tapped(object sender, EventArgs e)
			{
				fromImageView = 3;
				if (imageFillCounter3 == 3)
					ChooseAction();
				else
					TakeImage();
			}
			public void img4Tapped(object sender, EventArgs e)
			{
				fromImageView = 4;
				if (imageFillCounter4 == 4)
					ChooseAction();
				else
					TakeImage();
			}
			private void SetImage(int flag, Plugin.Media.Abstractions.MediaFile profileData)
			{
				try
				{
					switch (flag)
					{
						case 1:
							img1.Source = ImageSource.FromStream(() =>
	{
		var stream = profileData.GetStream();

		//file.Dispose();
		return stream;
	});
							_file1 = profileData;
							imageFillCounter1 = 1;
							break;
						case 2:
							img2.Source = ImageSource.FromStream(() =>
	{
		var stream = profileData.GetStream();

		//file.Dispose();
		return stream;
	});
							_file2 = profileData;
							imageFillCounter2 = 2;
							break;
						case 3:
							img3.Source = ImageSource.FromStream(() =>
	{
		var stream = profileData.GetStream();

		//file.Dispose();
		return stream;
	});
							_file3 = profileData;
							imageFillCounter3 = 3;
							break;
						case 4:
							img4.Source = ImageSource.FromStream(() =>
	{
		var stream = profileData.GetStream();

		//file.Dispose();
		return stream;
	});
							_file4 = profileData;
							imageFillCounter4 = 4;
							break;

					}
				}
				catch (Exception ex)
				{

				}
			}
			private void RemoveImage(int flag)
			{
				try
				{
					switch (flag)
					{
						case 1:
							img1.Source = "camera";
							imageFillCounter1 = 0;
							_file1 = null;
							break;
						case 2:
							img2.Source = "camera";
							imageFillCounter2 = 0;
							_file2 = null;
							break;
						case 3:

							img3.Source = "camera";
							imageFillCounter3 = 0;
							_file3 = null;
							break;
						case 4:
							img4.Source = "camera";
							imageFillCounter4 = 0;
							_file4 = null;
							break;

					}
				}
				catch (Exception ex)
				{

				}
			}
			protected override void OnAppearing()
			{
				base.OnAppearing();
				SetData();
				fromImageView = 1;
				imageFillCounter1 = 0;
				imageFillCounter2 = 0;
				imageFillCounter3 = 0;
				imageFillCounter4 = 0;
			}
			protected override void OnDisappearing()
			{
				base.OnDisappearing();
				StaticDataModel.IsFromSell = true;

				btnSell.Clicked -= BtnSell_Clicked;
				btnRent.Clicked -= BtnRent_Clicked;
				pickerTypeofBike_sell.SelectedIndexChanged -= PickerTypeofBike_Sell_SelectedIndexChanged;
				pickerInr_sell.SelectedIndexChanged -= PickerInr_Sell_SelectedIndexChanged;
				pickerPerDayHour.SelectedIndexChanged -= PickerPerDayHour_SelectedIndexChanged;
				pickerCondition_sell.SelectedIndexChanged -= PickerCondition_Sell_SelectedIndexChanged;

				pickerTypeofBike_rent.SelectedIndexChanged -= PickerTypeofBike_Rent_SelectedIndexChanged;
				pickerCondition_rent.SelectedIndexChanged -= PickerCondition_Rent_SelectedIndexChanged;
				pickerInr_rent.SelectedIndexChanged -= PickerInr_Rent_SelectedIndexChanged;

				btnSellit.Clicked -= BtnSellit_Clicked;
				btnRentit.Clicked -= BtnRentit_Clicked;

				switch_sell.Toggled -= Switch_Sell_Toggled;
				switch_rent.Toggled -= Switch_Rent_Toggled;


				btnMale_sell.Clicked -= BtnMale_Sell_Clicked;
				btnfeMale_sell.Clicked -= BtnfeMale_Sell_Clicked;
				btnother_sell.Clicked -= Btnother_Sell_Clicked;



			}
			

			

			

			async void menu_Tapped(object sender, System.EventArgs e)
			{
				try
				{
				//StaticDataModel._CurrentContext.MenuTapped.Execute(StaticDataModel._CurrentContext.MenuTapped);
				await	Navigation.PopAsync();
				}
				catch (Exception ex)
				{


				}
			}

		async void address_rentTapped(object sender, EventArgs e)
		{
			
			 try
          {
              var result = await CrossPlacePicker.Current.Display();
              if (result != null)
              {
					lblAddress_rent.Text = result.Address;
					lblAddress_rent.TextColor = Color.Black;
				}
          }
          catch (Exception ex)
          {
              await DisplayAlert("Error", ex.ToString(), "Oops");
          }
		}

			void Switch_Sell_Toggled(object sender, ToggledEventArgs e)
			{
				switchsellValue = e.Value;
			}

			void Switch_Rent_Toggled(object sender, ToggledEventArgs e)
			{
				switchrentValue = e.Value;
			}

			private void SetData()
			{
				try
				{
					pickerTypeofBike_sell.Items.Add("Road Bikes");
					pickerTypeofBike_sell.Items.Add("Mountain Bikes");
					pickerTypeofBike_sell.Items.Add("Hybrid Bikes");
					pickerTypeofBike_sell.Items.Add("Touring Bikes");
					pickerTypeofBike_sell.Items.Add("City Bikes");
					pickerTypeofBike_sell.Items.Add("Folding Bikes");
					pickerTypeofBike_sell.Items.Add("Electric Bikes");
					pickerTypeofBike_sell.Items.Add("Singlespeed Bikes");





					pickerCondition_sell.Items.Add("New");
					pickerCondition_sell.Items.Add("Like New");
					pickerCondition_sell.Items.Add("Ridable");
					pickerCondition_sell.Items.Add("Unridable");

					


					pickerTypeofBike_rent.Items.Add("Road Bikes");
					pickerTypeofBike_rent.Items.Add("Mountain Bikes");
					pickerTypeofBike_rent.Items.Add("Hybrid Bikes");
					pickerTypeofBike_rent.Items.Add("Touring Bikes");
					pickerTypeofBike_rent.Items.Add("City Bikes");
					pickerTypeofBike_rent.Items.Add("Folding Bikes");
					pickerTypeofBike_rent.Items.Add("Electric Bikes");
					pickerTypeofBike_rent.Items.Add("Singlespeed Bikes");





					pickerCondition_rent.Items.Add("New");
					pickerCondition_rent.Items.Add("Like New");
					pickerCondition_rent.Items.Add("Ridable");
					pickerCondition_rent.Items.Add("Unridable");

				    pickerInr_sell.Items.Add("EUR");
					pickerInr_sell.Items.Add("CHF");
					pickerInr_sell.Items.Add("USD");
				    pickerInr_sell.Items.Add("GBP");

					pickerInr_rent.Items.Add("EUR");
				    pickerInr_rent.Items.Add("CHF");
					pickerInr_rent.Items.Add("USD");
					pickerInr_rent.Items.Add("GBP");

					pickerPerDayHour.Items.Add("Per Day");
					pickerPerDayHour.Items.Add("Per Hour");

				pickerGender_rent.Items.Add("Man");
				pickerGender_rent.Items.Add("Women");
				pickerGender_rent.Items.Add("Unisex");

				    pickerSize_rent.Items.Add("S");
				    pickerSize_rent.Items.Add("M");
pickerSize_rent.Items.Add("L");
pickerSize_rent.Items.Add("XL");

				}
				catch (Exception ex)
				{

				}
			}
			private void PrepaireUI()
			{
				try
				{
					headerSize = (App.ScreenHeight / 3) - 20;
					imgTopBackgroud.HeightRequest = headerSize;
					cellSize = headerSize / 2 - 10;
					img1.HeightRequest = cellSize;
					img1.WidthRequest = cellSize;

					img2.HeightRequest = cellSize;
					img2.WidthRequest = cellSize;

					img3.HeightRequest = cellSize;
					img3.WidthRequest = cellSize;

					img4.HeightRequest = cellSize;
					img4.WidthRequest = cellSize;

					_scrollView.Padding = new Thickness(0);
					lstViewSell.IsVisible = false;
					if (StaticDataModel.IsFromSell)
					{
						bxSell.IsVisible = true;
						bxRent.IsVisible = false;
						slSell.IsVisible = true;
						slRent.IsVisible = false;
					}
					else
					{
						bxSell.IsVisible = false;
						bxRent.IsVisible = true;
						slSell.IsVisible = false;
						slRent.IsVisible = true;
					}


				}
				catch (Exception ex)
				{

				}
			} 

			void PickerInr_Sell_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerInr_sell.Items[pickerInr_sell.SelectedIndex];
				lblInr_sell.Text = item;
				lblInr_sell.TextColor = Color.Black;
			}

			void PickerTypeofBike_Sell_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerTypeofBike_sell.Items[pickerTypeofBike_sell.SelectedIndex];
				lblTypeofbike_sekk.Text = item;
				lblTypeofbike_sekk.TextColor = Color.Black;
			}

			void PickerPerDayHour_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerPerDayHour.Items[pickerPerDayHour.SelectedIndex];
				lblperdayhour.Text = item;
				lblperdayhour.TextColor = Color.Black;
			}

			void PickerCondition_Sell_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerCondition_sell.Items[pickerCondition_sell.SelectedIndex];
				lblCondition_sell.Text = item;
				lblCondition_sell.TextColor = Color.Black;
			}

			void PickerTypeofBike_Rent_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerTypeofBike_rent.Items[pickerTypeofBike_rent.SelectedIndex];
				lblTypeofbike_rent.Text = item;
				lblTypeofbike_rent.TextColor = Color.Black;
			}

			void PickerCondition_Rent_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerCondition_rent.Items[pickerCondition_rent.SelectedIndex];
				lblCondtition_rent.Text = item;
				lblCondtition_rent.TextColor = Color.Black;
			}

			void PickerInr_Rent_SelectedIndexChanged(object sender, EventArgs e)
			{
				var item = pickerInr_rent.Items[pickerInr_rent.SelectedIndex];
				lblInr_rent.Text = item;
				lblInr_rent.TextColor = Color.Black;
			}

			void BtnSell_Clicked(object sender, EventArgs e)
			{
				bxSell.IsVisible = true;
				bxRent.IsVisible = false;
				slSell.IsVisible = true;
				slRent.IsVisible = false;
			}

			void BtnRent_Clicked(object sender, EventArgs e)
			{
				bxSell.IsVisible = false;
				bxRent.IsVisible = true;
				slSell.IsVisible = false;
				slRent.IsVisible = true;
			}

			public void SellTapped(object sender, EventArgs e)
			{
				bxSell.IsVisible = true;
				bxRent.IsVisible = false;
				slSell.IsVisible = true;
				slRent.IsVisible = false;
			}
			public void RentTapped(object sender, EventArgs e)
			{
				bxSell.IsVisible = false;
				bxRent.IsVisible = true;
				slSell.IsVisible = false;
				slRent.IsVisible = true;
			}
			public void male_sellTapped(object sender, EventArgs e)
			{
				btnMale_sell.Image = "radio_check";
				btnfeMale_sell.Image = "radio_uncheck";
				btnother_sell.Image = "radio_uncheck";
				gender = "male";
			}
			public void female_sellTapped(object sender, EventArgs e)
			{
				btnMale_sell.Image = "radio_uncheck";
				btnfeMale_sell.Image = "radio_check";
				btnother_sell.Image = "radio_uncheck";
				gender = "female";
			}
			public void other_sellTapped(object sender, EventArgs e)
			{
				btnMale_sell.Image = "radio_uncheck";
				btnfeMale_sell.Image = "radio_uncheck";
				btnother_sell.Image = "radio_check";
				gender = "other";
			}
			void Btnother_Sell_Clicked(object sender, EventArgs e)
			{
				btnMale_sell.Image = "radio_uncheck";
				btnfeMale_sell.Image = "radio_uncheck";
				btnother_sell.Image = "radio_check";
				gender = "other";
			}

			void BtnfeMale_Sell_Clicked(object sender, EventArgs e)
			{
				btnMale_sell.Image = "radio_uncheck";
				btnfeMale_sell.Image = "radio_check";
				btnother_sell.Image = "radio_uncheck";
				gender = "female";
			}

			void BtnMale_Sell_Clicked(object sender, EventArgs e)
			{
				btnMale_sell.Image = "radio_check";
				btnfeMale_sell.Image = "radio_uncheck";
				btnother_sell.Image = "radio_uncheck";
				gender = "male";
			}



			async void typeofbike_sellTapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					SelectTypeOfBikePopup s = new SelectTypeOfBikePopup(0);
					await Navigation.PushPopupAsync(s);
					//pickerTypeofBike_sell.Focus();
				});
			}
		async void size_rentTapped(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>

			{
				pickerSize_rent.Focus();
			});
		}
		async void gender_rentTapped(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>

			{
				pickerGender_rent.Focus();
			});
		}
			async void condition_sellTapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					pickerCondition_sell.Focus();
				});
			}
			async void inr_sellTapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					pickerInr_sell.Focus();
				});
			}
			async void typeofbike_rentTapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					SelectTypeOfBikePopup s = new SelectTypeOfBikePopup(1);
					Navigation.PushPopupAsync(s);
				});
			}
			async void condition_rentTapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					pickerCondition_rent.Focus();
				});
			}
			async void inr_rentTapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					pickerInr_rent.Focus();
				});
			}
			async void perdayhour_Tapped(object sender, EventArgs e)
			{
				Device.BeginInvokeOnMainThread(async () =>

				{
					pickerPerDayHour.Focus();
				});
			}
			async void top_rentTapped(object sender, EventArgs e)
			{
				if (countTopRent % 2 == 0)
				{
					imgTop_rent.Source = "checkbox-checked.png";
				}
				else
				{
					imgTop_rent.Source = "checkbox-unchecked.png";
				}
				countTopRent++;
			}
			async void top_sellTapped(object sender, EventArgs e)
			{
				if (countTopSell % 2 == 0)
				{
					imgTop_sell.Source = "checkbox-checked.png";
				}
				else
				{
					imgTop_sell.Source = "checkbox-unchecked.png";
				}
				countTopSell++;
			}

			async void cameraTapped(object sender, EventArgs e)
			{
				TakeImage();

				//var list = await	DependencyService.Get<IiOSMethods>().MultiImagePicker();



			}
			private async void TakeImage()
			{
				var action = await DisplayActionSheet("Choose", "Cancel", null, "Camera", "Photo Library");
				Debug.WriteLine(action);
				if (action == "Camera")
				{
					Device.BeginInvokeOnMainThread(() =>
					   {
						   FromCamera();
					   });

				}
				else if (action == "Photo Library")
				{
					Device.BeginInvokeOnMainThread(() =>
					   {
						   FromLibrary();
					   });

				}

			}
			private async void ChooseAction()
			{
				var action = await DisplayActionSheet("Choose", "Cancel", null, "Change", "Remove");
				Debug.WriteLine(action);
				if (action == "Change")
				{
					Device.BeginInvokeOnMainThread(() =>
					   {
						   TakeImage();
					   });

				}
				else if (action == "Remove")
				{
					Device.BeginInvokeOnMainThread(() =>
					   {
						   RemoveImage(fromImageView);
					   });

				}
			}

			private async void FromCamera()
			{
				try
				{


					if (!CrossMedia.Current.IsPickPhotoSupported)
					{
						DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
						return;
					}
					profileData = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
					{
						PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
					});


					if (profileData == null)
						return;

					//				img1.Source = ImageSource.FromStream(() =>
					//{
					//	var stream = profileData.GetStream();

					//	//file.Dispose();
					//	return stream;
					//});
					SetImage(fromImageView, profileData);
					gvSingleImage.IsVisible = false;
					gvMultipleImage.IsVisible = true;

				}
				catch (Exception ex)
				{

				}
			}
			private async void FromLibrary()
			{
				try
				{


					if (!CrossMedia.Current.IsPickPhotoSupported)
					{
						DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
						return;
					}
					profileData = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
					{
						PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
					});


					if (profileData == null)
						return;

					//				img1.Source = ImageSource.FromStream(() =>
					//{
					//	var stream = profileData.GetStream();

					//	//file.Dispose();
					//	return stream;
					//});
					SetImage(fromImageView, profileData); gvSingleImage.IsVisible = false;
					gvMultipleImage.IsVisible = true;

				}
				catch (Exception ex)
				{

				}
			}
			private async void GetPicture()
			{
				try
				{
					await CrossMedia.Current.Initialize();

					if (!CrossMedia.Current.IsPickPhotoSupported)
					{
						DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
						return;
					}
					profileData = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
					{
						PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
					});


					if (profileData == null)
						return;

					imgProduct.Source = ImageSource.FromStream(() =>
					{
						var stream = profileData.GetStream();

						//file.Dispose();
						return stream;
					});

				}
				catch (Exception ex)
				{

				}
			}

			void BtnSellit_Clicked(object sender, EventArgs e)
			{

				if (IsvalidatedSellTab())
				{
					if (profileData != null)
					{

						AddProduct_Sell();
					}
					else
					{
						StaticMethods.ShowToast("Please select or take a picture to continue!");
					}

				}
			}

			void BtnRentit_Clicked(object sender, EventArgs e)
			{
				if (IsvalidatedRentTab())
				{
					AddProduct_Rent();
				}
			}
			private bool IsvalidatedSellTab()
			{

				try
				{
					if (string.IsNullOrEmpty(txtWhatYouAreSelling_sell.Text))
					{
						txtWhatYouAreSelling_sell.PlaceholderColor = Color.Red;
						return false;
					}

					else if (string.IsNullOrEmpty(txtDescribe_sell.Text))
					{
						txtDescribe_sell.PlaceholderColor = Color.Red;
						return false;
					}

					else if (lblTypeofbike_sekk.Text == "Type of bike")
					{
						lblTypeofbike_sekk.TextColor = Color.Red;
						return false;
					}
					else if (lblCondition_sell.Text == "Condition")
					{
						lblCondition_sell.TextColor = Color.Red;
						return false;
					}
					else if (lblInr_sell.Text == "INR")
					{
						lblInr_sell.TextColor = Color.Red;
						return false;
					}
					else if (string.IsNullOrEmpty(txtPrice_sell.Text))
					{
						txtPrice_sell.PlaceholderColor = Color.Red;
						return false;
					}
					else if (string.IsNullOrEmpty(txtAddress.Text))
					{
						txtAddress.PlaceholderColor = Color.Red;
						return false;
					}


					else
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					return false;
				}
				finally
				{

				}
			}
			private bool IsvalidatedRentTab()
			{

				try
				{
					if (string.IsNullOrEmpty(txtWhatYouAreSelling_rent.Text))
					{
						txtWhatYouAreSelling_rent.PlaceholderColor = Color.Red;
						return false;
					}

					else if (string.IsNullOrEmpty(txtDescribe_rent.Text))
					{
						txtDescribe_rent.PlaceholderColor = Color.Red;
						return false;
					}

					else if (lblTypeofbike_rent.Text == "Type of bike")
					{
						lblTypeofbike_rent.TextColor = Color.Red;
						return false;
					}
					else if (lblCondtition_rent.Text == "Condition")
					{
						lblCondtition_rent.TextColor = Color.Red;
						return false;
					}
					else if (lblInr_rent.Text == "INR")
					{
						lblInr_rent.TextColor = Color.Red;
						return false;
					}
					else if (string.IsNullOrEmpty(txtPrice_rent.Text))
					{
						txtPrice_rent.PlaceholderColor = Color.Red;
						return false;
					}
					else if (lblperdayhour.Text == "Per day/hour")
					{
						lblperdayhour.TextColor = Color.Red;
						return false;
					}
				else if (string.IsNullOrEmpty(lblAddress_rent.Text))
					{
					lblAddress_rent.TextColor = Color.Red;
						return false;
					}
					else
					{
						return true;
					}
				}
				catch (Exception ex)
				{
					return false;
				}
				finally
				{

				}
			}
			private async Task AddProduct_Sell()
			{
				string base64 = string.Empty;
				string ret = string.Empty;
				ProductModel um = new ProductModel();
				StaticMethods.ShowLoader();
				Task.Factory.StartNew(
						// tasks allow you to use the lambda syntax to pass wor
						() =>
						{

							um.user_id = StaticDataModel.userId;
							um.gender = gender;
							um.product_name = txtWhatYouAreSelling_sell.Text;
							um.product_description = txtDescribe_sell.Text;
							um.type_of_bike = lblTypeofbike_sekk.Text;
							um.address = txtAddress.Text;
							if (profileData != null)
							{
								var bytearray = StaticMethods.StreamToByte(profileData.GetStream());
								base64 = Convert.ToBase64String(bytearray);
								um.product_image = base64;
								var extsn = Path.GetExtension(profileData.Path);
								var str = extsn.Split('.');
								extsn = str[1];
								um.extension = extsn;
							}
							um.type = 0;
							um.currency = lblInr_sell.Text;

							um.price = txtPrice_sell.Text;
							um.condition = lblCondition_sell.Text;
							if (switchsellValue)
								um.is_facebook_sharable = 1;
							else
								um.is_facebook_sharable = 0;


							ret = WebService.AddProduct(um);



						}).ContinueWith(async
					t =>
						{
							if (ret == "success")
							{
								StaticMethods.ShowToast("Product added successfully");
								await Navigation.PushAsync(new HomePage());
							}
							else
							{
								StaticMethods.ShowToast("Failed to add product, Please try after some time.");
							}

							StaticMethods.DismissLoader();

						}, TaskScheduler.FromCurrentSynchronizationContext()
					);
			}
			private async Task AddProduct_Rent()
			{
				string base64 = string.Empty;
				ProductModel um = null;
				string ret = string.Empty;
				StaticMethods.ShowLoader();
				Task.Factory.StartNew(
						// tasks allow you to use the lambda syntax to pass wor
						() =>
						{
							um = new ProductModel();
							um.user_id = StaticDataModel.userId;
							um.gender = gender;
							um.product_name = txtWhatYouAreSelling_rent.Text;
							um.product_description = txtDescribe_rent.Text;
							um.type_of_bike = lblTypeofbike_rent.Text;
							um.address = lblAddress_rent.Text;
							if (profileData != null)
							{
								var bytearray = StaticMethods.StreamToByte(profileData.GetStream());
								base64 = Convert.ToBase64String(bytearray);
								um.product_image = base64;
								var extsn = Path.GetExtension(profileData.Path);
								var str = extsn.Split('.');
								extsn = str[1];
								um.extension = extsn;
							}
							um.type = 1;
							um.currency = lblInr_rent.Text;

							um.price = txtPrice_rent.Text;
							um.condition = lblCondtition_rent.Text;
							if (switchrentValue)
								um.is_facebook_sharable = 1;
							else
								um.is_facebook_sharable = 0;

							ret = WebService.AddProduct(um);


						}).ContinueWith(async
					t =>
						{
							if (ret == "success")
							{
								StaticMethods.ShowToast("Product added successfully");
								await Navigation.PushAsync(new HomePage());
							}
							else
							{
								StaticMethods.ShowToast("Failed to add product, Please try after some time.");
							}

							StaticMethods.DismissLoader();

						}, TaskScheduler.FromCurrentSynchronizationContext()
					);
			}
			private async Task AutoComplete()
			{
				string[] _autocompleteArray = null;
				AutoCompleteModel model = null;
				Task.Factory.StartNew(
						// tasks allow you to use the lambda syntax to pass wor
						() =>
						{


							model = WebService.GetAutoCompleteLocation(txtAddress.Text);


						}).ContinueWith(async
					t =>
						{
							if (model != null)
							{
								lstViewSell.IsVisible = true;
								lstViewSell.ItemsSource = model.predictions;
								Device.BeginInvokeOnMainThread(async () =>
								{
									ScrollToFeedback();
								});
							}

						}, TaskScheduler.FromCurrentSynchronizationContext()
					);
			}
			public async void ScrollToFeedback()
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					_scrollView.Padding = new Thickness(0, 0, 0, 110);
					await _scrollView.ScrollToAsync(txtAddress, ScrollToPosition.Start, true);
				});

			}
			private async Task AutoCompleteRent()
			{

				AutoCompleteModel model = null;
				Task.Factory.StartNew(
						// tasks allow you to use the lambda syntax to pass wor
						() =>
						{


							//model = WebService.GetAutoCompleteLocation(txtAddressRent.Text);


						}).ContinueWith(async
				t =>
			{
				if (model != null)
				{
					
					Device.BeginInvokeOnMainThread(async () =>
		{
			
		});
				}

			}, TaskScheduler.FromCurrentSynchronizationContext()
					);
			}
			
		}
	}


