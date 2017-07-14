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
using Newtonsoft.Json.Linq;
using Facebook;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;

namespace BikeSpot.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		ChatModel.Datum model = null;
        string userAgent = "Mozilla/5.0 (Linux; Android 5.1.1; Nexus 5 Build/LMY48B; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/43.0.2357.65 Mobile Safari/537.36";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			NSDictionary dictionary = NSDictionary.FromObjectAndKey(NSObject.FromObject(userAgent), NSObject.FromObject("UserAgent"));
			NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

			PlacesClient.ProvideApiKey("AIzaSyC0m0Td_wjHRWlS9uCOyFpZ_0kt6JYPRl8");
			MapServices.ProvideAPIKey("AIzaSyC0m0Td_wjHRWlS9uCOyFpZ_0kt6JYPRl8");

			global::Xamarin.Forms.Forms.Init();
			CarouselViewRenderer.Init();

			
			MobileAds.Configure("ca-app-pub-8565023384951297/2042844969");
			FormsMaps.Init();
			//Paypal initialization
			CrossPayPalManager.Init(		new PayPalConfiguration(
                PayPalEnvironment.Sandbox,
					"APP-80W284485P519543T"
				)
				{
                
					AcceptCreditCards = true,
					Language="en_US",
                ShippingAddressOption=ShippingAddressOption.None
				}
			);

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
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
			Facebook.CoreKit.AppEvents.ActivateApp(); 
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
			if (null != options && options.ContainsKey(new NSString("aps")))
			{
               
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;
				if (aps.ContainsKey(new NSString("moredata")))
				{
					 model = new ChatModel.Datum();
					NSDictionary json = (aps["moredata"] as NSDictionary);
                    if (!string.Equals(json["type"].ToString(), "status"))
                    {
                        model.created_at = json["created_at"].ToString();
                        model.message = json["message"].ToString();
                        model.name = json["name"].ToString();
                        model.offer_price = json["offer_price"].ToString();
                        model.product_id = json["product_id"].ToString();
                        model.product_image = json["product_image"].ToString();
                        model.product_name = json["product_name"].ToString();
                        model.profile_pic = json["profile_pic"].ToString();
                        model.reciever_id = json["reciever_id"].ToString();
                        model.sender_id = json["sender_id"].ToString();
                        model.status = json["status"].ToString();
                        model.type = json["type"].ToString();
                        model.user_id = json["user_id"].ToString();
                    }
                    else
                    {
						
						
						model.name = json["name"].ToString();
						model.offer_price = json["offer_price"].ToString();
						model.product_id = json["product_id"].ToString();
						model.product_image = json["product_image"].ToString();
						model.product_name = json["product_name"].ToString();
						model.profile_pic = json["profile_pic"].ToString();
						model.reciever_id = json["reciever_id"].ToString();
						model.sender_id = json["sender_id"].ToString();
						model.status = json["status"].ToString();
						model.type = json["type"].ToString();
						model.user_id = json["user_id"].ToString();

                    }
					
				} 

			}
            MessagingCenter.Send<object,ChatModel.Datum>(this, "NotificationRecieved",model);
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


/* init paypal library

     
    public void initLibrary()
{
    PayPal pp = PayPal.getInstance();
    if (pp == null)
    {  // Test to see if the library is already initialized

        // This main initialization call takes your Context, AppID, and target server
        pp = PayPal.initWithAppID(this, "APP-80W284485P519543T", PayPal.ENV_SANDBOX);
        // Required settings:
        // Set the language for the library
        pp.setLanguage("en_US";
        // Some Optional settings:
        // Sets who pays any transaction fees. Value is:
        // FEEPAYER_SENDER, FEEPAYER_PRIMARYRECEIVER, FEEPAYER_EACHRECEIVER, and FEEPAYER_SECONDARYONLY
        pp.setFeesPayer(PayPal.FEEPAYER_EACHRECEIVER);
        // true = transaction requires shipping
        pp.setShippingEnabled(false);
    }
}   
                       */
