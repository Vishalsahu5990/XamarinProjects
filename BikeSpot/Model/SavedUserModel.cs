using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BikeSpot
{
	public class SavedUserModel
	{
		public string result { get; set; }
		public List<Data> data { get; set; }
		public int responseCode { get; set; }

		public class Data
		{
			public string user_id { get; set; }
			public string name { get; set; }
			public string profile_pic { get; set; }
			public string email { get; set; }
			public string dob { get; set; }
			public string gender { get; set; }
			public string contact_number { get; set; }
			public string website_url { get; set; }
			public string total_reviews { get; set; }
			public string ratings { get; set; }
			public bool user_is_favorite { get; set; }

		}
	}
}

