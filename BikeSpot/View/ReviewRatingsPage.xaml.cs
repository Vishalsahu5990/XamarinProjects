using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class ReviewRatingsPage : ContentPage
	{
		public int _rating = 0;
		public ReviewRatingsPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			_frame.WidthRequest = App.ScreenWidth / 2;
			_frame.HeightRequest = App.ScreenWidth / 2;
			btnSubmit.Clicked += BtnSubmit_Clicked;
			txtReview.Focused+= TxtReview_Focused;
		}

		async void back_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				await Navigation.PopModalAsync();  

			}
			catch (Exception ex)
			{


			}
		}

		void TxtReview_Focused(object sender, FocusEventArgs e)
		{
			txtReview.PlaceholderColor = Color.Gray;
		}

		async void star1_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				_rating = 1;
				imgStar1.Source = "star-filled";
				imgStar2.Source = "star_empty";
				imgStar3.Source = "star_empty";
				imgStar4.Source = "star_empty";
			    imgStar5.Source = "star_empty";

			}
			catch (Exception ex)
			{


			}
		}
		async void star2_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				_rating = 2;
				imgStar1.Source = "star-filled";
				imgStar2.Source = "star-filled";
				imgStar3.Source = "star_empty";
				imgStar4.Source = "star_empty";
			    imgStar5.Source = "star_empty";

			}
			catch (Exception ex)
			{


			}
		}
		async void star3_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				_rating = 3;
				
				imgStar1.Source = "star-filled";
				imgStar2.Source = "star-filled";
				imgStar3.Source = "star-filled";
				imgStar4.Source = "star_empty";
			    imgStar5.Source = "star_empty";
			}
			catch (Exception ex)
			{


			}
		}
		async void star4_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				_rating = 4;
				imgStar1.Source = "star-filled";
				imgStar2.Source = "star-filled";
				imgStar3.Source = "star-filled";
				imgStar4.Source = "star-filled";
			    imgStar5.Source = "star_empty";

			}
			catch (Exception ex)
			{


			}
		}
		async void star5_Tapped(object sender, System.EventArgs e)
		{
			try
			{
				_rating = 5;
				imgStar1.Source = "star-filled";
				imgStar2.Source = "star-filled";
				imgStar3.Source = "star-filled";
				imgStar4.Source = "star-filled";
			    imgStar5.Source = "star-filled";
			}
			catch (Exception ex)
			{


			}
		}

		void BtnSubmit_Clicked(object sender, EventArgs e)
		{
			if (_rating == 0)
			{
				StaticMethods.ShowToast("Please rate before submit!");
			}
			else if (string.IsNullOrEmpty(txtReview.Text))
			{
				txtReview.PlaceholderColor = Color.Red;
			}
			else
			{
				AddComment();
			}
		}
private async Task AddComment()
{
	string ret = string.Empty;
			StaticMethods.ShowLoader();
	Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{

				ret = WebService.AddComment(_rating,txtReview.Text);


			}).ContinueWith(async
					t =>
			{
				if (ret == "success")
				{
					StaticMethods.ShowToast("Your reviews has been submitted successfully!");
						App.Current.MainPage =  new NavigationPage(new MainPage());
				}


					StaticMethods.DismissLoader();

			}, TaskScheduler.FromCurrentSynchronizationContext()
		);
		}
	}
}
