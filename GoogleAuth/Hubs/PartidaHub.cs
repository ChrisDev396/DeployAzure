﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class PartidaHub : Hub
{
    private static int maxUsersInRoom = 2;
    private static int usersInRoom = 0;

    public async Task JoinRoom(string roomName)//roomName
    {
        if (usersInRoom < maxUsersInRoom)
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            usersInRoom++;
            roomName += roomName;
            // Informe ao usuário que eles entraram na sala.
            await Clients.Caller.SendAsync("RoomJoined", roomName);
        }
        else
        {
            // A sala está cheia. Você pode informar ao usuário ou tomar outra ação apropriada.
            await Clients.Caller.SendAsync("RoomJoined", usersInRoom.ToString());
        }

        
    }

    public async Task Sala(string roomName)//chris@gmail.comteste@gmail.com
    {
        if (usersInRoom < maxUsersInRoom)
        {
      
            usersInRoom++;
            roomName += roomName;
            //await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            // Informe ao usuário que eles entraram na sala.
            await Clients.Caller.SendAsync("RoomJoined", roomName);
        }
        else
        {
            usersInRoom++;
            // A sala está cheia. Você pode informar ao usuário ou tomar outra ação apropriada.
            await Clients.Caller.SendAsync("RoomJoined", usersInRoom.ToString());
        }
    }

    public async Task SendMessageToRoom(string roomName, string user, string message)
    {
        await Clients.Group(roomName).SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Ao desconectar, reduza o contador de usuários na sala.
        usersInRoom--;

        await base.OnDisconnectedAsync(exception);
    }
        
    
}
