using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class QuestionAnswerModel
	{
public string result { get; set; }
public List<QuestionAnswer> data { get; set; }
public int responseCode { get; set; }
		public class QuestionAnswer
		{
			public string id { get; set; }
			public string question { get; set; }
			public string question_german { get; set; }
			public string ans1 { get; set; }
			public string ans1_german { get; set; }
			public string ans2 { get; set; }
			public string ans2_german { get; set; }
			public string ans3 { get; set; }
			public string ans3_german { get; set; }
			public string order { get; set; }
			public string created_at { get; set; }

		}

	}
}
