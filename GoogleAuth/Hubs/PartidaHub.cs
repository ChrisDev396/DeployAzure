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
    private static int sala = 1;
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private static List<string> salas;
    private static string nomeSala = "";

    public async Task JoinRoom(string baralho)
    {
        await _semaphore.WaitAsync();

        try
        {
            //Thread.Sleep(10000);
            //var claims = Context.User.Claims;
            //var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;

            //if (emailClaim != null)
            //{
            //    nomeSala += emailClaim;
            //}

            usersInRoom++;

            await Groups.AddToGroupAsync(Context.ConnectionId, sala.ToString());
   

            if (usersInRoom == 2)
            {
                await Clients.Group(sala.ToString()).SendAsync("JoinRoom", sala.ToString());
                //salas.Add(sala);
                if (sala < 1000000)
                {
                    sala++;
                }
                else
                {
                    sala = 1;
                }
                
                usersInRoom = 0;
                //nomeSala = "";
            }
            

            
        }
        finally
        {
            _semaphore.Release();
        }
    }


    public async Task SendMessageToRoom(string roomName, string acao, string valor)
    {
        await Clients.Group(roomName).SendAsync("SendMessageToRoom", acao);
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
