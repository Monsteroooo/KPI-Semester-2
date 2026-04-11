using Players;
using Cards;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Game
    {
        public Dealer Dealer;
        public Player player;
        
        public List<Player> ActiveBots; 

        public enum MessageType
        {
            Info,
            Warning,
            TurnAction,
            Result
        }

        public event Action<Player, string, MessageType>? OnMessageSent;
        public event Action<List<Player>, string>? OnGameEnded;


        public Game()
        {
            this.player = new Player(100);
            this.Dealer = new Dealer();
            
            this.ActiveBots = new List<Player> { new Bot_1(), new Bot_2(), new Bot_3() };
        }

        public void PrepareNewRound()
        {
            this.Dealer.hand.Clear();
            this.Dealer.deck = new Deck();

            this.player.ClearHand();

            this.ActiveBots.RemoveAll(b => b.Money <= 0);
            
            foreach (var bot in this.ActiveBots)
            {
                bot.ClearHand();
            }
        }

        public void StartGame(int playerBetAmount)
        {
            this.Dealer.GiveStartCards(this.player);
            
            foreach (var bot in this.ActiveBots)
            {
                this.Dealer.GiveStartCards(bot);
            }

            this.Dealer.getCard(this.Dealer.deck.DrawCard());
            this.Dealer.getCard(this.Dealer.deck.DrawCard());

            OnMessageSent?.Invoke(this.player, $"Game started! Your cards: ", MessageType.Info);
            foreach (var card in this.player.hand)
            {
                OnMessageSent?.Invoke(this.player, $"{card.Rank} of {card.Suit}", MessageType.Info);
            }

            this.player.makeBet(playerBetAmount);
            
            foreach (var bot in this.ActiveBots)
            {
                bot.makeBet(0);
                OnMessageSent?.Invoke(this.player, $"{bot.GetType().Name} робить ставку: {bot.Bet}", MessageType.Info);
            }

            this.Dealer.ShowFirstCard();

            OnMessageSent?.Invoke(this.player, "Cards dealt. Your turn!", MessageType.Info);
        }
        
        public void PlayerHit()
        {
            Card drawnCard = this.Dealer.deck.DrawCard();
            this.player.getCard(drawnCard);
            OnMessageSent?.Invoke(this.player, $"Ви витягнули: {drawnCard.ToString()} | Ваші очки: {this.player.Points}", MessageType.TurnAction);

            if (this.player.Points > 21)
            {
                OnMessageSent?.Invoke(this.player, "Bust! You exceeded 21 points.", MessageType.Warning);
                FinishGame();
            }
        }

        public void FinishGame()
        {
            BotTurn();
            DealerTurn();
            EndGame();
        }

        public void DealerTurn()
        {
            while (this.Dealer.CardsValue() < 17)
            {
                Card drawnCard = this.Dealer.deck.DrawCard();
                this.Dealer.getCard(drawnCard);
                OnMessageSent?.Invoke(this.player, $"Дилер витягнув: {drawnCard.ToString()} | Очки дилера: {this.Dealer.CardsValue()}", MessageType.TurnAction);
            }

            int cardsValue = this.Dealer.CardsValue();
            OnMessageSent?.Invoke(this.player, $"Dealer's turn ended with {cardsValue} points.", MessageType.Info);
        }

        public void BotTurn()
        {
            foreach (var bot in this.ActiveBots)
            {
                while (bot.getCard(this.Dealer.deck.DrawCard())) { }
            }
        }
        
        public void EndGame()
        {
            List<Player> participants = new List<Player> { this.player };
            participants.AddRange(this.ActiveBots);

            int dealerTotal = this.Dealer.CardsValue();
            
            if (dealerTotal > 21)
            {
                OnMessageSent?.Invoke(this.player, $"Dealer busted with {dealerTotal} points!", MessageType.Result);
            }

            foreach (var p in participants)
            {
                string playerName = p.GetType().Name;
                if (playerName == "Player") playerName = "You";

                if (p.Points > 21)
                {
                    OnMessageSent?.Invoke(p, $"{playerName} busted with {p.Points}!", MessageType.Result);
                }
                else if (p.Points == 21)
                {
                    OnMessageSent?.Invoke(p, $"{playerName} GOT BLACKJACK!", MessageType.Result);
                    p.Money += p.Bet * 3;
                }
                else if (dealerTotal > 21 || p.Points > dealerTotal)
                {
                    OnMessageSent?.Invoke(p, $"{playerName} won against dealer!", MessageType.Result);
                    p.Money += p.Bet * 2;
                }
                else if (p.Points == dealerTotal)
                {
                    OnMessageSent?.Invoke(p, $"{playerName} tied with dealer.", MessageType.Result);
                    p.Money += p.Bet;
                }
                else
                {
                    OnMessageSent?.Invoke(p, $"{playerName} lost to dealer.", MessageType.Result);
                }
            }

            OnGameEnded?.Invoke(participants, "--- FINAL RESULTS ---");
        }
    }
}
