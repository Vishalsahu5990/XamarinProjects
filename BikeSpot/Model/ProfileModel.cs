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
			public object dob { get; set; }
			public object gender { get; set; }
			public object mobile_no { get; set; }
			public object website_url { get; set; }
			public string total_reviews { get; set; }
			public string ratings { get; set; }
		}
	}
}
