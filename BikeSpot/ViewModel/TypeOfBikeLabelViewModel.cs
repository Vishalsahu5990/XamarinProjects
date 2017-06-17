using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BikeSpot
{
	public class TypeOfBikeLabelViewModel : INotifyPropertyChanged
	{
		string typeofbike;

		public event PropertyChangedEventHandler PropertyChanged;

		public TypeOfBikeLabelViewModel()
		{

			Device.BeginInvokeOnMainThread(async () =>
			{
				if (SelectTypeOfBikePopup.list == null)
				{
					this.TypeofBike = "Type of bike";
				}
				
			});

		}
		public TypeOfBikeLabelViewModel(string _value)
		{
			if (SelectTypeOfBikePopup.list == null)
				this.TypeofBike = _value;
			else
				this.TypeofBike = "value changed";

		}
		public string TypeofBike
		{
			set
			{
				if (typeofbike != value)
				{
					typeofbike = value;

					if (PropertyChanged != null)
					{
						PropertyChanged(this,
							new PropertyChangedEventArgs("TypeofBike"));
					}
				}
			}
			get
			{
				return typeofbike;
			}
		}
	}
}