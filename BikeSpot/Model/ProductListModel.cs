using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class ProductListModel
	{

		public string result { get; set; }
		public List<List<Product>> data { get; set; }
		public int responseCode { get; set; }

	}
	public class Product
	{
		public string listing_type { get; set; }
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
		public string img_path { get; set; } 
		public string advertisement_img { get; set; } 
		public string redirect_url { get; set; }
		public string add_to_top { get; set; } 
		public string name { get; set; } 
		public string profile_pic { get; set; }
		public int height { get; set; } 
		public double width { get; set; } 
        public double columnWidth { get; set; } 
		public double listviewHeight { get; set; }
        public double imageHeight { get; set; } 
		public string email { get; set; } 
		public bool isTopEnable { get; set; }
		public bool isEnableListview { get; set; } 
		public string borderColor { get; set; }
		public List<Product> list { get; set; }
        public List<Model> imageList { get; set; }
	}
	public  class Model { public string image { get; set; } }
	}

