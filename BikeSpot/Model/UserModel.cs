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
		public int is_social_signup { get; set; }

	}
}
