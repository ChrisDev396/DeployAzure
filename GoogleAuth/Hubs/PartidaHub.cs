﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

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
            //List<string> itensJogador1 = new List<string>();
            //foreach (ItemStatus item in dictionary[roomName][0].itemStatus)
            //{
            //    string carta = item.nome + "/" + item.tipo + "/" + item.forca.ToString() + "/" + item.vida.ToString();

            //    itensJogador1.Add(carta);
            //}

            string[] itensJogador1 = new string[dictionary[roomName][0].itemStatus.Count];

            for (int i = 0; i < dictionary[roomName][0].itemStatus.Count; i++)
            {
                itensJogador1[i] = dictionary[roomName][0].itemStatus[i].ToFormattedString();
            }

            string[] itensJogador2 = new string[dictionary[roomName][1].itemStatus.Count];

            for (int i = 0; i < dictionary[roomName][1].itemStatus.Count; i++)
            {
                itensJogador2[i] = dictionary[roomName][1].itemStatus[i].ToFormattedString();
            }

            
            string[] jogadorInfo1 = { dictionary[roomName][0].nome, dictionary[roomName][0].turno.ToString() };
            string[] jogadorInfo2 = { dictionary[roomName][1].nome, dictionary[roomName][1].turno.ToString() };

            
            await Clients.Group(roomName).SendAsync("GetJogadoresStatus", jogadorInfo1, jogadorInfo2, itensJogador1, itensJogador2);
            

            

        }
    }

    public async Task Atacar(string roomName)
    {

        if (dictionary.ContainsKey(roomName))
        {
            if (dictionary[roomName][0].turno)
            {
                dictionary[roomName][1].itemStatus[0].vida -= dictionary[roomName][0].itemStatus[0].forca;
            }
            else
            {
                dictionary[roomName][0].itemStatus[0].vida -= dictionary[roomName][1].itemStatus[0].forca;
            }

            dictionary[roomName][0].turno = !dictionary[roomName][0].turno;
            dictionary[roomName][1].turno = !dictionary[roomName][1].turno;

            await GetJogadoresStatus(roomName);

            if (CartaMestre.Resultado(dictionary[roomName][0], dictionary[roomName][1]))
            {
                dictionary.Remove(roomName);
            }
        }

    }

    public async Task CartaGenerica(string roomName, string carta)
    {
        var claims = Context.User.Claims;
        var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
        if (emailClaim.Value != null)
        { }
        if (dictionary.ContainsKey(roomName))
        {
            if (dictionary[roomName][0].turno)
            {
                if (dictionary[roomName][0].usuario == emailClaim.Value)
                {
                    CartaItem.Item(carta, dictionary[roomName][0], dictionary[roomName][1]);
                }
            }
            else
            {
                if (dictionary[roomName][1].usuario == emailClaim.Value)
                {
                    CartaItem.Item(carta, dictionary[roomName][1], dictionary[roomName][0]);
                }
            }
            await GetJogadoresStatus(roomName);
        }

    }

    //public override async Task OnDisconnectedAsync(Exception exception)
    //{

    //    await base.OnDisconnectedAsync(exception);
    //}
}