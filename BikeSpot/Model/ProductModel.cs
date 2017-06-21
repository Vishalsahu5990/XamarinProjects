using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class ProductModel
	{
		public string method { get; set; }
		public int user_id { get; set; }
		public string product_name { get; set; }
		public string product_description { get; set; }
		public byte[] product_image { get; set; }
		public string extension { get; set; }
		public int type { get; set; }
		public string condition { get; set; }
		public string currency { get; set; }
		public string price { get; set; }
		public string rent_time { get; set; }
		public int is_facebook_sharable { get; set; }
		public string type_of_bike { get; set; }
public string address { get; set; }
public string gender { get; set; }
		public List<byte[]> imageByteArray { get; set; }
public string size { get; set; }
		public int itemHeight { get; set; }

	}
}
