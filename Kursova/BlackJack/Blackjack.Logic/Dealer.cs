namespace Game
{
    public class Dealer
    {
        private Deck deck;
        private List<Card> hand;

        public Diller()
        {
            this.deck = new Deck();
            this.hand = new List<Card>();
        }

        public void TakeTwoCards()
        {
            getCard();
            getCard();
        }

        public void ShowFirstCard()
        {
            Console.WriteLine("Dealer's first card: " + this.hand[0].ToString());
        }

        public void ShowAllCards()
        {
            Console.WriteLine("Dealer's cards: ");
            foreach (Card c in this.hand)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public int CardsValue()
        {
            int sum = 0;
            foreach (Card c in this.hand)
            {
                sum += c.ReturnAmount();
            }

            return sum;
        }
        
        public void getCard(Card newCard)
        {
            this.hand.Add(newCard);
        }

        public void GiveStartCards(Player player)
        {
            player.getCard(this.deck.DrawCard());
            player.getCard(this.deck.DrawCard());
        }
    }
}
