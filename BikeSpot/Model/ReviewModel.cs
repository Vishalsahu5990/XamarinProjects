using System;
using System.Collections.Generic;

namespace BikeSpot
{
    public class ReviewModel
    {
        public string responseMessage { get; set; }
        public string result { get; set; }
        public int responseCode { get; set; }
        public List<ReviewData> review_data { get; set; }

        public class ReviewData
        {
            public string review_id { get; set; }
            public string user_id { get; set; }
            public string productuser_id { get; set; }
            public string review { get; set; }
            public string rating { get; set; }
            public string created_at { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string profile_pic { get; set; }

			public string star1 { get; set; }
			public string star2 { get; set; }
			public string star3 { get; set; }
			public string star4 { get; set; }
			public string star5 { get; set; }

        }
    }
}
