using System;
using System.Collections.Generic;
using CoreGraphics;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Windows.Input;
using Foundation;
using System.Collections.ObjectModel;
using BikeSpot.iOS;
using BikeSpot;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace BikeSpot.iOS
{
public class CustomMapRenderer : MapRenderer
{
	CustomMap formsMap;
	UIView customPinView;
	List<CustomPin> customPins;
	

	CustomPin customPin;
	private MKMapView NativeMap { get { return (this.NativeView as MapRenderer).Control as MKMapView; } }
	private readonly ICommand _PinTapped;
	public static int bar_id = 0;

	protected override void OnElementChanged(ElementChangedEventArgs<View> e)
	{
		base.OnElementChanged(e);

		if (e.OldElement != null)
		{
			var nativeMap = Control as MKMapView;

			nativeMap.GetViewForAnnotation = null;
			nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
			nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
			nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;

			nativeMap.ZoomEnabled = true;

		}

		if (e.NewElement != null)
		{
			formsMap = (CustomMap)e.NewElement;

			var nativeMap = Control as MKMapView;
			customPins = formsMap.CustomPins;
			//				var cmd = formsMap.PinTapped;
			//				MyMapDelegate myMapDelegate = new MyMapDelegate (cmd);
			//				nativeMap.Delegate = myMapDelegate;

			nativeMap.GetViewForAnnotation += GetViewForAnnotation;
			nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
			nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
			nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;

			var map = e.NewElement;

			//var petDetailCommand = CustomMap.ShowDetailCommand;

			// Send this along to MyMapDelegate
			// Initiate with relevant parameters
			SetNativeControl(nativeMap);


		}

	}

	protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		base.OnElementPropertyChanged(sender, e);
	}

	MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
	{
		MKAnnotationView annotationView = null;
		UIImage image;
		try
		{


			//if (annotation is MKUserLocation)
			//	return null;

			var anno = annotation as MKPointAnnotation;
			customPins = formsMap.CustomPins;
			customPin = GetCustomPin(anno);
			if (customPin == null)
			{
				return null;
				//					//throw new Exception ("Custom pin not found");
			}
			else
			{
				annotationView = mapView.DequeueReusableAnnotation(customPin.Id);
				//if (annotationView == null) {
				annotationView = new CustomMKPinAnnotationView(annotation, customPin.Id, customPin.Pin.Address);

				if (customPin.Pin.Label != "")
				{
					image = UIImage.FromFile("marker.png");
				}
				else
				{
					image = UIImage.FromFile("marker.png");
				}

				annotationView.Image = image;
				annotationView.CalloutOffset = new CGPoint(0, 0);
				annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile("logo.png"));
				annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
				annotationView.TintColor = UIColor.Blue;
				((CustomMKPinAnnotationView)annotationView).Id = customPin.Id;
				((CustomMKPinAnnotationView)annotationView).Url = customPin.Url;
				((CustomMKPinAnnotationView)annotationView).Url = customPin.Pin.Address;
				return annotationView;
				//}
			}
			annotationView.CanShowCallout = false;

		}
		catch (Exception ex)
		{
			return null;
		}



	}

	void OnCalloutAccessoryControlTapped(object sender, MKMapViewAccessoryTappedEventArgs e)
	{
		var customView = e.View as CustomMKPinAnnotationView;
		if (!string.IsNullOrWhiteSpace(customView.Url))
		{
			UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(customView.Url));
		}
	}

	void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
	{
		try
		{
			var customView = e.View as CustomMKPinAnnotationView;
			customPinView = new UIView();

			if (customView.Id == "Xamarin")
			{
				customPinView.Frame = new CGRect(0, 0, 200, 84);
				var image = new UIImageView(new CGRect(0, 0, 200, 84));
				//image.Image = UIImage.FromFile ("cat1.png");
				customPinView.AddSubview(image);
				customPinView.Center = new CGPoint(0, -(e.View.Frame.Height + 75));
				e.View.AddSubview(customPinView);
			}
			bar_id = Convert.ToInt32(customPin.Details);
			bar_id = Convert.ToInt32(customView.Url);
			formsMap.PinTapped.Execute(formsMap.PinTapped);
		}
		catch (Exception ex)
		{

		}
	}

	void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
	{
		try
		{
			//var ann = ((CustomMKPinAnnotationView)sender);
			var customView = e.View as CustomMKPinAnnotationView;

			//var anno = ((customPinView)sender);

			if (!e.View.Selected)
			{
				customPinView.RemoveFromSuperview();
				customPinView.Dispose();
				customPinView = null;

			}

		}
		catch (Exception ex)
		{

		}
	}

	CustomPin GetCustomPin(MKPointAnnotation annotation)
	{


		if (ProductDetailsPage.staticcustomPins == formsMap.CustomPins.Count)
		{
ProductDetailsPage.staticcustomPins = 0;
		}
		//			foreach (var pin in formsMap.CustomPins) 
		//			{					
		//				return pin;
		//			}
		CustomPin pin = null;
		pin = formsMap.CustomPins[ProductDetailsPage.staticcustomPins];
ProductDetailsPage.staticcustomPins++;
		return pin;
	}

	static UIImage FromUrl(string uri)
	{

		using (var url = new NSUrl(uri))
		using (var data = NSData.FromUrl(url))
			return UIImage.LoadFromData(data);
	}


}
}
