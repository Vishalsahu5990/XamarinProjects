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
using CarouselView.FormsPlugin.iOS;

namespace BikeSpot.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		string userAgent = "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48B; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/43.0.2357.65 Mobile Safari/537.36";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject(userAgent), NSObject.FromObject("UserAgent"));
			NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);


			PlacesClient.ProvideApiKey("AIzaSyBLtYoWAUYZ8IrkcmpxS84HXyWyi2XdxrI");
			MapServices.ProvideAPIKey("AIzaSyBLtYoWAUYZ8IrkcmpxS84HXyWyi2XdxrI");

			global::Xamarin.Forms.Forms.Init();
			CarouselViewRenderer.Init();

			MobileAds.Configure("ca-app-pub-7176870068365595~1750148263");
			FormsMaps.Init();
			ImageCircleRenderer.Init();
			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
			PrepareRemoteNotification();
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
		private void PrepareRemoteNotification()
		{
			try
			{
				if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
				{
					var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
						UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
						new NSSet());

					UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
					UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					UIApplication.SharedApplication.RegisterForRemoteNotifications();
				}
				else
				{
					UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
					UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);

				}
			}
			catch (Exception ex)
			{

			}
		}
		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			//if (CrossPushNotification.Current is IPushNotificationHandler)
			//{
			//	((IPushNotificationHandler)CrossPushNotification.Current).OnErrorReceived(error);
			//}
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary options, Action<UIBackgroundFetchResult> completionHandler)
		{
			MessagingCenter.Send<object,string>(this, "NotificationRecieved", "");
		}
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			var DeviceToken = deviceToken.Description;
			if (!string.IsNullOrWhiteSpace(DeviceToken))
			{
				DeviceToken = DeviceToken.Trim('<').Trim('>');
				DeviceToken = DeviceToken.Replace(" ", "");
				StaticDataModel.DeviceToken = DeviceToken.ToString();
				Console.WriteLine(DeviceToken);
			}


		}

		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			
		}



		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			
		}

	}
}
