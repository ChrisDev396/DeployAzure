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

    private static string[] jogador1;
    private static string[] jogador2;
    private Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();

    private static List<string> list = new List<string>();

    public async Task JoinRoom(string combatente, string carta1, string carta2, string carta3, string carta4, string carta5)
    {
        await _semaphore.WaitAsync();

        try
        {
            //Thread.Sleep(10000);
            var claims = Context.User.Claims;
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;

            if (emailClaim != null)
            {}

            usersInRoom++;

            //baralhos = new string[] { emailClaim ,baralho};
            //baralhos.Concat(baralho);

            //list.Add(emailClaim);
            
            //foreach (string valor in baralho)
            //{
            //    list.Add(valor);
            //}


            await Groups.AddToGroupAsync(Context.ConnectionId, sala.ToString());

            

            if (usersInRoom == 2)
            {
                jogador2 = new string[] { emailClaim, combatente, carta1, carta2, carta3, carta4, carta5 };
                jogador1.Concat(jogador2);
                await Clients.Group(sala.ToString()).SendAsync("JoinRoom", $"Testes{jogador1[0]}");
                dictionary.Add(sala.ToString(), jogador1);
                sala++;
                usersInRoom = 0;
                //Array.Clear(baralhos, 0, baralhos.Length);
            }
            else
            {
                jogador1 = new string[] { emailClaim, combatente, carta1, carta2, carta3, carta4, carta5 };
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