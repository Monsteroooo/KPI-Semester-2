
namespace Players
{
public class Bot_3 : Player
{
    private Random random;

    public Bot_3()
    : base(1000)
    {
        this.random = new Random();
    }

    public override int makeBet(int bet)
    {
        bet = random.Next(1, this.Money + 1);
        this.Money = this.Money - bet;
        return bet;
    }

    public override bool getCard(Card newCard)
    {
        bool wantCard;
        if (random.Next(0, 101) > 50)
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

