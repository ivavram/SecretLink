using Common;
using Comms;
using Models; 
using Interface;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Services
{
    public class GameService
    {
        private readonly IUnitOfWOrk unitOfWork; 
        //private HubService hubService;

        public GameService(IUnitOfWOrk unitOfWork, IHubContext<MessageHub> hub)
        {
            this.unitOfWork = unitOfWork; 
            //this.hubService = new HubService(hub);
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

        public async Task<Game> CreateGame(int playerID, bool access_flag)
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

                Connect4Game connect4 = new(){};

                Game game = new()
                {
                    GameTag = CommonMethods.GenerateRandomString(),
                    PlayerTurn = player,
                    NumOfPlayers = 1,
                    PlayerInGame = new List<PlayerInGame> { playerInGame },
                    Connect4Games = new List<Connect4Game> { connect4 },
                    GuessTheWord = guessTheWord, 
                    GameStatus = true,
                    PublicPrivate = access_flag
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

                //await hubService.NotifyOnPlayersChanges(game.ID, "JoinGame", player, player_in_game);

                Console.WriteLine("service: " + game!.GuessTheWord);

                return game;
            }
             
        }
        public async Task<Player> GetGameWinner(int gameID)
        {
            var player = await unitOfWork.GameRepository.GetGameWinner(gameID);
            return player;
        }

        public async Task<Game> LeaveGame(int gameID, int playerID)
        {
            var game = await unitOfWork.GameRepository.GetById(gameID);
            if(game == null)
                return null!;

            game.NumOfPlayers -=1;

            if(game.NumOfPlayers == 0)
                game.GameStatus = false;

            //var players_in_game = await unitOfWork.PlayerInGameRepository.GetByGameIDAndPlayerID(gameID, playerID);

            await unitOfWork.PlayerInGameRepository.DeleteComposite(gameID, playerID);

            await unitOfWork.CompleteAsync();
            
            return game;

        }
        public async Task<Game> GetPublicGame()
        {
            var game = await unitOfWork.GameRepository.GetPublicGame();
            return game;
        }

        public async Task<Game> GetConnect4Games(int gameID, string username)
        {
            var game = await unitOfWork.GameRepository.GetConnect4Games(gameID);
            var winner = await unitOfWork.PlayerRepository.GetPlyerByUsername(username);

            if(game != null && winner != null)
            {
                var connect4GameToUpdate = game.Connect4Games!
                        .FirstOrDefault(connGame => connGame.Winner == null);

            // Dodelite pobednika samo ako je pronađena igra
                if (connect4GameToUpdate != null)
                {
                    connect4GameToUpdate.Winner = winner;
                    await unitOfWork.CompleteAsync();
                }
            }
            return game!;
        }

        public async Task<Game> CheckConnect4Game(int gameID)
        {
            // vraca igre gde je winner == null
            var game = await unitOfWork.GameRepository.GetConnect4Games(gameID); 
            if(game != null)
                return game;
            return null!;
        }

        public async Task<Game> AddNewConnect4Game(int gameID)
        {
            var game = await unitOfWork.GameRepository.GetById(gameID);
            if(game != null)
            {
                Connect4Game connect4 = new() {};
                game.Connect4Games!.Add(connect4);
                await unitOfWork.CompleteAsync();
                return game;
            }
            return null!;
        }

        public async Task<Game> SetGameWinner(int gameID, string playerUsername)
        {
            var game = await unitOfWork.GameRepository.GetById(gameID);
            var player = await unitOfWork.PlayerRepository.GetPlyerByUsername(playerUsername);
            var c4_game = await unitOfWork.GameRepository.GetConnect4Games(gameID);

            if(game != null && player != null)
            {
                game.GameWinner = player;

                if(c4_game != null)
                {
                    var connect4GameToUpdate = c4_game.Connect4Games!
                                .FirstOrDefault(connGame => connGame.Winner == null);
                    if (connect4GameToUpdate != null)
                        connect4GameToUpdate.Winner = player;
                }
                game.GameStatus = false;
                await unitOfWork.CompleteAsync();
                return game;

            }
            return null!;
        }
    }
}