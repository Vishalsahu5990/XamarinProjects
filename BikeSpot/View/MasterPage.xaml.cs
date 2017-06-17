using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BikeSpot
{
	public partial class MasterPage : ContentPage
	{
		//initializing controls to access touch property 

      
		public Xamarin.Forms.Image _imgTimeline { get { return imgTimeline; } }
		public StackLayout _imgTemp{ get { return imgTemp; } }

		public ImageCircle.Forms.Plugin.Abstractions.CircleImage _imgProfile { get { return imgProfile; } }
		public Label _lblUserName { get { return lblUsername; } }
		public Label _lblEmail { get { return lblEmail; } }

		public StackLayout _slHome { get { return slHome; } }
		//public StackLayout _slProfile { get { return slProfile; } }
		public StackLayout _slSell { get { return slSell; } }
		public StackLayout _slRent { get { return slRent; } }
		public StackLayout _slMessages { get { return slMessages; } }
		public StackLayout _slBikeWizard { get { return slBikeWizard; } }
		public StackLayout _slUpgrades { get { return slUpgrades; } }
		public StackLayout _slPrivateRetailerAccount { get { return slPrivateRetailerAccount; } }
		public StackLayout _slFaq { get { return slFaq; } }
		public StackLayout _slContact { get { return slContact; } }
		public StackLayout _slSiteNotice { get { return slSiteNotice; }
		}
		public MasterPage() 
		{
			InitializeComponent();
				Icon = "menu.png";
			Title = "menu";
			NavigationPage.SetHasNavigationBar(this, false);
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			
		}
	}
}
