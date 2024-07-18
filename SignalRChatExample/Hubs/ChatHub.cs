using Microsoft.AspNetCore.SignalR;
using SignalRChatExample.Models;

namespace SignalRChatExample.Hubs
{

	public interface IChatClient
	{
		Task GroupAnnouncement(string username, string message);
		Task SendMailToTheGroup(string username, string message, string groupname);
	}

	public class ChatHub : Hub<IChatClient>
	{

        public async Task JoinChat(UserConnection connection)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, connection.groupName);
			await Clients.Group(connection.groupName).GroupAnnouncement("Emil:", $"{connection.username} has connected to {connection.groupName}");
		}

		public async Task<string> SendMail(string username, string message, string groupname)
		{
			await Console.Out.WriteLineAsync(username + " " + message + " " + groupname);
			await Clients.Groups(groupname).SendMailToTheGroup(username, message, groupname);
			await Console.Out.WriteLineAsync($"Response:-----------------------");
			return username +": " + message;
		}

	}
}
