using Microsoft.AspNetCore.SignalR;

namespace ImigramAPI.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected: {Context.ConnectionId}");
            Console.WriteLine($"UserId: {Context.UserIdentifier}");

            await base.OnConnectedAsync();
        }
    }
}
