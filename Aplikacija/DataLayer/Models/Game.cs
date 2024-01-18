namespace Models;


public class Game
{
    public int ID { get; set; }
    [Range(1,2)]
    public int NumOfPlayers { get; set; }
    public string? GameTag { get; set; }
    public Player? GameWinner { get; set; }
    public Player? PlayerTurn { get; set; }
    public GuessTheWordGame? GuessTheWord { get; set; }
    public List<Connect4Game>? Connect4Games { get; set; } = new List<Connect4Game>(); 
    public List<PlayerInGame>? PlayerInGame { get; set; } = new List<PlayerInGame>();
    public bool GameStatus { get; set; }
}