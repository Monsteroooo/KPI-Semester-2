
namespace Game
{
public class Game
{
    public Player player;
    public Bot_1 bot1;
    public Bot_2 bot2;
    public Bot_3 bot3;

    public Game()
    {
        this.bot1 = new Bot_1;
        this.bot2 = new Bot_2;
        this.bot3 = new Bot_3;
        this.player = new Player(1000);
    }
}
}
