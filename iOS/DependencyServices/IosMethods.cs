using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeSpot;
using BikeSpot.iOS;
using ELCImagePicker;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: Dependency(typeof(IosMethods))]
namespace BikeSpot.iOS
{
	public class IosMethods : UIViewController, IiOSMethods
	{
private TaskCompletionSource<List< string>> taskCompletionSource;
		public void ShareContent(string client_id, string url)
		{

		}
		async public static void Share(ImageSource imageSource)
		{
			try
			{
				var handler = new ImageLoaderSourceHandler();
				var uiImage = await handler.LoadImageAsync(imageSource);

				var item = NSObject.FromObject(uiImage);
				var item1 = NSObject.FromObject("Fantastic Bike from Bikspot.");
				var activityItems = new[] { item, item1 };
				var activityController = new UIActivityViewController(activityItems, null);

				var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

				while (topController.PresentedViewController != null)
				{
					topController = topController.PresentedViewController;
				}


				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
				{
					activityController.PopoverPresentationController.SourceView = topController.View;
					activityController.PopoverPresentationController.SourceRect = new CoreGraphics.CGRect((topController.View.Bounds.Width / 2), (topController.View.Bounds.Height / 4), 0, 0);
				}


				topController.PresentViewController(activityController, true, () => { });
				//topController.PopoverPresentationController(activityController, true, () => { });
			}
			catch (Exception ex)
			{

			}
		}
		async public static void Share(byte[] imageData)
		{
			try
			{
				var img = UIImage.LoadFromData(NSData.FromArray(imageData));

				var item = NSObject.FromObject(img);
				var activityItems = new[] { item };
				var activityController = new UIActivityViewController(activityItems, null);

				var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

				while (topController.PresentedViewController != null)
				{
					topController = topController.PresentedViewController;
				}

				topController.PresentViewController(activityController, true, () => { });
			}
			catch (Exception ex)
			{

			}
		}
public async Task <List<string>> MultiImagePicker()
		{
			List<string> path = null;
			try
			{
				var picker = ELCImagePickerViewController.Instance;
				picker.MaximumImagesCount = 4;

				picker.Completion.ContinueWith(t =>
				{
					picker.BeginInvokeOnMainThread(() =>
						{
					//dismiss the picker
					picker.DismissViewController(true, null);

							if (t.IsCanceled || t.Exception != null)
							{
							}
							else
							{
							taskCompletionSource = new TaskCompletionSource<List<string>>();
								 path = new List<string>();

								var items = t.Result as List<AssetResult>;
								foreach (var item in items)
								{
								path.Add(item.Path);
								}


							}


						});
				});
				var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
				while (topController.PresentedViewController != null)
				{
					topController = topController.PresentedViewController;
				}


				topController.PresentViewController(picker, true, null);

				}
			catch (Exception ex)
			{

			}
			return path;
		}

	}
}
