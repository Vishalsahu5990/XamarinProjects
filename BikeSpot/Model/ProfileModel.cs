using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class ProfileModel
	{
		public string result { get; set; }
		public List<Datum> data { get; set; }
		public int responseCode { get; set; }
		public class Datum
		{
			public string user_id { get; set; }
			public string name { get; set; }
			public string profile_pic { get; set; }
			public string email { get; set; }
			public string dob { get; set; }
			public string gender { get; set; }
			public string mobile_no { get; set; }
			public string contact_number { get; set; }
			public string website_url { get; set; }
			public string total_reviews { get; set; }
			public string ratings { get; set; }
			public string saved_users { get; set; }

		}
	}
}
