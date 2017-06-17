using System;
namespace BikeSpot
{
public class ChatModel
{
	public int Id { get; set; }
	public string Message { get; set; }
	public bool Incoming { get; set; }
	public bool Outgoing { get; set; }
	}
}
