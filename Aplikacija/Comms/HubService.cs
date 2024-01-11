

namespace Comms
{
    public class HubService
    {
        private readonly IHubContext<MessageHub> hubContext;

        public HubService(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task NotifyOnGameChanges(int gameID, string method, Object object_to_send)
        {
            await hubContext.Clients.Group("Game" + gameID).SendAsync(method, object_to_send);
        }

        public async Task NotifyOnPlayersChanges(int gameID, string method, Object object_to_send, Object second_object_to_send)
        {
             await hubContext.Clients.Group("Game" + gameID).SendAsync(method, object_to_send, second_object_to_send);
        }

        public async Task NotifyUser(int userID, String method, Object object_to_send)
        {
            await hubContext.Clients.Group("User" + userID).SendAsync(method, object_to_send);
        }
    }
}