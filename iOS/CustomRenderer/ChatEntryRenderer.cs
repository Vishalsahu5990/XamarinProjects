using System;
using BikeSpot;
using BikeSpot.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ChatEntry), typeof(ChatEntryRenderer))]
namespace BikeSpot.iOS
{
	public class ChatEntryRenderer : EntryRenderer
	{
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
			{
				if (Control != null)
				{
					// do whatever you want to the UITextField here!
					Control.BackgroundColor = UIColor.Clear;
					Control.Layer.BorderWidth = 0;
					Control.BorderStyle = UITextBorderStyle.None;
					Control.ReturnKeyType = UIReturnKeyType.Send;
					var chatEntryView = this.Element as ChatEntry;
					Control.ShouldEndEditing = (UITextField textField) =>
					{
						return !ChatEntry.keepOpen; // KeepOpen is a custom property I added and is set on my ViewModel if I want the Entry Keyboard to be kept open then I just set this to true.
					};

				}
			}

			base.OnElementPropertyChanged(sender, e);
		}

	}
}
