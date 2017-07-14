using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BikeSpot
{
    public partial class AddCardDetailsPage : ContentPage
    {
        string _paypalId = string.Empty;
        public AddCardDetailsPage()
        {
            InitializeComponent();
            Task.Factory.StartNew(() => 
             {
                _paypalId=    WebService.GetPaypalId(StaticDataModel.userId.ToString());
            }).ContinueWith((arg) => 
            {
                Device.BeginInvokeOnMainThread( () => 
                {

                if (!string.IsNullOrEmpty(_paypalId))
                    txtPaypalId.Text = _paypalId;
                });
            });
        }
        async void back_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();

        }
		void BtnSave_Clicked(object sender, EventArgs e)
		{
            if (!string.IsNullOrEmpty(txtPaypalId.Text))
                SavePaypalId().Wait();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            btnSave.Clicked+= BtnSave_Clicked;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
			btnSave.Clicked -= BtnSave_Clicked;
        }
        private async Task SavePaypalId()
        {
            string ret = string.Empty;
           try
            {
				StaticMethods.ShowLoader();
				Task.Factory.StartNew(() =>
				 {
                    ret = WebService.SavePaypalId(txtPaypalId.Text);
				 }).ContinueWith((arg) =>
				 {
					 Device.BeginInvokeOnMainThread(() =>
				   {

                        if (ret.Equals("success"))
                        {
                            StaticMethods.ShowToast("User paypal id saved successfully!");
                            Navigation.PopModalAsync();
                        }
				   });
				 });
				
            }
            catch (Exception ex)
            {

            }
            finally
            {
               StaticMethods.DismissLoader(); 
            }
        }

       
    }
}
