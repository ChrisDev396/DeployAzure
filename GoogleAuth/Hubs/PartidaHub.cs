using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class PartidaHub : Hub
{
    public async Task SendMessage(string user, string message)
    {

        //if (message == "salaCriada")
        //{
            // await Clients.Client(user).SendAsync("SendMessage", user, message);
            await Clients.All.SendAsync("SendMessage", user, message);
        //}
        //else
        //{

        //}
        
    }
}



//public async IAsyncEnumerable<DateTime> Straming(CancellationToken cancellationToken)
//{
//    yield return DateTime.Now;
//    await Task.Delay(1000, cancellationToken);
//}