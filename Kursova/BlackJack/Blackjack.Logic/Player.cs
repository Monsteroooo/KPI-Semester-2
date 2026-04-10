using Cards;
using System.Collections.Generic;

namespace Players
{
public class Player
{
    private int money;
    public List<Card> hand;
    public int bet;

    public Player(int money)
    {
        this.Money = money;
        this.hand = new List<Card>();
    }

    public int Bet
    {
        get
        {
            return this.bet;
        }

        set
        {
            if (value > this.money)
            {
                throw new ArgumentOutOfRangeException("Bet cannot be greater than the amount of money you have.");
            }
            else if (value < 0)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
            }

            this.bet = value;
        }
    }

    public int Money
    {
        get
        {
            return this.money;
        }

        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            this.money = value;
        }
    }

    public int Points
    {
        get
        {
            int sum = 0;
            foreach (Card c in this.hand)
            {
                sum += c.ReturnAmount();
            }

            return sum;
        }
    }



    public virtual void makeBet(int amount)
    {
        this.Bet = amount;
        this.money = this.money - this.Bet;
    }



    public virtual bool getCard(Card newCard)
    {
        this.hand.Add(newCard);
        return true;
    }

    public void ClearHand()
    {
        this.hand.Clear();
    }
}
}
