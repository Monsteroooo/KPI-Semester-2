using System;
using Cards;

namespace Players
{
public class Bot_2 : Player
{
    private Random random;
    public bool isLost;

    public Bot_2()
    : base(100)
    {
        this.random = new Random();
    }

    public override void makeBet(int bet)
    {
        int actual_bet;
        if (isLost)
        {
            actual_bet = this.Money;
        }
        else
        {
            actual_bet = (int)(this.Money * random.Next(30, 51) / 100);
        }

        this.Bet = actual_bet;
        this.Money = this.Money - this.Bet;
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

