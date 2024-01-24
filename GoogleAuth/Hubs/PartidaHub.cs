using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
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

    private static Jogador jogador1;
    private static Jogador jogador2;

    private static Dictionary<string, List<Jogador>> dictionary = new Dictionary<string, List<Jogador>>();

    private static List<Jogador> list = new List<Jogador>();

    private static bool valorAleatorio = false;

    public async Task JoinRoom(string[] baralho)
    {
        await _semaphore.WaitAsync();

        try
        {
            usersInRoom++;

            await Groups.AddToGroupAsync(Context.ConnectionId, sala.ToString());


            if (usersInRoom == 2)
            {
                valorAleatorio = !valorAleatorio;
                jogador2 = new Jogador(Context.ConnectionId.ToString(), baralho, valorAleatorio, sala.ToString());

                list.Add(jogador1);
                list.Add(jogador2);
                dictionary.Add(sala.ToString(), list);
                list.Clear();

                await Clients.Group(sala.ToString()).SendAsync("JoinRoom", sala.ToString());

                sala++;
                usersInRoom = 0;

            }
            else
            {
                valorAleatorio = new Random().Next(2) == 0;
                jogador1 = new Jogador(Context.ConnectionId.ToString(), baralho, valorAleatorio, sala.ToString());

            }
        }
        finally
        {
            _semaphore.Release();
        }

    }


    public async Task SendMessageToRoom(string roomName)
    {
        
        if (dictionary.ContainsKey(roomName))
        {
            List<Jogador> jogadores = dictionary[roomName];

            // Crie uma lista para armazenar os nomes dos jogadores
            List<string> nomesJogadores = new List<string>();

            // Adicione o nome de cada jogador à lista
            foreach (Jogador jogador in jogadores)
            {
                nomesJogadores.Add(jogador.nome);
               
            }

            
        }
        else
        {
            await Clients.Group(roomName).SendAsync("SendMessageToRoom", "Sala não encontrada.");
           
        }
        await Clients.Group(roomName).SendAsync("SendMessageToRoom", "testessss");
    }
    
public override async Task OnDisconnectedAsync(Exception exception)
    {

        await base.OnDisconnectedAsync(exception);
    }

    
    //if (jogadores.ContainsKey(chaveASerRemovida))
    //{
    //    jogadores.Remove(chaveASerRemovida);
    //    Console.WriteLine($"Chave {chaveASerRemovida} removida do cache.");
    //}
    //else
    //{
    //    Console.WriteLine($"Chave {chaveASerRemovida} não encontrada no cache.");
    //}
}