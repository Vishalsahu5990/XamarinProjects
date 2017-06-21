using System;
using BikeSpot.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BikeSpot.CustomImage), typeof(CustomImageRenderer))]
namespace BikeSpot.iOS
{
	public class CustomImageRenderer : ImageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
		{

			base.OnElementChanged(e);

			var newImage = e.NewElement as CustomImage;
			if (newImage != null)
			{
				newImage.GetBytes = () =>
				{
					return this.Control.Image.AsPNG().ToArray();
				};
			}

			var oldImage = e.OldElement as CustomImage;
			if (oldImage != null)
			{
				oldImage.GetBytes = null;
			}
		}

	}
}