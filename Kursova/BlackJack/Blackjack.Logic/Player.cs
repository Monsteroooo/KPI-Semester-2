using Cards;
using System.Collections.Generic;
namespace Players
{
public class Player
{
    private int money;
    protected List<Card> hand;

    public Player(int money)
    {
        this.Money = money;
        this.hand = new List<Card>();
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

    public virtual int makeBet(int bet)
    {
        this.money = this.money - bet;
        return bet;
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
