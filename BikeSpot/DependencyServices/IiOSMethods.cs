using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeSpot
{
	public interface IiOSMethods
	{
		void ShareContent(string client_id,string url);
        Task<List<string>> MultiImagePicker();
	}
}
