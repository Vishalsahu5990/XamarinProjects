using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class SavedUserModel
	{
public string result { get; set; }
public List<Data> data { get; set; }
public int responseCode { get; set; }
public string user_id { get; set; }
public string email { get; set; }
public string name { get; set; }
public string password { get; set; }
public string profile_pic { get; set; }
public string dob { get; set; }
public string gender { get; set; }
public string country { get; set; }
public string city { get; set; }
public string street { get; set; }
public string postal_code { get; set; }
public string contact_number { get; set; }
public string website_url { get; set; }
public string is_admin { get; set; }
public string is_retailer { get; set; }
public string is_social_signup { get; set; }
public string code { get; set; }
public string approve { get; set; }
public string device_type { get; set; }
public string device_id { get; set; }
public string subscribed_on { get; set; }
public string expire_on { get; set; }
public string created_date { get; set; }
public string lastsignedinon { get; set; }
public string resetsenton { get; set; }
public string resetpasscode { get; set; }
public class Data
{
	public string user_id { get; set; }
	public string email { get; set; }
	public string name { get; set; }
	public string password { get; set; }
	public string profile_pic { get; set; }
	public string dob { get; set; }
	public string gender { get; set; }
	public string country { get; set; }
	public string city { get; set; }
	public string street { get; set; }
	public string postal_code { get; set; }
	public string contact_number { get; set; }
	public string website_url { get; set; }
	public string is_admin { get; set; }
	public string is_retailer { get; set; }
	public string is_social_signup { get; set; }
	public string code { get; set; }
	public string approve { get; set; }
	public string device_type { get; set; }
	public string device_id { get; set; }
	public string subscribed_on { get; set; }
	public string expire_on { get; set; }
	public string created_date { get; set; }
	public string lastsignedinon { get; set; }
	public string resetsenton { get; set; }
	public string resetpasscode { get; set; }
	public bool user_is_favorite { get; set; }

}

	}
}
