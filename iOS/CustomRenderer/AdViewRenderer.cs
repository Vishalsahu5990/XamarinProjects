using CoreGraphics;
using Google.MobileAds;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using BikeSpot;
using BikeSpot.iOS;

[assembly: ExportRenderer(typeof(AdView), typeof(AdViewRenderer))]
namespace BikeSpot.iOS
{
	public class AdViewRenderer : ViewRenderer<AdView, BannerView>
	{
		//string bannerId = "ca-app-pub-7176870068365595/3226881467";
		string bannerId = "ca-app-pub-8565023384951297/6473044566";
		BannerView adView;
		BannerView CreateNativeControl()
		{
			if (adView != null)
				return adView;


			UIViewController visibleViewController = GetVisibleViewController();
			// Setup your BannerView, review AdSizeCons class for more Ad sizes. 

			if (!ReferenceEquals(visibleViewController, null))
			{
				adView = new BannerView(size: AdSizeCons.SmartBannerPortrait,
											   origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
				{
					AdUnitID = bannerId,
					RootViewController = visibleViewController
				};

				// Wire AdReceived event to know when the Ad is ready to be displayed
				adView.AdReceived += (object sender, EventArgs e) =>
				{
				//ad has come in
			};


				adView.LoadRequest(GetRequest());
			}
			return adView;
		}

		Request GetRequest()
		{
			var request = Request.GetDefaultRequest();
			// Requests test ads on devices you specify. Your test device ID is printed to the console when
			// an ad request is made. GADBannerView automatically returns test ads when running on a
			// simulator. After you get your device ID, add it here
			//request.TestDevices = new [] { Request.SimulatorId.ToString () };
			return request;
		}

		/// 
		/// Gets the visible view controller.
		/// 
		/// The visible view controller.
		UIViewController GetVisibleViewController()
		{
			UIViewController rootController = null;
			if (!ReferenceEquals(UIApplication.SharedApplication.KeyWindow, null))
			{
				rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			}
			else
			{
				if (!ReferenceEquals(UIApplication.SharedApplication.Windows, null))
				{
					foreach (UIWindow window in UIApplication.SharedApplication.Windows)
					{
						if (!ReferenceEquals(window.RootViewController, null))
						{
							rootController = window.RootViewController;
						}
					}
				}
			}

			if (!ReferenceEquals(rootController, null))
			{
				if (rootController.PresentedViewController == null)
					return rootController;

				if (rootController.PresentedViewController is UINavigationController)
				{
					return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
				}

				if (rootController.PresentedViewController is UITabBarController)
				{
					return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
				}

				return rootController.PresentedViewController;
			}

			return null;
		}
		protected override void OnElementChanged(ElementChangedEventArgs<AdView> e)
		{
			base.OnElementChanged(e);
			if (Control == null)
			{
				CreateNativeControl();
				if (!ReferenceEquals(adView, null))
				{
					SetNativeControl(adView);
				}

			}
		}

	}
}