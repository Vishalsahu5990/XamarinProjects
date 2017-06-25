using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class CommentModel
	{

		public string result { get; set; }
		public List<Comment> comments { get; set; }
		public int responseCode { get; set; }



		public class Comment
		{
			public string comment_id { get; set; }
			public string comment_by { get; set; }
			public string comment_for { get; set; }
			public string description { get; set; }
			public string created_at { get; set; }
			public string user_id { get; set; }
			public string name { get; set; }
			public string profile_pic { get; set; }
			public string email { get; set; }
			public string product_id { get; set; }
			public string product_name { get; set; }
			public string product_description { get; set; }
			public string product_image { get; set; }
            public bool isVisibleListView { get; set; }
			public bool isProductAdminn { get; set; }
			public double ListViewHeight { get; set; }
			public List<CommentReply> comment_reply { get; set; }

		}
		public class CommentReply
		{
			public string reply_id { get; set; }
			public string comment_id { get; set; }
			public string reply_by { get; set; }
			public string reply { get; set; }
			public string created_at { get; set; }
			public string name { get; set; }
			public string profile_pic { get; set; }


		}
	}
}
