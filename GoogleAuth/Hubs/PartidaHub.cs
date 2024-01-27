using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    private static bool valorAleatorio = false;

    public async Task JoinRoom(string[] baralho)
    {
        await _semaphore.WaitAsync();

        try
        {
            var claims = Context.User.Claims;
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            if (emailClaim.Value != null)
            {}

            usersInRoom++;

            await Groups.AddToGroupAsync(Context.ConnectionId, sala.ToString());

            if (usersInRoom == 2)
            {
                valorAleatorio = !valorAleatorio;
                jogador2 = new Jogador(emailClaim.Value, baralho, valorAleatorio);

                List<Jogador> novaLista = new List<Jogador>
                {
                    jogador1,
                    jogador2
                };

                dictionary.Add(sala.ToString(), novaLista);

                await Clients.Group(sala.ToString()).SendAsync("JoinRoom", sala.ToString());

                sala++;
                usersInRoom = 0;

            }
            else
            {
                valorAleatorio = new Random().Next(2) == 0;
                jogador1 = new Jogador(emailClaim.Value, baralho, valorAleatorio);

            }
        }
        finally
        {
            _semaphore.Release();
        }

    }
    
    public async Task GetJogadoresStatus(string roomName)
    {
       
        if (dictionary.ContainsKey(roomName))
        {
            string[] jogadorInfo1 = { dictionary[roomName][0].nome, dictionary[roomName][0].forca.ToString(), dictionary[roomName][0].vida.ToString() , dictionary[roomName][0].heroi, dictionary[roomName][0].turno.ToString() };
            string[] jogadorInfo2 = { dictionary[roomName][1].nome, dictionary[roomName][1].forca.ToString(), dictionary[roomName][1].vida.ToString(), dictionary[roomName][1].heroi, dictionary[roomName][1].turno.ToString() };
            await Clients.Group(roomName).SendAsync("GetJogadoresStatus", jogadorInfo1, jogadorInfo2);
        }
        else
        {
            await Clients.Group(roomName).SendAsync("GetJogadoresStatus", "Sala não encontrada.");
        }

    }

    public async Task Atacar(string roomName)
    {

        if (dictionary.ContainsKey(roomName))
        {
            if (dictionary[roomName][0].turno)
            {
                dictionary[roomName][1].vida = dictionary[roomName][1].vida - dictionary[roomName][0].forca;
                
            }
            else if (dictionary[roomName][1].turno)
            {
                dictionary[roomName][0].vida = dictionary[roomName][0].vida - dictionary[roomName][1].forca;
            }

            dictionary[roomName][0].turno = !dictionary[roomName][0].turno;
            dictionary[roomName][1].turno = !dictionary[roomName][1].turno;

            string[] jogadorInfo1 = { dictionary[roomName][0].nome, dictionary[roomName][0].forca.ToString(), dictionary[roomName][0].vida.ToString(), dictionary[roomName][0].heroi, dictionary[roomName][0].turno.ToString() };
            string[] jogadorInfo2 = { dictionary[roomName][1].nome, dictionary[roomName][1].forca.ToString(), dictionary[roomName][1].vida.ToString(), dictionary[roomName][1].heroi, dictionary[roomName][1].turno.ToString() };
            await Clients.Group(roomName).SendAsync("Atacar", jogadorInfo1, jogadorInfo2);
        }

    }

    public async Task SendMessageToRoom(string roomName)
    {
        string nome = "";
        if (dictionary.ContainsKey(roomName))
        {

            //dictionary.Remove(roomName);
            //List<string> nomesJogadores = new List<string>();

            //Adicione o nome de cada jogador à lista
            //foreach (Jogador jogador in dictionary[roomName])
            //{
            //    nomesJogadores.Add(jogador.nome);
            //    //nome = jogador.nome;
            //}
            //if (dictionary[roomName][0].nome == turno)
            //{
            //    nome = dictionary[roomName][1].nome;
            //}
            //else
            //{
            //    nome = dictionary[roomName][1].nome;
            //}
            await Clients.Group(roomName).SendAsync("SendMessageToRoom", nome);


        }
        else
        {
            await Clients.Group(roomName).SendAsync("SendMessageToRoom", "Sala não encontrada.");

        }

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