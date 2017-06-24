using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace BikeSpot
{
	public interface IiOSMethods
	{
		void ShareContent(string client_id,string url);
		Task<List<ImageSource>> MultiImagePicker();
		void Call(string contact_no);
	}
}
