using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using Xamarin.Auth;
using Xamarin.Forms;

namespace BikeSpot
{
	public partial class RegistrationPage : ContentPage
	{
		Account account;
		AccountStore store;
		LoginModel model = null;
		bool flag = false;
		public RegistrationPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			store = AccountStore.Create();
			account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();


			btnLogin.Clicked += BtnLogin_Clicked;
			btnRegister.Clicked += BtnRegister_Clicked;
			txtUsername.Focused += TxtUsername_Focused;
			txtEmail.Focused += TxtEmail_Focused;
			txtPass.Focused += TxtPass_Focused;
			txtConPass.Focused += TxtConPass_Focused;
			txtEmail.TextChanged += TxtEmail_TextChanged;
			if (StaticMethods.IsIpad())
                SetupIpadUI();
		}
		private void SetupIpadUI()
		{
			_slMainLayout.Padding = new Thickness(70);
			lblRegistration.FontSize = 30;
            lblRegistration.Margin = 20;
			_slEmail.HeightRequest = 70;
			_slName.HeightRequest = 70;
			_slPassword.HeightRequest = 70;
			_slConfirmPassword.HeightRequest = 70;
			lblFogotPassword.FontSize = 25;
			btnLogin.HeightRequest = 70;
			btnRegister.HeightRequest = 70;
			btnLogin.FontSize = 25;
			btnRegister.FontSize = 25;
			_slLeftName.WidthRequest = 70;
			_slLeftEmail.WidthRequest = 70;
			_slLeftPassword.WidthRequest = 70;
			_slLeftConfirmPassword.WidthRequest = 70;
			imgLogo.Margin = new Thickness(80, 0, 80, 0);
		}
		void TxtUsername_Focused(object sender, FocusEventArgs e)
		{
			txtUsername.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtEmail_Focused(object sender, FocusEventArgs e)
		{
			txtEmail.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtPass_Focused(object sender, FocusEventArgs e)
		{
			txtPass.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtConPass_Focused(object sender, FocusEventArgs e)
		{
			txtConPass.PlaceholderColor = Color.FromHex("#C1C1C1");
		}

		void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(txtEmail.Text))
			{
				if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
				{
					txtEmail.TextColor = Color.Red;

				}
				else
				{
					txtEmail.TextColor = Color.Black;
				}
			}
		}

		async void forgotPasswordTapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ForgotPasswordPage());
		}

		async void fbTapped(object sender, EventArgs e)
		{
			flag = true;
			var authenticator = new OAuth2Authenticator(
							clientId: Constants.ClientIdFacebook,
						   scope: "public_profile", // the scopes for the particular API you're accessing, delimited by "+" symbols
						   authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
						   redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}

		async void googleplusTapped(object sender, EventArgs e)
		{
			flag = false;
			var authenticator = new OAuth2Authenticator(
							Constants.ClientId,
							Constants.ClientSecret,
							Constants.Scope,
							new Uri(Constants.AuthorizeUrl),
							new Uri(Constants.RedirectUrl),
							new Uri(Constants.AccessTokenUrl));

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}
		async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
			StaticMethods.ShowLoader();
			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			try
			{

				if (e.IsAuthenticated)
				{
					// If the user is authenticated, request their basic user data from Google
					var UserInfoUrlFacebook = "https://graph.facebook.com/me?fields=email,first_name,last_name";
					var UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
					var GetUserDataUrl = string.Empty;
					if (flag)
						GetUserDataUrl = UserInfoUrlFacebook;
					else
						GetUserDataUrl = UserInfoUrl;

					var request = new OAuth2Request("GET", new Uri(GetUserDataUrl), null, e.Account);
					var response = await request.GetResponseAsync();
					if (response != null)
					{
						// Deserialize the data and store it in the account store
						// The users email address will be used to identify data in SimpleDB
						string userJson = await response.GetResponseTextAsync();
						if (userJson != "Not Found")
							App.User = JsonConvert.DeserializeObject<User>(userJson);
					}

					if (account != null)
					{
						store.Delete(account, Constants.AppName);
					}

					store.Save(account = e.Account, Constants.AppName);
					if (App.User.Email != null)
						SignUp(App.User).Wait();
					//App.Current.MainPage = new NavigationPage(new MainPage());
				}
			}
			catch (Exception ex)
			{

			}
			finally
			{
				StaticMethods.DismissLoader();
			}
			//Navigation.InsertPageBefore(new HomePage(), this);
			//await Navigation.PopToRootAsync();
		}


		void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;

			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			Debug.WriteLine("Authentication error: " + e.Message);
		}
		async void BtnLogin_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}

		async void BtnRegister_Clicked(object sender, EventArgs e)
		{
			if (Isvalidated())
			{
				SignUp().Wait();
			}
		}

		private bool Isvalidated()
		{

			try
			{
				if (string.IsNullOrEmpty(txtUsername.Text))
				{
					txtUsername.PlaceholderColor = Color.Red;
					return false;
				}

				else if (string.IsNullOrEmpty(txtEmail.Text))
				{
					txtEmail.PlaceholderColor = Color.Red;
					return false;
				}

				else if (string.IsNullOrEmpty(txtPass.Text))
				{
					txtPass.PlaceholderColor = Color.Red;
					return false;
				}
				else if (string.IsNullOrEmpty(txtConPass.Text))
				{
					txtConPass.PlaceholderColor = Color.Red;
					return false;
				}
				else if (txtPass.Text != txtConPass.Text)
				{
					txtConPass.TextColor = Color.Red;
					return false;
				}
				else if (!string.IsNullOrEmpty(txtEmail.Text))
				{
					if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
					{
						txtEmail.PlaceholderColor = Color.Red;
						StaticMethods.ShowToast("Invalid email.");
						return false;
					}
					else
					{
						return true;
					}
				}


				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
			finally
			{

			}
		}
		private async Task SignUp()
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						um = new UserModel();
						um.is_social_signup = 0;
						um.email = txtEmail.Text;
						um.name = txtUsername.Text;
						um.password = txtPass.Text;
						model = WebService.SignUp(um);


					}).ContinueWith(async
					t =>
					{
						if (model != null)
						{
							StaticDataModel.userId = Convert.ToInt32(model.user_id);
							StaticMethods.SaveLocalData(model);

							StaticMethods.ShowToast("Login successfully");
					        StaticDataModel.IsAnonymousLogin = false;
							App.Current.MainPage = new NavigationPage(new MainPage());

						}
						else
						{
							StaticMethods.ShowToast("User already exists.");
						}

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task SignUp(User user)
		{
			UserModel um = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						um = new UserModel();
						um.is_social_signup = 1;
						um.email = user.Email;
						if (user.Name != null)
							um.name = user.Name;
						else
							um.name = user.First_Name + "" + user.Last_Name;
						um.password = "123123";
						model = WebService.SignUp(um);


					}).ContinueWith(async
					t =>
					{
						if (model != null)
						{
							StaticDataModel.userId = Convert.ToInt32(model.user_id);
							StaticMethods.SaveLocalData(model);
							StaticMethods.ShowToast("Login successfully");
					StaticDataModel.IsAnonymousLogin = false;
							App.Current.MainPage = new NavigationPage(new MainPage());

						}
						else
						{
							StaticMethods.ShowToast("User already exists.");
						}

						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
