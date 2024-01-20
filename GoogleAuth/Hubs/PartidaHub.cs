using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;

[Authorize]
public class PartidaHub : Hub
{
    private static int usersInRoom = 0;
    private static string sala = "";
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private static List<string> salas;

    public async Task JoinRoom(string baralho)
    {
        await _semaphore.WaitAsync();

        try
        {
            //Thread.Sleep(10000);
            var claims = Context.User.Claims;
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            if (emailClaim.Value != null)
            {

                //string email = emailClaim.Value;

                //if (email.Equals(sala))
                //{
                //    sala = "bloqueado";
                //}

                if (usersInRoom < 2)
                {
                    usersInRoom++;
                    if (usersInRoom == 2)
                    {
                        salas.Add(sala);
                    }
                }
                else
                {
                    sala = "";
                    usersInRoom = 1;
                }

                sala += emailClaim.Value + "baralho" + baralho + "/";
                await Clients.Client(Context.ConnectionId).SendAsync("JoinRoom", sala);

            }

            
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
