namespace Models;

public class Connect4Game
{

    public int ID { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public Player? Winner { get; set; }
    
}