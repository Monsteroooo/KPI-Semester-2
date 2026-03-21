
namespace Cards
{
public class Deck
{
    public Card[] deck = new Card[52];

    public Deck()
    {
        GenerateDeck();
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
}
}
