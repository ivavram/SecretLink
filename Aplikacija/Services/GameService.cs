using Common;
using Comms;
using Models; 
using Interface; 

namespace Services
{
    public class GameService
    {
        private readonly IUnitOfWOrk unitOfWork; 
        private HubService hubService;

        public GameService(IUnitOfWOrk unitOfWork, IHubContext<MessageHub> hub)
        {
            this.unitOfWork = unitOfWork; 
            this.hubService = new HubService(hub);
        }

        /*
         public async Task<Player?> CreatePlayer(Player player)
        {
            using (unitOfWork)
            {
                
                if(player.Password.Length < 8 || player.Password.Length > 25)
                    return null; 

                Player p = await unitOfWork.PlayerRepository.GetPlyerByUsername(player.Username); 

                if(p != null)
                    return null; 
                
                player.Password = CommonMethods.EncryptPassword(player.Password, player.Username); 

                unitOfWork.PlayerRepository.Create(player); 
                await unitOfWork.CompleteAsync(); 

                return player; 
            }
        }
        */

        public async Task<Game> CreateGame(int playerID)
        {
            using(unitOfWork)
            {
                var player = await unitOfWork.PlayerRepository.GetById(playerID); 
                if(player == null)
                    return null!; 

                var words = await unitOfWork.WordRepository.GetAll();
                int totalWordsCount = words.Count();
                Word? wordToGuess = null;

                if (words != null)
                {
                    Random random = new Random();
                    int randomIndex = random.Next(1, totalWordsCount + 1);
                    //wordToGuess = words.ElementAtOrDefault(randomIndex - 1);
                    wordToGuess = await unitOfWork.WordRepository.GetById(randomIndex); 
                }

                GuessTheWordGame guessTheWord = new()
                {
                    Word = wordToGuess
                };

                PlayerInGame playerInGame = new()
                {
                    Player = player
                };

                Connect4Game connect4 = new()
                {
                    Row = 0,
                    Column = 0,
                   
                };


                Game game = new()
                {
                    GameTag = CommonMethods.GenerateRandomString(),
                    PlayerTurn = player,
                    NumOfPlayers = 1,
                    PlayerInGame = new List<PlayerInGame> { playerInGame },
                    Connect4Games = new List<Connect4Game> { connect4 },
                    GuessTheWord = guessTheWord, 
                    GameStatus = true
                };

                unitOfWork.GameRepository.Create(game);
                await unitOfWork.CompleteAsync();
                return game;
            }
        }

        public async Task<Game> JoinGame(int playerID, string game_tag)
        {
            var game = await unitOfWork.GameRepository.GetGameByTag(game_tag);
            var player = await unitOfWork.PlayerRepository.GetById(playerID);

            if(game == null || player == null || game.NumOfPlayers >= 2 || game.GameStatus == false)
                return null!;
            else
            {
                game.NumOfPlayers += 1; 
                PlayerInGame player_in_game = new()
                {
                    Player = player
                };
                game.PlayerInGame!.Add(player_in_game);

                unitOfWork.GameRepository.Update(game);

                await unitOfWork.CompleteAsync();

                await hubService.NotifyOnPlayersChanges(game.ID, "JoinGame", player, player_in_game);

                return game;
            }
             
        }
        public async Task<Player> GetGameWinner(int gameID)
        {
            var player = await unitOfWork.GameRepository.GetGameWinner(gameID);
            return player;
        }

    }
}