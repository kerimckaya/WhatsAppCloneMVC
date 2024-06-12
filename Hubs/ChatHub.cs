using Microsoft.AspNetCore.SignalR;
using WhatsAppCloneMVC.Data;
using WhatsAppCloneMVC.Models;

public class ChatHub : Hub
{
    private readonly ChatContext _context;

    public ChatHub(ChatContext context)
    {
        _context = context;
    }

    public async Task SendMessageToUser(string targetUserId, string message)
    {

        int senderId = 1;
        int targetId = 2;
        var sender = await _context.Users.FindAsync(senderId);
        var receiver = await _context.Users.FindAsync(targetId);

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

        await Clients.User(targetUserId).SendAsync("ReceiveMessage", sender.Username, message);
    }

    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        // Log the connection
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        // Log the disconnection
        return base.OnDisconnectedAsync(exception);
    }
}
