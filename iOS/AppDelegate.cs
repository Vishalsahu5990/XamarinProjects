using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.Support.iOS;
using Foundation;
using Google.Maps;
using Google.MobileAds;
using ImageCircle.Forms.Plugin.iOS;
using MapKit;
using Naxam.iOS.PlacePicker;
using NControl.Controls.iOS;
using UIKit;
using Xamarin;
using Xamarin.Auth.Presenters;
using Xamarin.Forms;
using Plugin.CrossPlacePicker.Abstractions;
namespace BikeSpot.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		string userAgent = "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48B; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/43.0.2357.65 Mobile Safari/537.36";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			//MapServices.ProvideAPIKey("AIzaSyCYs_pRMo3IVDGcVpNw96KBOWtf5Wd6mLQ");
			// set default useragent
			NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject(userAgent), NSObject.FromObject("UserAgent"));
			NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);


			PlacesClient.ProvideApiKey("AIzaSyBLtYoWAUYZ8IrkcmpxS84HXyWyi2XdxrI");
            MapServices.ProvideAPIKey("AIzaSyBLtYoWAUYZ8IrkcmpxS84HXyWyi2XdxrI");

			global::Xamarin.Forms.Forms.Init();

			MobileAds.Configure("ca-app-pub-7176870068365595~1750148263");
			FormsMaps.Init();
			ImageCircleRenderer.Init();
			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

			App.ScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
			App.ScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;

			Xamarin.Auth.Presenters.OAuthLoginPresenter.PlatformLogin = (authenticator) =>
						{
							var oAuthLogin = new OAuthLoginPresenter();
							oAuthLogin.Login(authenticator);
						};
			MessagingCenter.Subscribe<ImageSource>(this, "Share", IosMethods.Share, null);
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

	}
}
