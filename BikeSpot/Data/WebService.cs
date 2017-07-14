using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.SecureStorage;
using Xamarin.Forms;

namespace BikeSpot
{
	public static class WebService
	{
		static HttpClient client = new HttpClient();
		public static LoginModel SignUp(UserModel usermodel)
		{

			UserModel um = new UserModel();
			LoginModel _loginModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "signup");
				j.Add("is_social_signup", usermodel.is_social_signup);
				j.Add("name", usermodel.name);
				j.Add("email", usermodel.email);
				j.Add("password", usermodel.password);
				j.Add("device_type", "ios");
				j.Add("deviceId", StaticDataModel.DeviceToken);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);



						um.responseMessage = jObj["responseMessage"].ToString();
						um.result = jObj["result"].ToString();
						if (um.result == "success")
						{
							_loginModel = jObj["userdata"].ToObject<LoginModel>();

						}

					}

				}
				return _loginModel;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static LoginModel Login(UserModel usermodel)
		{

			UserModel um = new UserModel();
			LoginModel _loginModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "login");
				j.Add("email", usermodel.name);
				j.Add("password", usermodel.password);
				j.Add("device_type", "ios");
				j.Add("device_id", StaticDataModel.DeviceToken);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);



						um.responseMessage = jObj["responseMessage"].ToString();
						um.result = jObj["result"].ToString();
						if (um.result == "success")
						{
							_loginModel = jObj["data"].ToObject<LoginModel>();

						}
						else
						{
							StaticMethods.ShowToast(um.responseMessage);
						}

					}

				}
				return _loginModel;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static UserModel ForgotPassword(string email)
		{

			UserModel um = new UserModel();
			LoginModel _loginModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "forgot_password");
				j.Add("email", email);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);



						um.responseMessage = jObj["responseMessage"].ToString();
						um.result = jObj["result"].ToString();


					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static UserModel ChangeEmail(string email)
		{

			UserModel um = new UserModel();
			LoginModel _loginModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "change_email");
				j.Add("user_id", StaticDataModel.userId);
				j.Add("email", email);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);



						um.responseMessage = jObj["responseMessage"].ToString();
						um.result = jObj["result"].ToString();


					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static UserModel ChangePassword(string oldPassword, string newPassword)
		{

			UserModel um = new UserModel();
			LoginModel _loginModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "change_password");
				j.Add("user_id", StaticDataModel.userId);
				j.Add("old_password", oldPassword);
				j.Add("new_password", newPassword);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);



						um.responseMessage = jObj["responseMessage"].ToString();
						um.result = jObj["result"].ToString();


					}

				}
				return um;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static string AddProduct(ProductModel pm)
		{

			UserModel um = new UserModel();
			string ret = string.Empty;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "add_product");
				j.Add("user_id", pm.user_id);
				j.Add("product_name", pm.product_name);
				j.Add("address", pm.address);
				j.Add("type_of_bike", pm.type_of_bike);
				j.Add("product_description", pm.product_description);
				j.Add("product_image", pm.product_image);
				j.Add("extension", pm.extension);
				j.Add("type", pm.type);
				j.Add("condition", pm.condition);
				j.Add("currency", pm.currency);
				j.Add("price", pm.price);
				j.Add("rent_time", pm.rent_time);
				j.Add("gender", pm.gender);
				j.Add("is_facebook_sharable", pm.is_facebook_sharable);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;

				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);



						um.responseMessage = jObj["responseMessage"].ToString();
						um.result = jObj["result"].ToString();
						if (um.result == "success")
						{
							ret = um.result;

						}

					}

				}
				return ret;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
		}
		public static async Task<string> AddProductUsingMultipart(ProductModel pm)
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
			try
			{
				

				//using (var client = new HttpClient())
				//{
				//client.MaxResponseContentBufferSize=int.MaxValue;
				using (var content = new MultipartFormDataContent())
				{

					content.Add(new StringContent("add_product"), "method");
					content.Add(new StringContent(pm.user_id.ToString()), "user_id");
					//content.Add(new StringContent("3"), "user_id");
					content.Add(new StringContent(pm.product_name), "product_name");
					content.Add(new StringContent(pm.address), "address");
					content.Add(new StringContent(pm.type_of_bike), "type_of_bike");
					content.Add(new StringContent(pm.product_description), "product_description");
					content.Add(new StringContent(pm.type.ToString()), "type");
					content.Add(new StringContent(pm.condition), "condition");
					content.Add(new StringContent(pm.currency), "currency");
					content.Add(new StringContent(pm.size), "framesize");
					content.Add(new StringContent(pm.price), "price");
					content.Add(new StringContent(pm.gender), "rent_time");
					content.Add(new StringContent(pm.gender), "gender");
					content.Add(new StringContent(pm.is_facebook_sharable.ToString()), "is_facebook_sharable");

					//var str = Convert.ToBase64String(pm.imageByteArray[0]);

					if (pm.imageByteArray[0] != null)
						content.Add(new StreamContent(new MemoryStream(pm.imageByteArray[0])), "img1", "img1.jpg");
					if (pm.imageByteArray[1] != null)
						content.Add(new StreamContent(new MemoryStream(pm.imageByteArray[1])), "img2", "img2.jpg");
					if (pm.imageByteArray[2] != null)
						content.Add(new StreamContent(new MemoryStream(pm.imageByteArray[2])), "img3", "img3.jpg");
					if (pm.imageByteArray[3] != null)
						content.Add(new StreamContent(new MemoryStream(pm.imageByteArray[3])), "img4", "img4.jpg");
					client.Timeout = TimeSpan.FromSeconds(300);

					client.PostAsync(Constants.AddProductUrl, content).ContinueWith((arg) =>
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							StaticMethods.ShowToast("File uploaded successfully");
							StaticMethods.DismissLoader();
						});
						tcs.SetResult("SUCCESS");
					});
					//var message = await client.PostAsync(Constants.AddProductUrl, content);

					client.Timeout = TimeSpan.FromSeconds(300);

					/*if (message.IsSuccessStatusCode)
					{
						StaticMethods.ShowToast("File uploaded successfully");
						return "SUCCESS";
					}
					else
					{
						StaticMethods.ShowToast("Failed to upload file. Please try again after some time.");
					}*/

					//var input = await message.Content.ReadAsStringAsync();

					return tcs.Task.Result;
					//return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;



				}

			}

			catch (Exception ex)
			{
			}
			finally
			{
				StaticMethods.DismissLoader();
			}
			return tcs.Task.Result;
		}

		public static Product GetProductDetailsbyId(int product_id)
		{

			Product _productModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_product_by_id");
				j.Add("product_id", product_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["data"];

						if (jObj["result"].ToString() == "success")
						{
							foreach (var item in data)
							{

								_productModel = item.ToObject<Product>();

							}

						}

					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return _productModel;
		}
		public static AutoCompleteModel GetAutoCompleteLocation(string text)
		{

			string[] strPredictiveText = null;
			AutoCompleteModel model = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;

				response = client.GetAsync(Constants.strAutoCompleteGoogleApi + text + "&key=" + Constants.apiKey).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						Debug.WriteLine(contents);
						JObject jObj = JObject.Parse(contents);

						model = JsonConvert.DeserializeObject<AutoCompleteModel>(contents);

					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return model;
		}
		public static List<Product> GetAllProductList()
		{
			List<Product> _product = new List<Product>();
			UserModel um = new UserModel();
			ProductListModel _productModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_all_product");

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["data"];

						if (jObj["result"].ToString() == "success")
						{
							foreach (var item in data)
							{
								_product.Add(new Product
								{
									list = item.ToObject<List<Product>>()
								});

							}

						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return _product;
		}
        public static List<Product> SearchProduct(string searchValue)
        {
            List<Product> _product = new List<Product>();
            UserModel um = new UserModel();
            ProductListModel _productModel = null;
            try
            {
                string url = Constants.BaseUrl;
                HttpResponseMessage response = null;
                JObject j = new JObject();
                j.Add("method", "get_productsearch");
                j.Add("searchval", searchValue); 

                var json = JsonConvert.SerializeObject(j);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {

                    using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
                    {
                        var contents = reader.ReadToEnd();
                        JObject jObj = JObject.Parse(contents);
                        var data = jObj["data"]; 

                        if (jObj["result"].ToString() == "success")
                        {
                            foreach (var item in data)
                            {
                                _product.Add(new Product
                                {
                                    list = item.ToObject<List<Product>>()
                                });

                            }

                        }


                    }

                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            finally
            {
                StaticMethods.DismissLoader();

            }
            return _product;
        }
		public static List<Product> FilterProducts(List<string> type_of_bike, List<string> condition, List<string> type,
												   int min_price, int max_price,
												   int max_distance, List<string> gender,
												   List<string> framesize, double lat, double @long)
		{
			List<Product> _product = new List<Product>();
			UserModel um = new UserModel();
			ProductListModel _productModel = null;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "filter");
				j.Add("type_of_bike", string.Join(",", type_of_bike.ToArray()));
				j.Add("min_price", min_price);
				j.Add("max_price", max_price);
				j.Add("condition", string.Join(",", condition.ToArray()));
				j.Add("type", string.Join(",", type.ToArray()));
				j.Add("distance", max_distance);
				j.Add("gender", string.Join(",", gender.ToArray()));
				j.Add("framesize", string.Join(",", framesize.ToArray()));
				j.Add("lat", lat);
				j.Add("long", @long);

				Debug.WriteLine(string.Join(",", type_of_bike.ToArray()));
				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["data"];

						if (jObj["result"].ToString() == "success")
						{
							foreach (var item in data)
							{
								_product.Add(new Product
								{
									list = item.ToObject<List<Product>>()
								});

							}

						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return _product;
		}
		public static CommentModel GetCommentsList(int product_id)
		{
			CommentModel _comments = new CommentModel();
			UserModel um = new UserModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_comments_by_product_id");
				j.Add("product_id", product_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["comments"];

						if (jObj["result"].ToString() == "success")
						{

							_comments.comments = data.ToObject<List<CommentModel.Comment>>();
							//_comments.co = data["comment_reply"].ToObject<List<CommentModel.CommentReply>>();


						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return _comments;
		}
		public static string AddComment(int comment_for, string description)
		{
			string ret = string.Empty;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "product_comment");
				j.Add("comment_by", StaticDataModel.userId);
				j.Add("comment_for", comment_for);
				j.Add("description", description);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["comments"];

						ret = jObj["result"].ToString();


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static string CommentReply(string msg, int comment_id)
		{
			string ret = string.Empty;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "comment_reply");
				j.Add("reply_by", StaticDataModel.userId);
				j.Add("comment_id", comment_id);
				j.Add("reply", msg);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["comments"];

						ret = jObj["result"].ToString();


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static string SubmitReviews(int rating, string review)
		{
			string ret = string.Empty;
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "review");
				j.Add("user_id", StaticDataModel.userId);
				j.Add("review", review);
				j.Add("rating", rating);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["comments"];

						ret = jObj["result"].ToString();


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static ProfileModel GetProfile(int user_id)
		{
			ProfileModel profileData = new ProfileModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "profile");
				j.Add("user_id", user_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							var data = jObj["data"];
							profileData.data = data.ToObject<List<ProfileModel.Datum>>();


						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return profileData;
		}
		public static ProfileModel UpdateProfilePic(string pic, string ext)
		{
			ProfileModel profileData = new ProfileModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "update_user_pic");
				j.Add("pic", pic);
				j.Add("ext", ext);
				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{

							var profilepic = jObj["profile-pic"].ToString();
							if (!string.IsNullOrEmpty(profilepic))
								CrossSecureStorage.Current.SetValue("profilePic", profilepic);
						}
						else
						{
							StaticMethods.ShowToast("Problem on uploading profile picture.");
						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return profileData;
		}
		public static string UpdateProfile(ProfileModel.Datum model)
		{
			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "edit_profile");
				j.Add("user_id", StaticDataModel.userId);
				j.Add("email", model.email);
				j.Add("name", model.name);
				j.Add("contact_number", model.mobile_no);
				j.Add("website_url", model.website_url);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{


							ret = "success";
						}



					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static CategoryModel GetCategoriesProductbyUserId(int user_id)
		{
			CategoryModel categoryModel = new CategoryModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_product_by_user_id");
				j.Add("user_id", user_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							var dataSell = jObj["data"]["sell"];
							var dataRent = jObj["data"]["rent"];
							//categoryModel.data.sell = dataSell.ToObject<List<CategoryModel.Data>>();
							//categoryModel.data.rent = dataRent.ToObject<List<CategoryModel.Data.>>();

						}

					}
				}
			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return categoryModel;
		}
		public static QuestionAnswerModel GetWizardQuestionAnswers()
		{
			QuestionAnswerModel qa = new QuestionAnswerModel();
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_wizard_que_ans");


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							qa.data = jObj["data"].ToObject<List<QuestionAnswerModel.QuestionAnswer>>();
						}

					}
				}
			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return qa;
		}
		public static WizardResultModel GetWizardResult(List<string> answer)
		{
			WizardResultModel wr = new WizardResultModel();
			try
			{

				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_wizard_result_ios");
				j.Add("answers", string.Join(",", answer.ToArray()));

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							wr.data = jObj["data"].ToObject<List<WizardResultModel.Answer>>();
						}

					}
				}
			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return wr;
		}
		public static SavedUserModel CheckSavedUser(string savedUserId)
		{
			SavedUserModel sa = new SavedUserModel();
			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "check_saved_user_by_id");
				j.Add("user_id", StaticDataModel.userId);
				j.Add("saved_user_id", savedUserId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							sa.data = jObj["data"].ToObject<List<SavedUserModel.Data>>();
						}

					}
				}
			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return sa;
		}
		public static string SaveUnsaveUser(string savedUserId, string method_name)
		{
			SavedUserModel sa = new SavedUserModel();
			string ret = string.Empty;
			try
			{
				string url = Constants.BaseUrl;

				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", method_name);
				j.Add("user_id", StaticDataModel.userId);
				j.Add("saved_user_id", savedUserId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							ret = "success";
						}

					}
				}
			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static MyProfileDataModel.Data GetProductByUserId()
		{
			MyProfileDataModel.Data _product = new MyProfileDataModel.Data();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_product_by_user_id");
				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["data"];

						if (jObj["result"].ToString() == "success")
						{

							_product.sell = data["sell"].ToObject<List<MyProfileDataModel.Sell>>();
							_product.rent = data["rent"].ToObject<List<MyProfileDataModel.Rent>>();
							_product.sold = data["sold"].ToObject<List<MyProfileDataModel.Sold>>();

						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return _product;
		}
		public static MyProfileDataModel.Data GetBuyingProductByUserId()
		{
			MyProfileDataModel.Data _product = new MyProfileDataModel.Data();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "user_buyingdetails");
				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["data"];

						if (jObj["result"].ToString() == "success")
						{


							_product.rent = data["rent"].ToObject<List<MyProfileDataModel.Rent>>();
							_product.sold = data["sold"].ToObject<List<MyProfileDataModel.Sold>>();

						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return _product;
		}
		public static SavedUserModel GetSavedUser()
		{
			SavedUserModel savedUserModel = new SavedUserModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_saved_user_by_user_id");
				j.Add("user_id", StaticDataModel.userId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var data = jObj["data"];

						if (jObj["result"].ToString() == "success")
						{

							savedUserModel.data = data.ToObject<List<SavedUserModel.Data>>();


						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return savedUserModel;
		}
		public static ChatUserModel GetChatUserList()
		{
			ChatUserModel chatModel = new ChatUserModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_chat_user_list");
				j.Add("sender_user_id", "13");

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							var data = jObj["data"];
							chatModel.data = data.ToObject<List<ChatUserModel.Datum>>();


						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return chatModel;
		}
		public static SaveChatUserModel SaveChatUser(int reciever_id, int product_id)
		{
			SaveChatUserModel chatUserModel = new SaveChatUserModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "save_chat_users");
				j.Add("sender_user_id", StaticDataModel.userId);
				j.Add("reciever_user_id", reciever_id);
				j.Add("product_id", product_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
							var chatUserId = jObj["chat_user_id"];
							var offerStatus = jObj["offer_status"];
							var msg = jObj["msg"];
							if (!ReferenceEquals(chatUserId, null))
							{
								chatUserModel.chat_user_id = Convert.ToInt32(chatUserId.ToString());
							}
							if (!ReferenceEquals(offerStatus, null))
							{
								chatUserModel.offer_status = Convert.ToInt32(offerStatus.ToString());
							}
							if (!ReferenceEquals(msg, null)){
								chatUserModel.msg = msg.ToString();
							}
						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return chatUserModel;
		}
		public static string SaveChatMessage(int reciever_id, int product_id, string msg, double offer_price)
		{
			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "save_chat_messages");
				j.Add("sender_id", StaticDataModel.userId);
				j.Add("reciever_id", reciever_id);
				j.Add("product_id", product_id);
				j.Add("message", msg);
				j.Add("offer_price", offer_price);
				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{

							ret = "success";
						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		//Using form data
		public static ChatModel GetAllChatMessage(int reciever_id, int product_id)
		{
			string ret = string.Empty;
			ChatModel chatModel = new ChatModel();
			try
			{
				string url = Constants.GetChatMessagesUrl;

				Dictionary<string, string> parameters = new Dictionary<string, string>();

				parameters.Add("method", "get_chat_messages");
				parameters.Add("sender_id", StaticDataModel.userId.ToString());
				parameters.Add("reciever_id", reciever_id.ToString());
				parameters.Add("product_id", product_id.ToString());
				using (var httpClient = new HttpClient())
				{
					using (var content = new FormUrlEncodedContent(parameters))
					{
						content.Headers.Clear();
						content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

						var response1 = httpClient.PostAsync(url, content);


						using (StreamReader reader = new StreamReader(response1.Result.Content.ReadAsStreamAsync().Result))
						{
							var contents = reader.ReadToEnd();
							JObject jObj = JObject.Parse(contents);
							if (jObj["result"].ToString() == "success")
							{

								var data = jObj["data"];
								chatModel.data = data.ToObject<List<ChatModel.Datum>>();

							}

						}

					}
				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return chatModel;
		}
		public static OfferStatusModel SaveOfferStatus(int reciever_id, int product_id, double offer_price,string status)
		{
OfferStatusModel offerStatusModel = new OfferStatusModel();

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "save_offer_status");
				j.Add("sender_id", StaticDataModel.userId);
				j.Add("reciever_id", reciever_id);
				j.Add("product_id", product_id);
				j.Add("offer_price", offer_price);
				j.Add("status", status);
				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{
                            offerStatusModel.result = jObj["result"].ToString();
							offerStatusModel.responseMessage = jObj["responseMessage"].ToString();
							offerStatusModel.offer_id = Convert.ToInt32(jObj["offer_id"].ToString());
							offerStatusModel.notification_response = jObj["notification_response"].ToString();
						}
                        else
                        {
							offerStatusModel.result = jObj["result"].ToString();
                            offerStatusModel.responseMessage = jObj["responseMessage"].ToString();
                        }


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return offerStatusModel;
		}
public static string DeleteProductByProductId(string product_id)
{
	string ret = string.Empty;
	try
	{
		string url = Constants.BaseUrl;
		HttpResponseMessage response = null;
		JObject j = new JObject();
		j.Add("method", "delete_product");
		j.Add("user_id", StaticDataModel.userId);
		j.Add("product_id", product_id);
		

		var json = JsonConvert.SerializeObject(j);
		var content = new StringContent(json, Encoding.UTF8, "application/json");
		response = client.PostAsync(url, content).Result;
		if (response.IsSuccessStatusCode)
		{

			using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
			{
				var contents = reader.ReadToEnd();
				JObject jObj = JObject.Parse(contents);
				
				ret = jObj["result"].ToString(); 


			}

		}

	}
	catch (Exception ex)
	{

		Debug.WriteLine(@"ERROR {0}", ex.Message);
	}
	finally
	{
		StaticMethods.DismissLoader();

	}
	return ret;
		}
        public static ReviewModel GetMyReviews()
        {
            ReviewModel rm = new ReviewModel();


            try
            {
                string url = Constants.BaseUrl;
                HttpResponseMessage response = null;
                JObject j = new JObject();
                j.Add("method", "get_my_review");
                j.Add("user_id", StaticDataModel.userId);

                var json = JsonConvert.SerializeObject(j);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {

                    using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
                    {
                        var contents = reader.ReadToEnd();
                        JObject jObj = JObject.Parse(contents);
                        if (jObj["result"].ToString() == "success")
                        {
                            var data = jObj["review_data"];
                            rm.review_data = data.ToObject<List<ReviewModel.ReviewData>>();


                        }


                    }

                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            finally
            {
                StaticMethods.DismissLoader();

            }
            return rm;
        }
		public static string GetPaypalId(string productUserId)
		{
			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_paypaldetails");
                j.Add("productuser_id",productUserId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{

                            ret = jObj["paypal_id"].ToString();
						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
		public static string SavePaypalId(string paypal_id)
		{
			string ret = string.Empty;

			try
			{
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "save_paypaldetails");
				j.Add("user_id", StaticDataModel.userId);
                j.Add("paypal_id",paypal_id);  


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(url, content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						if (jObj["result"].ToString() == "success")
						{

							ret = "success";
						}


					}

				}

			}
			catch (Exception ex)
			{

				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			finally
			{
				StaticMethods.DismissLoader();

			}
			return ret;
		}
	}
}



