using System;
namespace BikeSpot
{
	public static class Constants
	{
		public static readonly string BaseUrl = "http://risensys.com/bikespot/webservice/api.php";
		public static readonly string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
		public static readonly string apiKey = "AIzaSyBMdRUum6X87xG5QD3m0s-w8QVjDYSVFqE";
		public static readonly string ImageUrl = "http://risensys.com/bikespot/uploads/";
public static readonly string ProfilePicUrl = "http://risensys.com/bikespot/uploads/profile-pic/";
public static readonly string GetChatMessagesUrl = "http://risensys.com/bikespot/webservice/get_chat_messages/";


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
public static string Html_TypeofBike = @"<html>
<head>
	<title>BIKE ENGLISH</title>
	 <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
	 <style type=""text/css"">
	 	.wrapper {width: 100%;}
	 	.d p {text-align: justify;}
	 </style>
</head>
<body>

<div class=""wrapper"">
	<div class=""d"">
		<h2>Citybike : </h2>
		<p>Citybikes are mostly used to get to work, do shopping and make errands. They are equipped with medium to heavy frames and with helpful accessories. Citybikes often have a very low entrance to get on and off easily. The upright seating ensures comfortable driving at short distances.</p>
	</div>
	<div class=""d"">
		<h2>Mountainbike : </h2>
		<p>Mountain bikes are designed for use on the terrain. There are several subgroups of off-road bikes, such as cross-country, downhill and freeride bikes. Typically, they have sturdy frames and wheels, wide tire strips and wide handlebars. Many mountain bikes have suspension systems and hydraulic or mechanical brakes.</p>
	</div>

	<div class=""d"">
		<h2>Road Bike : </h2>
		<p>Racing bikes are mainly designed for high speeds on a fixed ground. Cyclocross bikes, timing machines, triathlon bikes and bicycles are also included in this category. They have lightweighted frames, handlebars that allow an areodynamic driving position and tires that tolerate high pressure and low rolling resistance.</p>
	</div>
	<div class=""d"">
		<h2>Touring Bike : </h2>
		<p>It is stable, comfortable and resilient. This means that it is, a mixture of mountain and city bikes. Trekking bicycles can be used in many ways - both as a sports equipment and as a means of transport in the city. They are designed for carrying luggage and have a quite comfortable, slightly tilted seat position, which also allows faster driving.</p>
	</div>
	<div class=""d"">
		<h2>Child’s Bike : </h2>
		<p>Even in recent years, balance, reaction and co-ordination skills should be trained. The balance bike and tricycle as well as the scooter offer the best approach to cycling. When the children are experienced the game wheel, which already has a pedal drive, can be used. Then the child can change to a child / youth bike.</p>
	</div>
	<div class=""d"">
		<h2>E-Bike : </h2>
		<p>E-bikes can drive without pedal support with the push of a button. This system is requiring licensing from six kilometers per hour. That's why e-bikes are rarely offered. The term for supporting bicycles is ""pedelec"". Nevertheless, many vendors refer to all the wheels with electrical support. It is true, however, that these bicycles do not support, but can also be driven completely independently..</p>
	</div>
	<div class=""d"">
		<h2>Pedelec : </h2>
		<p>Pedelecs only provide engine support when the driver steps into the pedals. If pedal support is up to 25 kilometers per hour, pedelecs are considered a bicycle and are not requiring licensing.</p>
	</div>
	
	<div class=""d"">
		<h2>Folding Bike : </h2>
		<p>Folding bicycles are bikes that can be brought to a very small size. They are therefore particularly well suited to take them on public transport or cars and are designed for short distances.</p>
	</div>
	<div class=""d"">
		<h2>BMX/Dirtbike : </h2>
		<p>Both wheel types are designed for stunts / tricks or for sprint races with steep curves and obstacles depending on the model. BMX have 20 inch wheels, no gear shift and no rim brakes. Dirtbikes, on the other hand, are a bit larger. In general, a dirtbike is intended for off-road use and a BMX for road use.</p>
	</div>
	<div class=""d"">
		<h2>Fatbike : </h2>
		<p>The Fatbikes have their roots in Alaska, where already in the middle of the 80's two rims were welded to a wide one. Nowadays these wheels are also available in Europe. The extremely wide tires distinguish the bike. This means you get through sand, mud and snow without problems.</p>
	</div>
	<div class=""d"">
		<h2>Cruiser: </h2>
		<p>These wheels are reminiscent of the style of the 1950s and combine lifestyle with design. The low frame provides a back-resting seating position, and they are designed for comfortable driving on different terrain. For the sporty, this bike is less recommendable, but cozy cyclists love the cruiser bikes.</p>
	</div>
	<div class=""d"">
		<h2>Singlespeed : </h2>
		<p>Singlespeed is a term for bikes that have only one gear. The reasons to renounce an arsenal of gears are very diverse. As a result, the wheel is less susceptible to defects, has less wear and is usually at a lower price. In addition, it looks tidier, which probably explains the singlespeed hype of the last years.</p>
	</div>
	<div class=""d"">
		<h2>Dutch Roadster : </h2>
		<p>These wheels are more for the comfort than the speed. They are similar to city bikes, but offer more comfort for longer distances. The rear-bent handlebar is very popular and sits comfortably in the hand. For cozy trips, Holland bicycles also provide enough space for luggage.</p>
	</div>
	<div class=""d"">
		<h2>Tandem : </h2>
		<p>Tandem refers to bicycles with space for more people sitting behind each other. Most tandems are designed for two people. All riders can pedal, only one can steer the tandem.</p>
	</div>

	<div class=""d"">
		<h2>Unicycle : </h2>
		<p>Unicycles are mainly used by artists, but they are also used for ball sports, such as unicycle hockey and unicycle baseball. They usually have a rigid drive and with exceptions no gear shifting. Wheel sizes range from 18 to 30 inches.</p>
	</div>

</div>


</body>
</html>";
public static string Html_SizeofBike = @"<html>
<head>
	<title>html</title>
	  <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
	<style type=""text/css"">
		table tr th {text-align: left;border: 1px solid #000;}	
		table tr td {text-align: left;border: 1px solid #000;}	
		
	</style>
</head>
<body>

<table align=""left"" style=""width:100%"">
	<h2>Hardtail Mountainbikes</h2>	
  <tr>
    <th>Body height/ Körpergröße</th>
    <th>Frame height(cm)/ Rahmenhöhe(cm)</th> 
    <th>Size-Category/ Größen-Kategorie</th>
  </tr>
  <tr>
    <td>1,65 – 1,75 m </td>
    <td>40 – 45 cm </td>
    <td>S – Small</td>
  </tr>
  <tr>
    <td>1,76 – 1,85 m </td>
    <td>46 – 50 cm </td>
    <td>M – Medium</td>
  </tr>
  <tr>
    <td>1,86 – 1,92 m </td>
    <td>51 – 54 cm </td>
    <td>L – Large</td>
  </tr>
  <tr>
    <td>1,93 – 2,00 m  </td>
    <td>ab 55 cm</td>
    <td>XL – Extra Large</td>
  </tr>
</table>
<table>
  	
  	<h2>Fullsuspension Mountainbikes</h2>	
  <tr>
    <th>Body height/ Körpergröße</th>
    <th>Frame height(cm)/ Rahmenhöhe(cm)</th> 
    <th>Size-Category/ Größen-Kategorie</th>
  </tr>
  <tr>
    <td>1,65 – 1,75 m </td>
    <td>38 – 42 cm  </td>
    <td>S – Small</td>
  </tr>
  <tr>
    <td>1,76 – 1,85 m </td>
    <td>43 – 46 cm  </td>
    <td>M – Medium</td>
  </tr>
  <tr>
    <td>ab 1,86 m  </td>
    <td>ab 47 cm  </td>
    <td>L – Large</td>
  </tr>
  <tr>
    <td>1,93 – 2,00 m  </td>
    <td>ab 55 cm</td>
    <td>XL – Extra Large</td>
  </tr>
</table>
<table>

  <br/>
  	<h2>Road Bikes/Rennräder</h2>


 <tr>
    <th>Body height/ Körpergröße</th>
    <th>Frame height(cm)/ Rahmenhöhe(cm)</th> 
    <th>Size-Category/ Größen-Kategorie</th>
  </tr>
  <tr>
    <td>1,55 – 1,65 m </td>
    <td>ca. 50 cm </td>
    <td>S – Small</td>
  </tr>
  <tr>
    <td>1,65 – 1,75 m </td>
    <td>ca. 53 cm  </td>
    <td>M – Medium</td>
  </tr>
  <tr>
    <td>1,75 – 1,85 m   </td>
    <td>ca. 55 cm </td>
    <td>L – Large</td>
  </tr>
  <tr>
    <td>ab 1,86 m   </td>
    <td>ca. 58 cm </td>
    <td>XL – Extra Large</td>
  </tr>
 </table>
 <table>
  
  <h2>Touring Bikes, Citybikes & other Types/Trekkingräder, Citybikes & andere Typen</h2>

 <tr>
    <th>Body height/ Körpergröße</th>
    <th>Frame height(cm)/ Rahmenhöhe(cm)</th> 
    <th>Size-Category/ Größen-Kategorie</th>
  </tr>
  <tr>
    <td>1,55 – 1,65 m </td>
    <td>40 – 45 cm  </td>
    <td>S – Small</td>
  </tr>
  <tr>		
    <td>1,65 – 1,75 m </td>
    <td>46 – 50 cm </td>
    <td>M – Medium</td>
  </tr>
  <tr>
    <td>1,76 – 1,85 m    </td>
    <td>50 – 55 cm </td>
    <td>L – Large</td>
  </tr>
  <tr>
    <td>ab 1,86 m   </td>
    <td>ab 56 cm  </td>
    <td>XL – Extra Large</td>
  </tr>

</table>



</body>
</html>";
	}
}
