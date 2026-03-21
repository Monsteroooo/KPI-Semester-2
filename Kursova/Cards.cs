namespace Cards
{
public enum CardSuits { Hearts, Diamonds, Clubs, Spades }
public enum RankPos { Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack, Queen, King, Ace }

public class Card
{
    private CardSuits suit;
    private RankPos rank;

    public Card(CardSuits suit, RankPos rank)
    {
        this.suit = suit;
        this.rank = rank;
    }

    public CardSuits Suit
    {
        get
        {
            return suit;
        }

        set
        {
            this.suit = value;
        }
    }

    public RankPos Rank
    {
        get
        {
            return rank;
        }

        set
        {
            this.rank = value;
        }
    }

    public int ReturnAmount()
    {
        switch (this.rank)
        {
            case RankPos.Jack:
            case RankPos.Queen:
            case RankPos.King:
                return 10;

            case RankPos.Ace:
                return 11;  /// Remake in future

            default:
                return (int)this.rank;
        }
    }
}
}
