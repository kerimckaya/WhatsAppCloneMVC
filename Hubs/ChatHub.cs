using BaseAPI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WhatsAppCloneMVC.Data;
using WhatsAppCloneMVC.Models;

public class ChatHub : Hub
{
    private readonly ChatContext _context;
    private readonly IUserService _userService;
    public ChatHub(ChatContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task SendMessageToUser(string targetUserName, string message, string token)
    {

        int senderId = _userService.GetUserIdWithToken(token);
        //int targetId = 2;
        var sender = await _context.Users.FindAsync(senderId);
        var receiver = await _context.Users.FirstOrDefaultAsync(x => x.ConnectionId == targetUserName);

        if (sender == null || receiver == null) return;

        var newMessage = new Message
        {
            Content = message,
            Timestamp = DateTime.Now,
            SenderId = sender.Id,
            ReceiverId = receiver.Id
        };

        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync();

        await Clients.User(sender.ConnectionId).SendAsync("ReceiveMessage", sender.ConnectionId, message);
    }

    public override Task OnConnectedAsync()
    {
       // var userId = Context.UserIdentifier;
        var user = _context.Users.FirstOrDefault(x => x.Id == int.Parse(Context.User.FindFirstValue("UserId")));
        user.ConnectionId = Context.ConnectionId;
        _context.Update(user);
        _context.SaveChanges();
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        // Log the disconnection
        return base.OnDisconnectedAsync(exception);
    }
}
