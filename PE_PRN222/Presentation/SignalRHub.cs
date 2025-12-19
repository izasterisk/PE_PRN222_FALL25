using Microsoft.AspNetCore.SignalR;

namespace Presentation
{
    public class SignalRHub : Hub
    {
        public async Task NotifyProfileDeleted(int id)
        {
            await Clients.All.SendAsync("ProfileDeleted", id);
        }
    }
}
