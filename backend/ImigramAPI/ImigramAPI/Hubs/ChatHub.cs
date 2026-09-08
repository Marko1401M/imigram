using ImigramAPI.DTOs;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ImigramAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        public ChatHub(IMessageService messageService, IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _messageService = messageService;
            _userService = userService;
        }

        public async Task SendMessage(string chatId, string receiverId, string content)
        {
            var senderId = Context.UserIdentifier;

            var message = await _messageService.SendMessage(chatId, senderId, content);

            var user = await _userService.GetUserById(senderId);

            var notification = new MessageNotificationDto
            {
                Id = message.Id,
                ChatId = chatId,
                Content = content,
                IsRead = message.IsRead,
                SenderId = senderId,
                SenderFullName = user.FirstName + " " + user.LastName,
                SenderProfileImage = user.ProfileImage,
                SenderUsername = user.Username,
                SentAt = message.SentAt
            };

            await Clients.User(receiverId).SendAsync("ReceiveMessage", notification);

            await Clients.Caller.SendAsync("ReceiveMessage", notification);
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("CHAT HUB:");
            Console.WriteLine($"Connection: {Context.ConnectionId}");
            Console.WriteLine($"UserIdentifier: {Context.UserIdentifier}");
            Console.WriteLine($"IsAuthenticated: {Context.User?.Identity?.IsAuthenticated}");

            foreach (var claim in Context.User?.Claims ?? Enumerable.Empty<Claim>())
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            await base.OnConnectedAsync();
        }    }
}
