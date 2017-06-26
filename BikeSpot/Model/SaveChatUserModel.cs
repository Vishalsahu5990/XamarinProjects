using System;
namespace BikeSpot
{
	public class SaveChatUserModel
	{
		public string responseMessage { get; set; }
		public string result { get; set; }
		public int responseCode { get; set; }
		public int chat_user_id { get; set; }
		public string msg { get; set; }
		public int offer_status { get; set; }
	}
}
