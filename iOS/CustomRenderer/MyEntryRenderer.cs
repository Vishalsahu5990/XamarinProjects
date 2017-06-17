﻿using System;
using BikeSpot;
using BikeSpot.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace BikeSpot.iOS
{
public class MyEntryRenderer : EntryRenderer
{
	protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
	{
		base.OnElementChanged(e);

		if (Control != null)
		{
			// do whatever you want to the UITextField here!
			Control.BackgroundColor = UIColor.Clear;
			Control.Layer.BorderWidth = 0;
			Control.BorderStyle = UITextBorderStyle.None;

		}
	}
}
}
