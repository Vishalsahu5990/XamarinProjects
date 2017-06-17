using BikeSpot.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("BikeSpot")]  //Note: only one in a project please...
[assembly: ExportEffect(typeof(RangeSliderEffect), nameof(RangeSliderEffect))]
namespace BikeSpot.iOS
{
public class RangeSliderEffect : PlatformEffect
{
	protected override void OnAttached()
	{
		var ctrl = (Xamarin.RangeSlider.RangeSliderControl)Control;
			ctrl.TintColor = Color.FromHex("#20D1C8").ToUIColor(); 

	}

	protected override void OnDetached()
	{
			
	}
	
	}
}
