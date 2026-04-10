using System.Collections.Generic;

namespace Cards
{
public class Deck
{
    private Random random;
    public Card[] deck = new Card[52];
    public Stack<Card> cardsStack = new Stack<Card>();

    public Deck()
    {
        this.random = new Random();
        GenerateDeck();
        Shuffle();
        foreach (Card c in deck)
        {
            this.cardsStack.Push(c);
        }
    }

    private void GenerateDeck()
    {
        int index = 0;
        foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
        {
            foreach (RankPos rank in Enum.GetValues(typeof(RankPos)))
            {
                deck[index] = new Card(suit, rank);
                index++;
            }
        }
    }

    public void Shuffle()
    {
        for (int i = deck.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    public Card DrawCard()
    {
        if (cardsStack.Count > 0)
        {
            return cardsStack.Pop();
        }
        else
        {
            throw new ArgumnetException("stack cant be empty", cardsStack);
        }
    }
}
}
