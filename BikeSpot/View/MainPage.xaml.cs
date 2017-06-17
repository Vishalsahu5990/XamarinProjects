using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class MainPage : MasterDetailPage
	{
        BikeSpot.MasterPage menuPage;
		public static readonly BindableProperty PinTappedCommandProperty = BindableProperty.Create<HomePage, ICommand>(x => x.OpenMenuCommand, null);

		public Command MenuTapped
		{
			get { return (Command)GetValue(PinTappedCommandProperty); }
			set
			{
				SetValue(PinTappedCommandProperty, value);
			}
		}

		public MainPage()
		{
			
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			//menuPage = new MasterPage();
   //         Master = menuPage;
			//Detail = new NavigationPage(new HomePage());

			var model=StaticMethods.GetLocalSavedData();
			if (model != null)
			{
				if (!string.IsNullOrEmpty(model.name))
					_masterpage._lblUserName.Text = model.name;
				else
					_masterpage._lblUserName.Text = "(User Name)";
				
				_masterpage._lblEmail.Text = model.email;
			}

			//To access navigation menu on a button click
			StaticDataModel._CurrentContext = this;

			//event to opent menu slider
			MenuTapped = new Command(async (x) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
											   {

												   this.IsPresented = true;
											   });
			});


			//Tap events 

var tapeventHome = new TapGestureRecognizer();
tapeventHome.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(HomePage)));
			};
			_masterpage._slHome.GestureRecognizers.Add(tapeventHome);


var tapevent = new TapGestureRecognizer();
tapevent.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(Settings)));
			};
			_masterpage._imgTemp.GestureRecognizers.Add(tapevent);



			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				//Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(MyProfilePage)));

			};
			_masterpage._imgProfile.GestureRecognizers.Add(tapGestureRecognizer);




var tapGestureRecognizerProfile = new TapGestureRecognizer();
tapGestureRecognizerProfile.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				StaticDataModel.IsFromSell = true;
				Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(AddProductPage)));
			};
			_masterpage._slSell.GestureRecognizers.Add(tapGestureRecognizerProfile);



			var tapGestureRecognizer3 = new TapGestureRecognizer();
			tapGestureRecognizer3.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				StaticDataModel.IsFromSell = false;
				Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(AddProductPage)));
			};
			_masterpage._slRent.GestureRecognizers.Add(tapGestureRecognizer3);



			var tapGestureRecognizer4 = new TapGestureRecognizer();
			tapGestureRecognizer4.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(ChatPage)));
			};
			_masterpage._slMessages.GestureRecognizers.Add(tapGestureRecognizer4);



			var tapGestureRecognizer5 = new TapGestureRecognizer();
			tapGestureRecognizer5.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
				//Detail = new NavigationPage((Page)Activator.CreateInstance (typeof(BikeWizardStartPage)));
				Navigation.PushModalAsync(new BikeWizardStartPage());
			};
			_masterpage._slBikeWizard.GestureRecognizers.Add(tapGestureRecognizer5);



			var tapGestureRecognizer6 = new TapGestureRecognizer();
			tapGestureRecognizer6.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
			};
			_masterpage._slUpgrades.GestureRecognizers.Add(tapGestureRecognizer6);



			var tapGestureRecognizer7 = new TapGestureRecognizer();
			tapGestureRecognizer7.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
			};
			_masterpage._slPrivateRetailerAccount.GestureRecognizers.Add(tapGestureRecognizer7);


			var tapGestureRecognizer8 = new TapGestureRecognizer();
			tapGestureRecognizer8.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
			};
			_masterpage._slFaq.GestureRecognizers.Add(tapGestureRecognizer8);


			var tapGestureRecognizer9 = new TapGestureRecognizer();
			tapGestureRecognizer9.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
			};
			_masterpage._slContact.GestureRecognizers.Add(tapGestureRecognizer9);


			var tapGestureRecognizer10 = new TapGestureRecognizer();
			tapGestureRecognizer10.Tapped += (s, e) =>
			{
				// handle the tap
				IsPresented = false;
			};
			_masterpage._slSiteNotice.GestureRecognizers.Add(tapGestureRecognizer10);


		}



	}
}
