using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class ProductDetailsModel
	{
public class Datum
{
	public string product_id { get; set; }
	public string user_id { get; set; }
	public string product_name { get; set; }
	public string product_description { get; set; }
	public object address { get; set; }
	public object lat { get; set; }
	public object @long { get; set; }
	public string type_of_bike { get; set; }
	public string product_image { get; set; }
	public string type { get; set; }
	public string condition { get; set; }
	public string currency { get; set; }
	public string price { get; set; }
	public string rent_time { get; set; }
	public string is_facebook_sharable { get; set; }
	public string created_at { get; set; }
	public string name { get; set; }
	public string profile_pic { get; set; }
	public string email { get; set; }
}


	public string result { get; set; }
	public List<Datum> data { get; set; }
	public int responseCode { get; set; }

	}
}
