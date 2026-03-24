using Cards;
using System.Collections.Generic;
namespace Players
{
public class Player
{
    private int money;
    private int points;
    public List<Card> hand;

    public Player(int money)
    {
        this.Money = money;
        this.points = 0;
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

            return this.points;
        }

        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            this.points = value;
        }
    }

    public int makeBet(int bet)
    {
        this.money = this.money - bet;
        return bet;
    }

    public void getCard(Card newCard)
    {
        this.hand.Add(newCard);
    }

    public void ClearHand()
    {
        this.hand.Clear();
    }
}
}
