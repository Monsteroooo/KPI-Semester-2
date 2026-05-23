using System;
using System.Collections.Generic;
using Cards;
using Players;

namespace Game
{
    public class Dealer
    {
        public Deck deck;
        public List<Card> hand;

        public event Action<string>? OnMessageSent;

        public Dealer()
        {
            this.deck = new Deck();
            this.hand = new List<Card>();
        }

        public void ShowFirstCard()
        {
            OnMessageSent?.Invoke("Dealer's first card: " + this.hand[0].ToString());
        }

        public void ShowAllCards()
        {
            OnMessageSent?.Invoke("Dealer's cards: ");
            foreach (Card c in this.hand)
            {
                OnMessageSent?.Invoke(c.ToString());
            }
        }

        public int CardsValue()
        {
            int acesCount = 0;
            int sum = 0;
            foreach (Card c in this.hand)
            {
                sum += c.ReturnAmount();

                if (c.Rank == RankPos.Ace)
                {
                    acesCount++;
                }
            }

            while (sum > 21 && acesCount > 0)
            {
                sum -= 10;
                acesCount--;
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
