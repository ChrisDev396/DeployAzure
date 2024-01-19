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
        var claims = Context.User.Claims;

        var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        if (usersInRoom < maxUsersInRoom)
        {
            if (emailClaim != null)
            {
                string email = emailClaim.Value;

                //if (email.Equals(sala))
                //{
                //    sala = "bloqueado";
                //}
                sala += email;
            }
      
            //await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            
            usersInRoom++;
            await Clients.All.SendAsync("JoinRoom", sala, usersInRoom);
        }
        else
        {
            sala = "";

            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                sala += email;
            }
            usersInRoom = 1;
            await Clients.Caller.SendAsync("JoinRoom", sala);
        }
    }

    public async Task SendMessageToRoom(string roomName, string user, Jogador jogador)
    {
        
        await Clients.Group(roomName).SendAsync("acao", user);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        //sala = "";
        //usersInRoom = 0;

        await base.OnDisconnectedAsync(exception);
    }


}
