using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class CategoryModel
	{
		public string result { get; set; }
		public Data data { get; set; }
		public int responseCode { get; set; }
		public class Rent
		{
			public string product_id { get; set; }
			public string user_id { get; set; }
			public string product_name { get; set; }
			public string product_description { get; set; }
			public string address { get; set; }
			public string lat { get; set; }
			public string @long { get; set; }
			public string type_of_bike { get; set; }
			public string product_image { get; set; }
			public string type { get; set; }
			public string condition { get; set; }
			public string currency { get; set; }
			public string price { get; set; }
			public string rent_time { get; set; }
			public string is_facebook_sharable { get; set; }
			public string created_at { get; set; }
		}
		public class Sell
		{
			public string product_id { get; set; }
			public string user_id { get; set; }
			public string product_name { get; set; }
			public string product_description { get; set; }
			public string address { get; set; }
			public string lat { get; set; }
			public string @long { get; set; }
			public string type_of_bike { get; set; }
			public string product_image { get; set; }
			public string type { get; set; }
			public string condition { get; set; }
			public string currency { get; set; }
			public string price { get; set; }
			public string rent_time { get; set; }
			public string is_facebook_sharable { get; set; }
			public string created_at { get; set; }
		}
		public class Data
		{
			public string user_id { get; set; }
			public string name { get; set; }
			public object profile_pic { get; set; }
			public string email { get; set; }
			public object dob { get; set; }
			public string gender { get; set; }
			public object mobile_no { get; set; }
			public object website_url { get; set; }
			public string total_reviews { get; set; }
			public object ratings { get; set; }
			public List<Sell> sell { get; set; }
			public List<Rent> rent { get; set; }
		}
	}
}
