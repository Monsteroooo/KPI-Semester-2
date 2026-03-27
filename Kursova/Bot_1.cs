
namespace Players
{
public class Bot_1 : Player
{
    private Random random;

    public Bot_1()
    : base(1000)
    {
        this.random = new Random();
    }

    public override int makeBet(int bet)
    {
        bet = (int)(this.Money * random.Next(5, 11) / 100);
        this.Money = this.Money - bet;
        return bet;
    }

    public override bool getCard(Card newCard)
    {
        bool wantCard;
        if (this.Points >= 14)
        {
            wantCard = false;
        }
        else
        {
            wantCard = true;
        }

        if (wantCard)
        {
            this.hand.Add(newCard);
            return true;
        }
        else
        {
            return false;
        }
    }
}
}
