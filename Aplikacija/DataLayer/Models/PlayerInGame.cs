namespace Models;


public class PlayerInGame
{
    public int ID { get; set; }
   [JsonIgnore]
    public Game? Game { get; set; }
    public int GameID { get; set; }
    public Player? Player { get; set; }
    public int PlayerID { get; set; }
}