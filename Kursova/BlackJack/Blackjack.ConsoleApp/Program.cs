using System;
using System.Text;
using Game;
using Players;

Console.OutputEncoding = Encoding.UTF8;

Game.Game game = new Game.Game();
SetupEvents(game);

Console.WriteLine("=== БЛЕКДЖЕК ===");

bool keepPlaying = true;

while (keepPlaying)
{
    if (game.player.Money <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nВИ БАНКРУТ! У вас закінчилися гроші.");
        Console.ResetColor();
        Console.WriteLine("Бажаєте почати повністю нову гру? (y - так, n - вийти)");
        
        if (Console.ReadLine()?.ToLower() == "y")
        {
            game = new Game.Game();
            SetupEvents(game);
            continue; 
        }
        else
        {
            break;
        }
    }

    Console.WriteLine($"\nВаш поточний баланс: {game.player.Money}");
    Console.WriteLine($"Ботів у грі: {game.ActiveBots.Count}");

    int bet = 0;
    while (true)
    {
        Console.Write("Введіть вашу ставку: ");
        if (int.TryParse(Console.ReadLine(), out bet) && bet > 0 && bet <= game.player.Money)
            break;
        Console.WriteLine("Некоректна ставка. Вона має бути більшою за 0 і не перевищувати ваш баланс.");
    }

    game.StartGame(bet);

    Console.WriteLine("\nВаші карти:");
    foreach (var card in game.player.hand)
    {
        Console.WriteLine($"- {card.ToString()}");
    }
    Console.WriteLine($"Очки: {game.player.Points}\n");

    while (game.player.Points <= 21)
    {
        Console.WriteLine("Взяти ще карту? (y - так, n - ні/досить)");
        string? input = Console.ReadLine()?.ToLower();

        if (input == "y")
        {
            game.PlayerHit();
        }
        else if (input == "n")
        {
            break;
        }
    }

    if (game.player.Points <= 21)
    {
        game.FinishGame();
    }

    if (game.player.Money > 0)
    {
        Console.WriteLine("\nЗіграти ще один раунд? (y - так, n - вийти)");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            game.PrepareNewRound();
        }
        else
        {
            keepPlaying = false;
        }
    }
}

Console.WriteLine("Дякуємо за гру!");

// МЕТОДИ-ОБРОБНИКИ ПОДІЙ

void SetupEvents(Game.Game g)
{
    g.OnMessageSent += HandleGameMessage;
    g.OnGameEnded += HandleGameEnded;
    g.Dealer.OnMessageSent += (msg) => Console.WriteLine($"[Дилер]: {msg}");
}

void HandleGameMessage(Player player, string message, Game.Game.MessageType type)
{
    switch (type)
    {
        case Game.Game.MessageType.Warning:
            Console.ForegroundColor = ConsoleColor.Red;
            break;
        case Game.Game.MessageType.Result:
            Console.ForegroundColor = ConsoleColor.Yellow;
            break;
        case Game.Game.MessageType.TurnAction:
            Console.ForegroundColor = ConsoleColor.Cyan;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.White;
            break;
    }

    Console.WriteLine(message);
    Console.ResetColor();
}

void HandleGameEnded(System.Collections.Generic.List<Player> participants, string finalMessage)
{
    Console.WriteLine($"\n{finalMessage}");
    Console.WriteLine("-------------------------");
    
    foreach (var p in participants)
    {
        string name = p.GetType().Name;
        if (name == "Player") name = "Ви (Гравець)";
        
        string status = p.Money <= 0 ? "[БАНКРУТ]" : "";
        Console.WriteLine($"{name} | Баланс: {p.Money} {status} | Очки у цьому раунді: {p.Points}");
    }
}
