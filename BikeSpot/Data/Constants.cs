using System;
namespace BikeSpot
{
	public static class Constants
	{
		public static readonly string BaseUrl = "http://risensys.com/bikespot/webservice/api.php";
		public static readonly string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
		public static readonly string apiKey = "AIzaSyBMdRUum6X87xG5QD3m0s-w8QVjDYSVFqE";
		public static readonly string ImageUrl = "http://risensys.com/bikespot/uploads/";


public static string AppName = "Bikespot";
// AWS
// Sign up for an AWS account at https://aws.amazon.com/
// Configure at https://console.aws.amazon.com/cognito/
public static string CognitoIdentityPoolId = "<insert_id_here>";

// OAuth
// For Google login, configure at https://console.developers.google.com/
public static string ClientId = "250769118368-16no493rn1t2peo8af9k6bfi9vo3lejr.apps.googleusercontent.com";
public static string ClientIdFacebook = "1605074516186884";

public static string ClientSecret = "a20VJFFvLZK6Hj7v5bQFaKf0";

// These values do not need changing 
public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
public static string AccessTokenUrl = "https://accounts.google.com/o/oauth2/token";
public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v4/token";

// Set this property to the location the user will be redirected too after successfully authenticating
public static string RedirectUrl = "https://www.youtube.com/c/HoussemDellai/";
	}
}
