using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using Game;
using Players;
using Cards;

enum GameState
{
    Betting,
    Playing,
    GameOver,
    TotalLoss,
    Paused,
    Rules
}

class Program
{
    static Rectangle GetCardSourceRec(Card card, Texture2D sheet)
    {
        float cardWidth = sheet.Width / 13f;
        float cardHeight = sheet.Height / 4f;

        int col;
        if (card.Rank == RankPos.Ace) col = 0;
        else col = (int)card.Rank - 1;

        int row = 0;
        switch (card.Suit)
        {
            case CardSuits.Hearts:   row = 0; break;
            case CardSuits.Spades:   row = 1; break;
            case CardSuits.Diamonds: row = 2; break;
            case CardSuits.Clubs:    row = 3; break;
        }

        return new Rectangle(col * cardWidth, row * cardHeight, cardWidth, cardHeight);
    }

    static void Main()
    {
        Raylib.InitWindow(1600, 1200, "Blackjack");
        Raylib.SetTargetFPS(60);

        Texture2D bgTexture = Raylib.LoadTexture("table_bg.png");
        Texture2D cardsSheet = Raylib.LoadTexture("cards.png");

        Game.Game game = new Game.Game();

        GameState currentState = GameState.Betting;
        GameState previousState = GameState.Betting; 
        int currentBet = 10;

        Dictionary<Player, string> roundResults = new Dictionary<Player, string>();

        game.OnMessageSent += (player, message, type) =>
        {
            if (type == Game.Game.MessageType.Result && player != null)
            {
                roundResults[player] = message;
            }
        };

        Rectangle hitButton = new Rectangle(560, 1040, 200, 80);
        Rectangle standButton = new Rectangle(840, 1040, 200, 80);
        Rectangle playAgainBtn = new Rectangle(600, 540, 400, 100);

        Rectangle betMinusBtn = new Rectangle(560, 800, 80, 80);
        Rectangle betPlusBtn = new Rectangle(960, 800, 80, 80);
        Rectangle betMaxBtn = new Rectangle(660, 900, 280, 60);
        Rectangle dealBtn = new Rectangle(660, 800, 280, 80);

        Rectangle pauseBtn = new Rectangle(1480, 20, 80, 80);
        Rectangle resumeBtn = new Rectangle(600, 400, 400, 100);
        Rectangle rulesBtn = new Rectangle(600, 540, 400, 100);
        Rectangle quitBtn = new Rectangle(600, 680, 400, 100);
        Rectangle backBtn = new Rectangle(600, 1000, 400, 100);

        Rectangle restartBtn = new Rectangle(550, 700, 500, 100);
        Rectangle quitLossBtn = new Rectangle(550, 820, 500, 100);
        
        bool keepPlaying = true; 

        int destCardWidth = 120;
        int destCardHeight = 168;

        while (!Raylib.WindowShouldClose() && keepPlaying)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePos = Raylib.GetMousePosition();

                if (currentState == GameState.Paused)
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, resumeBtn)) currentState = previousState;
                    else if (Raylib.CheckCollisionPointRec(mousePos, rulesBtn)) currentState = GameState.Rules;
                    else if (Raylib.CheckCollisionPointRec(mousePos, quitBtn)) keepPlaying = false; 
                }
                else if (currentState == GameState.Rules)
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, backBtn)) currentState = GameState.Paused; 
                }
                else if (currentState == GameState.TotalLoss)
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, restartBtn))
                    {
                        game = new Game.Game();
                        game.OnMessageSent += (player, message, type) => {
                            if (type == Game.Game.MessageType.Result && player != null) roundResults[player] = message;
                        };
                        roundResults.Clear();
                        currentBet = 10;
                        currentState = GameState.Betting;
                    }
                    else if (Raylib.CheckCollisionPointRec(mousePos, quitLossBtn))
                    {
                        keepPlaying = false;
                    }
                }
                else
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, pauseBtn))
                    {
                        previousState = currentState;
                        currentState = GameState.Paused;
                    }
                    else if (currentState == GameState.Betting)
                    {
                        if (Raylib.CheckCollisionPointRec(mousePos, betMinusBtn)) currentBet = Math.Max(1, currentBet - 10); 
                        else if (Raylib.CheckCollisionPointRec(mousePos, betPlusBtn)) currentBet = Math.Min(game.player.Money, currentBet + 10); 
                        else if (Raylib.CheckCollisionPointRec(mousePos, betMaxBtn)) currentBet = game.player.Money; 
                        else if (Raylib.CheckCollisionPointRec(mousePos, dealBtn))
                        {
                            game.StartGame(currentBet);
                            currentState = GameState.Playing;
                        }
                    }
                    else if (currentState == GameState.Playing)
                    {
                        if (Raylib.CheckCollisionPointRec(mousePos, hitButton) && game.player.Points <= 21)
                        {
                            game.PlayerHit();
                            if (game.player.Points > 21) currentState = GameState.GameOver;
                        }
                        else if (Raylib.CheckCollisionPointRec(mousePos, standButton))
                        {
                            game.FinishGame();
                            currentState = GameState.GameOver;
                        }
                    }
                    else if (currentState == GameState.GameOver)
                    {
                        if (Raylib.CheckCollisionPointRec(mousePos, playAgainBtn))
                        {
                            if (game.player.Money > 0)
                            {
                                game.PrepareNewRound();
                                roundResults.Clear();
                                currentBet = Math.Min(currentBet, game.player.Money); 
                                currentState = GameState.Betting;
                            }
                            else
                            {
                                currentState = GameState.TotalLoss;
                            }
                        }
                    }
                }
            }

            Raylib.BeginDrawing();
            
            Raylib.DrawTexturePro(bgTexture, 
                new Rectangle(0, 0, bgTexture.Width, bgTexture.Height), 
                new Rectangle(0, 0, 1600, 1200), 
                new Vector2(0, 0), 0f, Color.White);

            int dealerX = 700; int dealerY = 40;
            Raylib.DrawText("- DEALER -", dealerX, dealerY, 40, Color.Gold);
            
            if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss)
            {
                Raylib.DrawText($"Points: {game.Dealer.CardsValue()}", dealerX, dealerY + 50, 40, Color.White);
                
                int dYOffset = dealerY + 100;
                int currentCardX = dealerX;
                foreach (var card in game.Dealer.hand)
                {
                    Rectangle src = GetCardSourceRec(card, cardsSheet);
                    Rectangle dest = new Rectangle(currentCardX, dYOffset, destCardWidth, destCardHeight);
                    Raylib.DrawTexturePro(cardsSheet, src, dest, new Vector2(0, 0), 0f, Color.White);
                    currentCardX += 40;
                }
            }

            int playerX = 40; int playerY = 700;
            Raylib.DrawText("- YOU -", playerX, playerY, 40, Color.Green);
            Raylib.DrawText($"Balance: {game.player.Money} $", playerX, playerY + 50, 40, Color.Gold);
            
            if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss)
            {
                Raylib.DrawText($"Bet: {game.player.Bet} $", playerX, playerY + 90, 40, Color.Gold);
                Raylib.DrawText($"Points: {game.player.Points}", playerX, playerY + 130, 40, Color.White);
                
                int pYOffset = playerY + 180;
                int currentCardX = playerX;
                foreach (var card in game.player.hand) 
                {
                    Rectangle src = GetCardSourceRec(card, cardsSheet);
                    Rectangle dest = new Rectangle(currentCardX, pYOffset, destCardWidth, destCardHeight);
                    Raylib.DrawTexturePro(cardsSheet, src, dest, new Vector2(0, 0), 0f, Color.White);
                    currentCardX += 40; 
                }
                
                if (currentState == GameState.GameOver && roundResults.ContainsKey(game.player))
                    Raylib.DrawText(roundResults[game.player], playerX, pYOffset + destCardHeight + 20, 36, Color.Yellow);
            }

            Vector2[] botPositions = new Vector2[] { new Vector2(40, 160), new Vector2(1200, 160), new Vector2(1200, 700) };
            for (int i = 0; i < game.ActiveBots.Count; i++)
            {
                var bot = game.ActiveBots[i];
                int bX = (int)botPositions[i].X; int bY = (int)botPositions[i].Y;

                Raylib.DrawText($"- {bot.GetType().Name} -", bX, bY, 40, Color.SkyBlue);
                Raylib.DrawText($"Balance: {bot.Money} $", bX, bY + 50, 36, Color.Gold);

                if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss)
                {
                    Raylib.DrawText($"Bet: {bot.Bet} $", bX, bY + 90, 36, Color.Gold);
                    Raylib.DrawText($"Points: {bot.Points}", bX, bY + 130, 36, Color.White);
                    
                    int botCardY = bY + 180;
                    int currentCardX = bX;
                    foreach (var card in bot.hand)
                    {
                        Rectangle src = GetCardSourceRec(card, cardsSheet);
                        Rectangle dest = new Rectangle(currentCardX, botCardY, destCardWidth, destCardHeight);
                        Raylib.DrawTexturePro(cardsSheet, src, dest, new Vector2(0, 0), 0f, Color.White);
                        currentCardX += 40;
                    }
                    if (currentState == GameState.GameOver && roundResults.ContainsKey(bot))
                        Raylib.DrawText(roundResults[bot], bX, botCardY + destCardHeight + 20, 32, Color.Yellow);
                }
            }

            if (currentState == GameState.Betting)
            {
                Raylib.DrawText("PLACE YOUR BET", 620, 720, 40, Color.White);
                Raylib.DrawRectangleRec(betMinusBtn, Color.LightGray); Raylib.DrawText("-", (int)betMinusBtn.X + 30, (int)betMinusBtn.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(betPlusBtn, Color.LightGray); Raylib.DrawText("+", (int)betPlusBtn.X + 24, (int)betPlusBtn.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(dealBtn, Color.Gold); Raylib.DrawText($"DEAL: {currentBet}$", (int)dealBtn.X + 30, (int)dealBtn.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(betMaxBtn, Color.Red); Raylib.DrawText("ALL IN", (int)betMaxBtn.X + 70, (int)betMaxBtn.Y + 10, 40, Color.White);
            }
            else if (currentState == GameState.Playing)
            {
                Raylib.DrawRectangleRec(hitButton, Color.LightGray); Raylib.DrawText("HIT", (int)hitButton.X + 64, (int)hitButton.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(standButton, Color.LightGray); Raylib.DrawText("STAND", (int)standButton.X + 30, (int)standButton.Y + 20, 40, Color.Black);
            }
            else if (currentState == GameState.GameOver)
            {
                Raylib.DrawRectangleRec(playAgainBtn, Color.Blue); Raylib.DrawText("PLAY AGAIN", (int)playAgainBtn.X + 80, (int)playAgainBtn.Y + 30, 40, Color.White);
            }

            if (currentState == GameState.TotalLoss)
            {
                Raylib.DrawRectangle(0, 0, 1600, 1200, new Color(0, 0, 0, 220));
                
                Raylib.DrawText("YOU ARE BROKE!", 550, 400, 80, Color.Red);
                Raylib.DrawText("The house always wins...", 620, 500, 30, Color.Gray);
                
                Raylib.DrawText("Want to start a new game?", 560, 600, 40, Color.White);

                Raylib.DrawRectangleRec(restartBtn, Color.DarkBlue);
                Raylib.DrawText("START NEW GAME", (int)restartBtn.X + 35, (int)restartBtn.Y + 30, 40, Color.White);
                
                Raylib.DrawRectangleRec(quitLossBtn, Color.Maroon);
                Raylib.DrawText("QUIT", (int)quitLossBtn.X + 90, (int)quitLossBtn.Y + 30, 40, Color.White);
            }
            else if (currentState == GameState.Paused || currentState == GameState.Rules)
            {
                Raylib.DrawRectangle(0, 0, 1600, 1200, new Color(0, 0, 0, 180));
                
                if (currentState == GameState.Paused)
                {
                    Raylib.DrawText("--- PAUSED ---", 600, 240, 60, Color.Gold);
                    
                    Raylib.DrawRectangleRec(resumeBtn, Color.LightGray);
                    Raylib.DrawText("RESUME", (int)resumeBtn.X + 120, (int)resumeBtn.Y + 30, 40, Color.Black);
                    
                    Raylib.DrawRectangleRec(rulesBtn, Color.LightGray);
                    Raylib.DrawText("RULES", (int)rulesBtn.X + 140, (int)rulesBtn.Y + 30, 40, Color.Black);

                    Raylib.DrawRectangleRec(quitBtn, Color.Red);
                    Raylib.DrawText("QUIT GAME", (int)quitBtn.X + 80, (int)quitBtn.Y + 30, 40, Color.White);
                }
                else if (currentState == GameState.Rules)
                {
                    Raylib.DrawText("--- RULES ---", 620, 120, 60, Color.Gold);
                    Raylib.DrawText("1. Goal: Get closer to 21 than the dealer without going over.", 160, 300, 40, Color.White);
                    Raylib.DrawText("2. Cards 2-10 are worth their face value.", 160, 400, 40, Color.White);
                    Raylib.DrawText("3. Kings, Queens, and Jacks are worth 10.", 160, 500, 40, Color.White);
                    Raylib.DrawText("4. Aces are worth 1 or 11, depend on your needs.", 160, 600, 40, Color.White);
                    Raylib.DrawText("5. Hit to take a card, Stand to hold your total.", 160, 700, 40, Color.White);
                    
                    Raylib.DrawRectangleRec(backBtn, Color.LightGray);
                    Raylib.DrawText("BACK", (int)backBtn.X + 140, (int)backBtn.Y + 30, 40, Color.Black);
                }
            }
            else
            {
                Raylib.DrawRectangleRec(pauseBtn, Color.DarkGray);
                Raylib.DrawText("||", (int)pauseBtn.X + 30, (int)pauseBtn.Y + 20, 40, Color.White);
            }

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(bgTexture);
        Raylib.UnloadTexture(cardsSheet);
        
        Raylib.CloseWindow();
    }
}
