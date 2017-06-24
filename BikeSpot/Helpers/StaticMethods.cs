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
				um.profile_pic = CrossSecureStorage.Current.GetValue("profile_Pic", null);

				um.contact_number = CrossSecureStorage.Current.GetValue("contact_number", null);
				um.website_url = CrossSecureStorage.Current.GetValue("website_url", null);
				um.is_admin = CrossSecureStorage.Current.GetValue("is_admin", null);
				um.is_retailer = CrossSecureStorage.Current.GetValue("is_retailer", null);

				um.code = CrossSecureStorage.Current.GetValue("code", null);
				um.approve = CrossSecureStorage.Current.GetValue("approve", null);
				um.help_url = CrossSecureStorage.Current.GetValue("help_url", null);
				um.owner_email = CrossSecureStorage.Current.GetValue("owner_email", null);
				um.owner_phone_number = CrossSecureStorage.Current.GetValue("owner_phone_number", null);
				um.is_social_signup = Convert.ToInt32(CrossSecureStorage.Current.GetValue("is_social_signup", null));

				return um;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public static void SaveLocalData(LoginModel login_model)
		{

			try
			{
				CrossSecureStorage.Current.SetValue("userId", login_model.user_id.ToString());
				CrossSecureStorage.Current.SetValue("profile_Pic", login_model.profile_pic.ToString());
				CrossSecureStorage.Current.SetValue("name", login_model.name.ToString());
				CrossSecureStorage.Current.SetValue("email", login_model.email.ToString());

				CrossSecureStorage.Current.SetValue("contact_number", login_model.contact_number.ToString());
				CrossSecureStorage.Current.SetValue("website_url", login_model.website_url);
				CrossSecureStorage.Current.SetValue("is_admin", login_model.is_admin.ToString());
				CrossSecureStorage.Current.SetValue("is_retailer", login_model.is_retailer.ToString());

				CrossSecureStorage.Current.SetValue("code", login_model.code.ToString());
				CrossSecureStorage.Current.SetValue("approve", login_model.approve);
				CrossSecureStorage.Current.SetValue("help_url", login_model.help_url.ToString());
				CrossSecureStorage.Current.SetValue("owner_email", login_model.owner_email.ToString());
				CrossSecureStorage.Current.SetValue("owner_phone_number", login_model.owner_phone_number.ToString());
				CrossSecureStorage.Current.SetValue("is_social_signup", login_model.is_social_signup.ToString());

			}
			catch (Exception ex)
			{

			}
		}
	}
}
