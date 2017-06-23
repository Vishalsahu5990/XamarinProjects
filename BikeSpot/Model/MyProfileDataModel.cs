using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class MyProfileDataModel
	{
public string result { get; set; }
public Data data { get; set; }
public int responseCode { get; set; }
public class Sell
{
	public string product_id { get; set; }
	public string user_id { get; set; }
	public string product_name { get; set; }
	public string product_description { get; set; }
	public string gender { get; set; }
	public string address { get; set; }
	public string lat { get; set; }
	public string @long { get; set; }
	public string type_of_bike { get; set; }
	public string product_image { get; set; }
	public string type { get; set; }
	public string condition { get; set; }
	public string currency { get; set; }
	public object price { get; set; }
	public string rent_time { get; set; }
	public string framesize { get; set; }
	public string add_to_top { get; set; }
	public string created_at { get; set; }
public int height { get; set; }
public double width { get; set; }
public double columnWidth { get; set; }
public double listviewHeight { get; set; }
public double imageHeight { get; set; } 
}

public class Rent
{
	public string product_id { get; set; }
	public string user_id { get; set; }
	public string product_name { get; set; }
	public string product_description { get; set; }
	public string gender { get; set; }
	public string address { get; set; }
	public string lat { get; set; }
	public string @long { get; set; }
	public string type_of_bike { get; set; }
	public string product_image { get; set; }
	public string type { get; set; }
	public string condition { get; set; }
	public string currency { get; set; }
	public object price { get; set; }
	public string rent_time { get; set; }
	public string framesize { get; set; }
	public string add_to_top { get; set; }
	public string created_at { get; set; }
public int height { get; set; }
public double width { get; set; }
public double columnWidth { get; set; }
public double listviewHeight { get; set; }
public double imageHeight { get; set; } 
}

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
	public List<Sell> sell { get; set; }
	public List<Rent> rent { get; set; }
}
	}
}
