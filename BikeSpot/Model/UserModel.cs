using System;
namespace BikeSpot
{
	public class UserModel
	{
		public string result { get; set; }
		public string responseMessage { get; set; }
		public int responseCode { get; set; }
		public int user_id { get; set; }
		public string email { get; set; }
		public string name { get; set; }
		public string password { get; set; }
		public string created_date { get; set; }
		public string profile_pic { get; set; }
		public int is_social_signup { get; set; }

public string contact_number { get; set; }
public string website_url { get; set; }

public string is_retailer { get; set; }
public string is_admin { get; set; }
public string code { get; set; }
public string approve { get; set; }


public string device_type { get; set; }
public string device_id { get; set; }
public string subscribed_on { get; set; }
public string expire_on { get; set; }
public string owner_phone_number { get; set; }
public string owner_email { get; set; }
public string help_url { get; set; }

	}
}
