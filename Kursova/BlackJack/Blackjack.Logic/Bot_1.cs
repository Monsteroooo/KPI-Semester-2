using System;
using Cards;

namespace Players
{
public class Bot_1 : Player
{
    private Random random;

    public Bot_1()
    : base(100)
    {
        this.random = new Random();
    }

    public override void makeBet(int bet)
    {
        int actual_bet;
        actual_bet = (int)(this.Money * random.Next(5, 11) / 100);
        this.Bet = actual_bet;
        this.Money = this.Money - this.Bet;
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
