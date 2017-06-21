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
			try
			{

				//using (var client = new HttpClient())
				//{
					//client.MaxResponseContentBufferSize=int.MaxValue;
					using (var content =
						new MultipartFormDataContent())
					{

						content.Add(new StringContent("add_product"), "method");
						//content.Add(new StringContent(pm.user_id.ToString()), "user_id");
						content.Add(new StringContent("3"), "user_id");
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
						using (


						   var message =
                        await client.PostAsync("http://risensys.com/bikespot/webservice/add_product", content))
						{
						client.Timeout = TimeSpan.FromSeconds(300);
						StaticMethods.DismissLoader();
						if (message.IsSuccessStatusCode)
						{
							StaticMethods.ShowToast("File uploaded successfully");

						} 
						else
						{ 
							StaticMethods.ShowToast("Failed to upload file. Please try again after some time.");
						}

							var input = await message.Content.ReadAsStringAsync();

							return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
						}
					}
				

			}

			catch (Exception ex)
			{

			}
			finally
			{
				StaticMethods.DismissLoader();
			}
			return null;
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
		public static List<Product> FilterProducts(List<string> type_of_bike, List<string> condition, List<string> type, int min_price, int max_price, int max_distance, string gender, double lat, double @long)
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
				j.Add("gender", gender);
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
		public static List<CommentModel> GetCommentsList(int product_id)
		{
			List<CommentModel> _comments = new List<CommentModel>();
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

							_comments = data.ToObject<List<CommentModel>>();



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
				string[] arrayAnswer = answer.Select(i => i.ToString()).ToArray();
				string url = Constants.BaseUrl;
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("method", "get_wizard_result");
				j.Add("answers", JsonConvert.SerializeObject(arrayAnswer));

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

	}
}
