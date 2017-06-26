using System;
namespace BikeSpot
{
	public class OfferStatusModel
	{
		public string responseMessage { get; set; }
		public string result { get; set; }
		public int responseCode { get; set; }
		public int offer_id { get; set; }
		public object notification_response { get; set; }
	}
}
