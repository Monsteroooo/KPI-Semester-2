using namespace Players;
using namespace Cards;
using System;

namespace Game
{
public class Game
{
    public Dealer Dealer;
    public Player player;
    public Bot_1 bot1;
    public Bot_2 bot2;
    public Bot_3 bot3;

    public Main()
    {
        this.bot1 = new Bot_1;
        this.bot2 = new Bot_2;
        this.bot3 = new Bot_3;
        this.player = new Player(1000);
        this.Dealer = new Dealer();
        StartGame();
        PlayerTurn();
        BotTurn();
        EndGame();
    }

    public void StartGame()
    {
        this.Dealer.GiveStartCards(this.player);
        this.Dealer.GiveStartCards(this.bot1);
        this.Dealer.GiveStartCards(this.bot2);
        this.Dealer.GiveStartCards(this.bot3);
    }

    public void PlayerTurn()
    {
        while (true)
        {
            this.Dealer.ShowFirstCard();
            Console.WriteLine("Your points: " + this.player.Points);
            Console.Writeline("You need to make a bet.");
            Console.WriteLine("Enter your bet amount:");
            int bet = int.Parse(Console.ReadLine());
            this.player.makeBet(bet);
            
            do
            {
                Console.WriteLine("Do you want to take another card? (y/n)");
                string wantMoreCard = Console.ReadLine();
                if (answer == "y")
                {
                    this.player.getCard(this.Dealer.deck.DrawCard());
                }
                else
                {
                    throw new ArgumentException("Invalid input, please enter 'y' or 'n'.");
                }
            } while (wantMoreCard == "y");
        }
    }

    public void BotTurn()
    {
        while (this.bot1.getCard(this.Dealer.deck.DrawCard())) { }
        int bet1 = this.bot1.makeBet(0);
        while (this.bot2.getCard(this.Dealer.deck.DrawCard())) { }
        int bet2 = this.bot2.makeBet(0);
        while (this.bot3.getCard(this.Dealer.deck.DrawCard())) { }
        int bet3 = this.bot3.makeBet(0);
    }

    public void EndGame()
    {
        Console.WriteLine("Your points: " + this.player.Points);
        Console.WriteLine("Bot 1 points: " + this.bot1.Points);
        Console.WriteLine("Bot 2 points: " + this.bot2.Points);
        Console.WriteLine("Bot 3 points: " + this.bot3.Points);
        if (this.player.Points > 21)
        {
            Console.WriteLine("You lose!");
        }
        else if (this.bot1.Points > 21)
        {
            Console.WriteLine("Bot 1 busts!");
        }
        else if (this.bot2.Points > 21)
        {
            Console.WriteLine("Bot 2 busts!");
        }
        else if (this.bot3.Points > 21)
        {
            Console.WriteLine("Bot 3 busts!");
        }
        else if (this.bot1.Points > this.Dealer.CardsValue())
        {
            Console.WriteLine("Bot 1 wins!");
            bot1.Money += bet1 * 2;
        }
        else if (this.bot2.Points > this.Dealer.CardsValue())
        {
            Console.WriteLine("Bot 2 wins!");
            bot2.Money += bet2 * 2;
        }
        else if (this.bot3.Points > this.Dealer.CardsValue())
        {
            Console.WriteLine("Bot 3 wins!");
            bot3.Money += bet3 * 2;
        }
        else if (this.bot1.Points == this.Dealer.CardsValue())
        {
            Console.WriteLine("Bot 1 ties with the dealer!");
            bot1.Money += bet3;
        }
        else if (this.bot2.Points == this.Dealer.CardsValue())
        {
            Console.WriteLine("Bot 2 ties with the dealer!");
            bot2.Money += bet3;
        }
        else if (this.bot3.Points == this.Dealer.CardsValue())
        {
            Console.WriteLine("Bot 3 ties with the dealer!");
            bot3.Money += bet3;
        }

        else
        {
            if (this.player.Points > Dealer.CardsValue())
            {
                Console.WriteLine("You win!");
                player.Money += bet * 2;
            }
            else if (this.player.Points < Dealer.CardsValue())
            {
                Console.WriteLine("You lose!");
                bet = 0;
            }
            else
            {
                Console.WriteLine("It's a tie!");
                this.Player.Money += bet;
            }
        }
    }
}
}
