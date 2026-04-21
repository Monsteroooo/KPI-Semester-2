using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using Game;
using Players;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Blackjack");
        Raylib.SetTargetFPS(60);

        Game.Game game = new Game.Game();

        // 1. СТАН ГРИ ТА РЕЗУЛЬТАТИ
        bool isGameOver = false;
        // Словник для зберігання фінального тексту ("You won!", "Busted!") для кожного гравця
        Dictionary<Player, string> roundResults = new Dictionary<Player, string>();

        // 2. ПІДПИСКА НА ПОДІЇ
        // Ловимо повідомлення типу Result і зберігаємо їх у словник для відмальовки
        game.OnMessageSent += (player, message, type) =>
        {
            if (type == Game.Game.MessageType.Result && player != null)
            {
                roundResults[player] = message;
            }
        };

        game.StartGame(100);

        Rectangle hitButton = new Rectangle(280, 520, 100, 40);
        Rectangle standButton = new Rectangle(420, 520, 100, 40);
        
        // Нова кнопка по центру екрану
        Rectangle playAgainBtn = new Rectangle(300, 270, 200, 50); 

        while (!Raylib.WindowShouldClose())
        {
            // ==========================================
            // ЛОГІКА (ОБРОБКА КЛІКІВ)
            // ==========================================
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePos = Raylib.GetMousePosition();

                if (!isGameOver) // Стан 1: Гра триває
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, hitButton) && game.player.Points <= 21)
                    {
                        game.PlayerHit();
                        // Якщо після добору очок > 21, PlayerHit сам викликає FinishGame
                        if (game.player.Points > 21) isGameOver = true;
                    }
                    else if (Raylib.CheckCollisionPointRec(mousePos, standButton))
                    {
                        game.FinishGame();
                        isGameOver = true; // Перемикаємо стан
                    }
                }
                else // Стан 2: Гра завершена (GameOver)
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, playAgainBtn))
                    {
                        if (game.player.Money > 0)
                        {
                            game.PrepareNewRound();
                            roundResults.Clear(); // Очищаємо старі результати
                            isGameOver = false;
                            game.StartGame(100);
                        }
                        else
                        {
                            // Якщо гравець банкрут, створюємо об'єкт гри з нуля
                            game = new Game.Game();
                            game.OnMessageSent += (player, message, type) => {
                                if (type == Game.Game.MessageType.Result && player != null) roundResults[player] = message;
                            };
                            roundResults.Clear();
                            isGameOver = false;
                            game.StartGame(100);
                        }
                    }
                }
            }

            // ==========================================
            // ВІДМАЛЬОВКА
            // ==========================================
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(0, 80, 0, 255)); 

            // 1. ДИЛЕР
            int dealerX = 300;
            int dealerY = 20;
            Raylib.DrawText("- DEALER -", dealerX, dealerY, 20, Color.Gold);
            Raylib.DrawText($"Points: {game.Dealer.CardsValue()}", dealerX, dealerY + 25, 20, Color.White);
            
            int dYOffset = dealerY + 50;
            foreach (var card in game.Dealer.hand)
            {
                Raylib.DrawText($"- {card.ToString()}", dealerX, dYOffset, 20, Color.White);
                dYOffset += 20;
            }

            // 2. ГРАВЕЦЬ
            int playerX = 20;
            int playerY = 350;
            Raylib.DrawText("- YOU -", playerX, playerY, 20, Color.Green);
            Raylib.DrawText($"Balance: {game.player.Money} $", playerX, playerY + 25, 20, Color.Gold);
            Raylib.DrawText($"Bet: {game.player.Bet} $", playerX, playerY + 45, 20, Color.Gold);
            Raylib.DrawText($"Points: {game.player.Points}", playerX, playerY + 65, 20, Color.White);
            
            int pYOffset = playerY + 90;
            foreach (var card in game.player.hand) 
            {
                Raylib.DrawText($"- {card.ToString()}", playerX, pYOffset, 18, Color.LightGray);
                pYOffset += 20;
            }
            
            // МАЛЮЄМО РЕЗУЛЬТАТ ГРАВЦЯ
            if (isGameOver && roundResults.ContainsKey(game.player))
            {
                Raylib.DrawText(roundResults[game.player], playerX, pYOffset + 10, 18, Color.Yellow);
            }

            // 3. БОТИ
            Vector2[] botPositions = new Vector2[] {
                new Vector2(20, 80),
                new Vector2(600, 80),
                new Vector2(600, 350)
            };

            for (int i = 0; i < game.ActiveBots.Count; i++)
            {
                var bot = game.ActiveBots[i];
                int bX = (int)botPositions[i].X;
                int bY = (int)botPositions[i].Y;

                Raylib.DrawText($"- {bot.GetType().Name} -", bX, bY, 20, Color.SkyBlue);
                Raylib.DrawText($"Balance: {bot.Money} $", bX, bY + 25, 18, Color.Gold);
                Raylib.DrawText($"Bet: {bot.Bet} $", bX, bY + 45, 18, Color.Gold);
                Raylib.DrawText($"Points: {bot.Points}", bX, bY + 65, 18, Color.White);

                int botCardY = bY + 90;
                foreach (var card in bot.hand)
                {
                    Raylib.DrawText($"- {card.ToString()}", bX, botCardY, 16, Color.LightGray);
                    botCardY += 20;
                }

                // МАЛЮЄМО РЕЗУЛЬТАТ БОТА
                if (isGameOver && roundResults.ContainsKey(bot))
                {
                    Raylib.DrawText(roundResults[bot], bX, botCardY + 10, 16, Color.Yellow);
                }
            }

            // 4. КНОПКИ (Рендеринг залежить від стану гри)
            if (!isGameOver)
            {
                Raylib.DrawRectangleRec(hitButton, Color.LightGray);
                Raylib.DrawText("HIT", (int)hitButton.X + 32, (int)hitButton.Y + 10, 20, Color.Black);

                Raylib.DrawRectangleRec(standButton, Color.LightGray);
                Raylib.DrawText("STAND", (int)standButton.X + 15, (int)standButton.Y + 10, 20, Color.Black);
            }
            else
            {
                Raylib.DrawRectangleRec(playAgainBtn, Color.Blue);
                Raylib.DrawText("PLAY AGAIN", (int)playAgainBtn.X + 40, (int)playAgainBtn.Y + 15, 20, Color.White);
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
