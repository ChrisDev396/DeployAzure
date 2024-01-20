using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

[Authorize]
public class PartidaHub : Hub
{
    private static int maxUsersInRoom = 2;
    private static int usersInRoom = 0;
    private static string sala = "";
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    //private static List<string> Salas = new List<string>();


    public async Task JoinRoom()
    {
        await _semaphore.WaitAsync();

        try
        {
            Thread.Sleep(10000);
            var claims = Context.User.Claims;
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            if (usersInRoom < maxUsersInRoom)
            {
                if (emailClaim != null)
                {
                    string email = emailClaim.Value;
                    usersInRoom++;
                    sala += email + "/";
                }
            }
            else
            {
                sala = "";
                if (emailClaim != null)
                {
                    string email = emailClaim.Value;
                    sala += email + "/";
                }
                usersInRoom = 1;
            }
            await Clients.All.SendAsync("JoinRoom", sala);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task SendMessageToRoom(string roomName, string mensagem)
    {
        var claims = Context.User.Claims;
        var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

        string[] nomes = roomName.TrimEnd('/').Split('/');

        if (nomes[0] == emailClaim.Value || nomes[1] == emailClaim.Value)
        {
            await Clients.Group(roomName).SendAsync("mensagem", mensagem);
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        //sala = "";
        //usersInRoom = 0;

        await base.OnDisconnectedAsync(exception);
    }

    /*
                if (email.Equals(sala))
                {
                    sala = "bloqueado";
                }
    */
}
