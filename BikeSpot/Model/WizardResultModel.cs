using System;
using System.Collections.Generic;

namespace BikeSpot
{
	public class WizardResultModel
	{
		public string result { get; set; }
		public List<Answer> data { get; set; }
		public int responseCode { get; set; }
		public class Answer
		{
			public string id { get; set; }
			public string type_of_bike { get; set; }
			public string type_of_bike_german { get; set; }
			public string description { get; set; }
			public string description_german { get; set; }
			public string attachment { get; set; }
			public string created_at { get; set; }
		}
	}
}
