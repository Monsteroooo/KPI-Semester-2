
namespace Players
{
public class Bot_2 : Player
{
    private Random random;
    public bool isLost;

    public Bot_2()
    : base(1000)
    {
        this.random = new Random();
    }

    public override int makeBet(int bet)
    {
        if (isLost)
        {
            bet = this.Money;
        }
        else
        {
            bet = (int)(this.Money * random.Next(30, 51) / 100);
        }

        this.Money = this.Money - bet;
        return bet;
    }

    public override bool getCard(Card newCard)
    {
        bool wantCard;
        if (this.Points <= 17)
        {
            wantCard = true;
        }
        else
        {
            wantCard = false;
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

