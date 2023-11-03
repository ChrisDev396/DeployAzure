using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

[Authorize]
public class PartidaHub : Hub
{
    private static int maxUsersInRoom = 2;
    private static int usersInRoom = 0;
    private static string sala = "";

    public async Task JoinRoom()
    {
        if (usersInRoom < maxUsersInRoom)
        {
            var claims = Context.User.Claims;

            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                sala += email;
            }
            //await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            
            usersInRoom++;
            await Clients.All.SendAsync("JoinRoom", sala);
        }
        else
        {
            sala = "";
            usersInRoom = 0;
            await Clients.Caller.SendAsync("JoinRoom", sala);
        }
    }

    public async Task SendMessageToRoom(string roomName, string user, string message)
    {
        await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        sala = "";
        usersInRoom = 0;

        await base.OnDisconnectedAsync(exception);
    }


}
