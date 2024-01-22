using System.Collections.Concurrent;
using System.Collections.Generic;
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

    private static string[] baralhos;
    private Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();

    public async Task JoinRoom(string baralho)
    {
        await _semaphore.WaitAsync();

        try
        {
            //Thread.Sleep(10000);
            var claims = Context.User.Claims;
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;

            if (emailClaim != null)
            {
            }

            usersInRoom++;

            baralhos = new string[] { emailClaim ,baralho};
            //baralhos.Concat(baralho);


            await Groups.AddToGroupAsync(Context.ConnectionId, sala.ToString());

            

            if (usersInRoom == 2)
            {
                dictionary.Add(sala.ToString(), baralhos);
                await Clients.Group(sala.ToString()).SendAsync("JoinRoom", $"{baralhos[0]} {baralhos[1]} {baralhos[2]} {baralhos[3]}");
                
                sala++;
                usersInRoom = 0;
                Array.Clear(baralhos, 0, baralhos.Length);
            }

        }
        finally
        {
            _semaphore.Release();
        }
    }


    public async Task SendMessageToRoom(string roomName)
    {
        await Clients.Group(roomName).SendAsync("SendMessageToRoom", "teste");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {

        await base.OnDisconnectedAsync(exception);
    }
//    if (jogadores.ContainsKey(chaveASerRemovida))
//{
//    jogadores.Remove(chaveASerRemovida);
//    Console.WriteLine($"Chave {chaveASerRemovida} removida do cache.");
//}
//else
//{
//    Console.WriteLine($"Chave {chaveASerRemovida} não encontrada no cache.");
//}
}