using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class ChatUserModel
	{
		public class Datum
		{
			public string chat_user_id { get; set; }
			public string product_id { get; set; }
			public string user_id { get; set; }
			public string name { get; set; }
			public string profile_pic { get; set; }
			public string product_name { get; set; }
			public string product_image { get; set; }
			public string message { get; set; }
			public string last_chating_time { get; set; }
			public string offer_price { get; set; }
			public string status { get; set; }
			public string product_owner_id { get; set; }
		}


		public string result { get; set; }
		public List<Datum> data { get; set; }
		public int responseCode { get; set; }

	}
}
