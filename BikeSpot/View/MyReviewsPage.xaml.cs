using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
    public partial class MyReviewsPage : ContentPage
    {
        ReviewModel model = null;
        public MyReviewsPage()
        {
            InitializeComponent();
            flowlistview.FlowColumnMinWidth = App.ScreenWidth;
            flowlistview.FlowItemsSource = Enumerable.Range(0, 10).ToList();
        }
        async void backTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetReviews().Wait();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private async Task GetReviews()
        {
            
            StaticMethods.ShowLoader();
            Task.Factory.StartNew(

                    () => 
                    { 
                 
                        model = WebService.GetMyReviews();


                    }).ContinueWith(async
                    t =>
                    {
                if (!ReferenceEquals(model.review_data,null))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                        for (int i = 0; i < model.review_data.Count; i++)
                                {
                                    if (!string.IsNullOrEmpty(model.review_data[i].profile_pic))
                                        model.review_data[i].profile_pic = Constants.ProfilePicUrl + model.review_data[i].profile_pic;
                                    else
                                        model.review_data[i].profile_pic = "dummyprofile.png";

                            SetRating(Convert.ToInt32( model.review_data[i].rating),i);
                                }
                                flowlistview.FlowItemsSource = model.review_data;
                            });
                        } 
                else
                {

                    StaticMethods.ShowToast("No Reviews!");
                }


                        StaticMethods.DismissLoader();

                    }, TaskScheduler.FromCurrentSynchronizationContext()
                );
        }
        private void SetRating(int num,int position)
        {
            try
            {
                switch(num) 
                {
                    case 1:
                        model.review_data[position].star1 = "star_small.png";
                        model.review_data[position].star2 = "star_small_empty.png";
                        model.review_data[position].star3 = "star_small_empty.png";
                        model.review_data[position].star4 = "star_small_empty.png";
                        model.review_data[position].star5 = "star_small_empty.png";
                        break;
					case 2:
						model.review_data[position].star1= "star_small.png";
						model.review_data[position].star2= "star_small.png";
						model.review_data[position].star3= "star_small_empty.png";
						model.review_data[position].star4= "star_small_empty.png";
						model.review_data[position].star5= "star_small_empty.png";
						break;
					case 3:
						model.review_data[position].star1= "star_small.png";
						model.review_data[position].star2= "star_small.png";
						model.review_data[position].star3= "star_small.png";
						model.review_data[position].star4= "star_small_empty.png";
						model.review_data[position].star5= "star_small_empty.png";
						break;
					case 4:
						model.review_data[position].star1 = "star_small.png";
						model.review_data[position].star2 = "star_small.png";
						model.review_data[position].star3 = "star_small.png";
						model.review_data[position].star4 = "star_small.png";
						model.review_data[position].star5 = "star_small_empty.png";
						break;
					case 5:
						model.review_data[position].star1 = "star_small.png";
						model.review_data[position].star2 = "star_small.png";
						model.review_data[position].star3 = "star_small.png";
						model.review_data[position].star4 = "star_small.png";
						model.review_data[position].star5 = "star_small.png";
						break;
                }
            } 
            catch (Exception ex)
            {

            }

        }

    }
}
