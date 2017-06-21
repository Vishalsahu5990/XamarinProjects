using System;
using Xamarin.Forms;

namespace BikeSpot
{
	public class CustomImage : Image
	{
		public Func<byte[]> GetBytes { get; set; }
	}
}
