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
