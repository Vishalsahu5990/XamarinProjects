using System;
using System.IO;
using Acr.UserDialogs;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace BikeSpot
{
	public static class StaticMethods
	{

		public static bool IsIpad()
		{
			if (Device.Idiom == TargetIdiom.Phone)
				return false;
			else
				return true;
		}
		public static void ShowLoader()
		{
			
			try
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					UserDialogs.Instance.ShowLoading();

				}
				else
				{


				}
			}
			catch (Exception ex)
			{

			}
		}
		public static void DismissLoader()
		{
			
			try
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					UserDialogs.Instance.HideLoading();

				}
				else
				{


				}
			}
			catch (Exception ex)
			{

			}
		}
		public static void ShowToast(string msg)
		{
			
			try
			{
				UserDialogs.Instance.Toast(msg);
			}
			catch (Exception ex)
			{

			}
		}
public static byte[] StreamToByte(Stream input)
{
	try
	{


		using (MemoryStream ms = new MemoryStream())
		{
			input.CopyTo(ms);
			return ms.ToArray();
		}

	}
	catch (Exception ex)
	{
		return null;
	}
}
public static UserModel GetLocalSavedData()
{
	UserModel um = null;
	try
	{
		um = new UserModel();
		um.user_id = Convert.ToInt32(CrossSecureStorage.Current.GetValue("userId", null));
				um.name = CrossSecureStorage.Current.GetValue("name", null);
				um.email = CrossSecureStorage.Current.GetValue("email", null);


		return um;
	}
	catch (Exception ex)
	{
		return null;
	}
		}
	}
}
