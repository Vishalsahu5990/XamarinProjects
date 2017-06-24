using System.Reflection;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.SecureStorage;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BikeSpot
{
	public partial class App : Application
	{
		public static double ScreenHeight;
		public static double ScreenWidth;
		public static IGeolocator locator;
        public static User User { get; set; } 
		public App()
		{
			InitializeComponent(); 
			 
			//To initialize location
			locator = CrossGeolocator.Current; 
			locator.DesiredAccuracy = 50;

			var exists = CrossSecureStorage.Current.HasKey("userId");
			if (!exists)
			{ 
				MainPage = new NavigationPage(new LoginPage()); 
			}
			else
			{
				var model=StaticMethods. GetLocalSavedData();
				StaticDataModel.userId = model.user_id;

				MainPage = new  BikeSpot.MainPage();
			}
			//MainPage = new NavigationPage(new BikeSpot.Settings());  
		}

		protected override void OnStart()
		{
			// Handle when your app starts

		} 



	}
}
