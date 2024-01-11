

namespace Comms
{
    public class MessageHub : Hub
    {
        public async Task<string> JoinApp(int playerID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Player" + playerID);
            
            return "Joined app";
        }

        public async Task LeaveApp(int playerID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Player" + playerID);
        }

        public async Task<string> JoinGame(int gameID, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game" + gameID);
            
            await Clients.Group("Game" + gameID).SendAsync("SendMessageJoin", username, " has joined the game!");

            
            return "Player " + username + " joined the game " + gameID;
        }
        
        public async Task<string> LeaveGame(int gameID, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Game" + gameID);
            
            await Clients.Group("Game" + gameID).SendAsync("SendMessageLeave", username, " has left the game!");

            
            return "Player " + username + " left the game " + gameID;
        }

        public async Task SendGroupChatMessage(int gameID, string username, string message)
        {
            await Clients.Group("Game" + gameID).SendAsync("ReceiveMessage", username, message);
        }

    }
}