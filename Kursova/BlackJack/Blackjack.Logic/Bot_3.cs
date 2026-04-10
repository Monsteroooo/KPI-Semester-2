using System;
using Cards;

namespace Players
{
public class Bot_3 : Player
{
    private Random random;

    public Bot_3()
    : base(100)
    {
        this.random = new Random();
    }

    public override void makeBet(int bet)
    {
        int actual_bet = random.Next(1, this.Money + 1);
        this.Bet = actual_bet;
        this.Money = this.Money - this.Bet;
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

