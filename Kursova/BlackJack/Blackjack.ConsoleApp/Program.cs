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

class CardAnim
{
    public bool Active;
    public Card? Card;
    public Rectangle SourceRec;
    public int DestX, DestY;
    public float StartX, StartY, CurrentX, CurrentY; 
    public float Alpha;
    public object? Target;
    public float Progress;
}

class Program
{
    static Font uaFont;

    static void DrawUA(string text, int x, int y, int size, Color color)
    {
        Raylib.DrawTextEx(uaFont, text, new Vector2(x, y), size, 2, color);
    }

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

    static int GetVisualPoints(List<Card> hand, int visualCount)
    {
        int acesCount = 0;
        int sum = 0;
        for (int i = 0; i < visualCount; i++)
        {
            Card c = hand[i];
            sum += c.ReturnAmount();
            if (c.Rank == RankPos.Ace) acesCount++;
        }
        while (sum > 21 && acesCount > 0)
        {
            sum -= 10;
            acesCount--;
        }
        return sum;
    }

    static void Main()
    {
        Raylib.InitWindow(1600, 1200, "Блекджек");
        Raylib.SetTargetFPS(60);

        List<int> codepoints = new List<int>();

        for (int i = 32; i < 127; i++) codepoints.Add(i);

        for (int i = 1024; i < 1280; i++) codepoints.Add(i);

        uaFont = Raylib.LoadFontEx("font.ttf", 48, codepoints.ToArray(), codepoints.Count);

        Texture2D bgTexture = Raylib.LoadTexture("table_bg.png");
        Texture2D cardsSheet = Raylib.LoadTexture("cards.png");

        Game.Game game = new Game.Game();
        GameState currentState = GameState.Rules;
        GameState previousState = GameState.Betting; 
        int currentBet = 10;
        bool isFirstTimeRules = true;
        Dictionary<Player, string> roundResults = new Dictionary<Player, string>();

        game.OnMessageSent += (player, message, type) =>
        {
            if (type == Game.Game.MessageType.Result && player != null) roundResults[player] = message;
        };

        int dealerVisualCards = 0;
        Dictionary<Player, int> playerVisualCards = new Dictionary<Player, int>();
        CardAnim currentAnim = new CardAnim();

        void InitVisuals()
        {
            dealerVisualCards = 0;
            playerVisualCards.Clear();
            playerVisualCards[game.player] = 0;
            foreach (var bot in game.ActiveBots) playerVisualCards[bot] = 0;
            currentAnim.Active = false;
        }

        bool IsCatchingUp()
        {
            if (currentAnim.Active) return true;
            if (dealerVisualCards < game.Dealer.hand.Count) return true;
            if (playerVisualCards.ContainsKey(game.player) && playerVisualCards[game.player] < game.player.hand.Count) return true;
            foreach (var bot in game.ActiveBots)
                if (playerVisualCards.ContainsKey(bot) && playerVisualCards[bot] < bot.hand.Count) return true;
            return false;
        }

        void StartAnim(Card card, object target, int destX, int destY)
        {
            currentAnim.Active = true;
            currentAnim.Card = card;
            currentAnim.Target = target;
            currentAnim.DestX = destX;
            currentAnim.DestY = destY;
            currentAnim.StartX = 700;
            currentAnim.StartY = 140; 
            currentAnim.CurrentX = currentAnim.StartX;
            currentAnim.CurrentY = currentAnim.StartY;
            currentAnim.Alpha = 0;
            currentAnim.Progress = 0;
            currentAnim.SourceRec = GetCardSourceRec(card, cardsSheet);
        }

        InitVisuals();

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
        Rectangle backBtn = new Rectangle(600, 1000, 400, 100);
        Rectangle quitBtn = new Rectangle(600, 680, 400, 100);
        Rectangle restartBtn = new Rectangle(550, 700, 500, 100);
        Rectangle quitLossBtn = new Rectangle(550, 820, 500, 100);

        bool keepPlaying = true; 
        int destCardWidth = 120;
        int destCardHeight = 168;
        Vector2[] botPositions = new Vector2[] { new Vector2(40, 160), new Vector2(1200, 160), new Vector2(1200, 700) };

        while (!Raylib.WindowShouldClose() && keepPlaying)
        {
            if (currentAnim.Active)
            {
                currentAnim.Progress += 0.06f;
                if (currentAnim.Progress >= 1f)
                {
                    currentAnim.Progress = 1f;
                    currentAnim.Active = false;
                    if (currentAnim.Target is Dealer) dealerVisualCards++;
                    else if (currentAnim.Target is Player p) playerVisualCards[p]++;
                }
                else
                {
                    float ease = 1f - (float)Math.Pow(1f - currentAnim.Progress, 3);
                    currentAnim.CurrentX = currentAnim.StartX + (currentAnim.DestX - currentAnim.StartX) * ease;
                    currentAnim.CurrentY = currentAnim.StartY + (currentAnim.DestY - currentAnim.StartY) * ease;
                    currentAnim.Alpha = 255f * ease;
                }
            }
            else if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss && currentState != GameState.Paused)
            {
                if (dealerVisualCards < game.Dealer.hand.Count) StartAnim(game.Dealer.hand[dealerVisualCards], game.Dealer, 700 + dealerVisualCards * 40, 140);
                else if (playerVisualCards[game.player] < game.player.hand.Count) StartAnim(game.player.hand[playerVisualCards[game.player]], game.player, 40 + playerVisualCards[game.player] * 40, 880);
                else
                {
                    for (int i = 0; i < game.ActiveBots.Count; i++)
                    {
                        var bot = game.ActiveBots[i];
                        if (playerVisualCards[bot] < bot.hand.Count)
                        {
                            int bX = (int)botPositions[i].X;
                            int bY = (int)botPositions[i].Y;
                            StartAnim(bot.hand[playerVisualCards[bot]], bot, bX + playerVisualCards[bot] * 40, bY + 180);
                            break;
                        }
                    }
                }
            }

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
                    if (Raylib.CheckCollisionPointRec(mousePos, backBtn))
                    {
                        if (isFirstTimeRules) { isFirstTimeRules = false; currentState = GameState.Betting; }
                        else currentState = GameState.Paused;
                    }
                }
                else if (currentState == GameState.TotalLoss && !IsCatchingUp())
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, restartBtn))
                    {
                        game = new Game.Game();
                        game.OnMessageSent += (player, message, type) => { if (type == Game.Game.MessageType.Result && player != null) roundResults[player] = message; };
                        roundResults.Clear(); currentBet = 10; InitVisuals(); currentState = GameState.Betting;
                    }
                    else if (Raylib.CheckCollisionPointRec(mousePos, quitLossBtn)) keepPlaying = false;
                }
                else if (!IsCatchingUp())
                {
                    if (Raylib.CheckCollisionPointRec(mousePos, pauseBtn)) { previousState = currentState; currentState = GameState.Paused; }
                    else if (currentState == GameState.Betting)
                    {
                        if (Raylib.CheckCollisionPointRec(mousePos, betMinusBtn)) currentBet = Math.Max(1, currentBet - 10); 
                        else if (Raylib.CheckCollisionPointRec(mousePos, betPlusBtn)) currentBet = Math.Min(game.player.Money, currentBet + 10); 
                        else if (Raylib.CheckCollisionPointRec(mousePos, betMaxBtn)) currentBet = game.player.Money; 
                        else if (Raylib.CheckCollisionPointRec(mousePos, dealBtn)) { game.StartGame(currentBet); currentState = GameState.Playing; }
                    }
                    else if (currentState == GameState.Playing)
                    {
                        if (Raylib.CheckCollisionPointRec(mousePos, hitButton) && GetVisualPoints(game.player.hand, playerVisualCards[game.player]) <= 21)
                        {
                            game.PlayerHit(); if (game.player.Points > 21) currentState = GameState.GameOver;
                        }
                        else if (Raylib.CheckCollisionPointRec(mousePos, standButton)) { game.FinishGame(); currentState = GameState.GameOver; }
                    }
                    else if (currentState == GameState.GameOver)
                    {
                        if (Raylib.CheckCollisionPointRec(mousePos, playAgainBtn))
                        {
                            if (game.player.Money > 0) { game.PrepareNewRound(); roundResults.Clear(); currentBet = Math.Min(currentBet, game.player.Money); InitVisuals(); currentState = GameState.Betting; }
                            else currentState = GameState.TotalLoss;
                        }
                    }
                }
            }

            Raylib.BeginDrawing();
            Raylib.DrawTexturePro(bgTexture, new Rectangle(0, 0, bgTexture.Width, bgTexture.Height), new Rectangle(0, 0, 1600, 1200), new Vector2(0, 0), 0f, Color.White);

            int dealerX = 700; int dealerY = 40;
            DrawUA("- ДИЛЕР -", dealerX, dealerY, 40, Color.Gold);
            if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss)
            {
                DrawUA($"Очки: {GetVisualPoints(game.Dealer.hand, dealerVisualCards)}", dealerX, dealerY + 50, 40, Color.White);
                int dYOffset = dealerY + 100; int currentCardX = dealerX;
                for (int i = 0; i < dealerVisualCards; i++) { Rectangle src = GetCardSourceRec(game.Dealer.hand[i], cardsSheet); Rectangle dest = new Rectangle(currentCardX, dYOffset, destCardWidth, destCardHeight); Raylib.DrawTexturePro(cardsSheet, src, dest, new Vector2(0, 0), 0f, Color.White); currentCardX += 40; }
            }

            int playerX = 40; int playerY = 700;
            DrawUA("- ВИ -", playerX, playerY, 40, Color.Green);
            DrawUA($"Баланс: {game.player.Money} $", playerX, playerY + 50, 40, Color.Gold);
            if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss)
            {
                DrawUA($"Ставка: {game.player.Bet} $", playerX, playerY + 90, 40, Color.Gold);
                DrawUA($"Очки: {GetVisualPoints(game.player.hand, playerVisualCards[game.player])}", playerX, playerY + 130, 40, Color.White);
                int pYOffset = playerY + 180; int currentCardX = playerX;
                for (int i = 0; i < playerVisualCards[game.player]; i++) { Rectangle src = GetCardSourceRec(game.player.hand[i], cardsSheet); Rectangle dest = new Rectangle(currentCardX, pYOffset, destCardWidth, destCardHeight); Raylib.DrawTexturePro(cardsSheet, src, dest, new Vector2(0, 0), 0f, Color.White); currentCardX += 40; }
                if (currentState == GameState.GameOver && roundResults.ContainsKey(game.player) && !IsCatchingUp()) { string res = roundResults[game.player]; int textY = pYOffset + destCardHeight + 15; int textW = (int)Raylib.MeasureTextEx(uaFont, res, 36, 2).X; Raylib.DrawRectangle(playerX - 5, textY - 5, textW + 10, 36 + 10, new Color(0, 0, 0, 150)); DrawUA(res, playerX, textY, 36, Color.Yellow); }
            }

            for (int i = 0; i < game.ActiveBots.Count; i++)
            {
                var bot = game.ActiveBots[i]; int bX = (int)botPositions[i].X; int bY = (int)botPositions[i].Y;
                DrawUA($"- {bot.GetType().Name} -", bX, bY, 40, Color.SkyBlue);
                DrawUA($"Баланс: {bot.Money} $", bX, bY + 50, 36, Color.Gold);
                if (currentState != GameState.Betting && currentState != GameState.Rules && currentState != GameState.TotalLoss)
                {
                    DrawUA($"Ставка: {bot.Bet} $", bX, bY + 90, 36, Color.Gold);
                    DrawUA($"Очки: {GetVisualPoints(bot.hand, playerVisualCards[bot])}", bX, bY + 130, 36, Color.White);
                    int botCardY = bY + 180; int currentCardX = bX;
                    for (int j = 0; j < playerVisualCards[bot]; j++) { Rectangle src = GetCardSourceRec(bot.hand[j], cardsSheet); Rectangle dest = new Rectangle(currentCardX, botCardY, destCardWidth, destCardHeight); Raylib.DrawTexturePro(cardsSheet, src, dest, new Vector2(0, 0), 0f, Color.White); currentCardX += 40; }
                    if (currentState == GameState.GameOver && roundResults.ContainsKey(bot) && !IsCatchingUp()) { string res = roundResults[bot]; int textY = botCardY + destCardHeight + 15; int textW = (int)Raylib.MeasureTextEx(uaFont, res, 32, 2).X; Raylib.DrawRectangle(bX - 5, textY - 5, textW + 10, 32 + 10, new Color(0, 0, 0, 150)); DrawUA(res, bX, textY, 32, Color.Yellow); }
                }
            }

            if (currentAnim.Active) { Rectangle dest = new Rectangle((int)currentAnim.CurrentX, (int)currentAnim.CurrentY, destCardWidth, destCardHeight); Color tint = new Color(255, 255, 255, (int)currentAnim.Alpha); Raylib.DrawTexturePro(cardsSheet, currentAnim.SourceRec, dest, new Vector2(0, 0), 0f, tint); }

            if (currentState == GameState.Betting)
            {
                DrawUA("ЗРОБІТЬ ВАШУ СТАВКУ", 620, 720, 40, Color.White);
                Raylib.DrawRectangleRec(betMinusBtn, Color.LightGray); DrawUA("-", (int)betMinusBtn.X + 30, (int)betMinusBtn.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(betPlusBtn, Color.LightGray); DrawUA("+", (int)betPlusBtn.X + 24, (int)betPlusBtn.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(dealBtn, Color.Gold); DrawUA($"ГРАТИ: {currentBet}$", (int)dealBtn.X + 30, (int)dealBtn.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(betMaxBtn, Color.Red); DrawUA("ВА-БАНК", (int)betMaxBtn.X + 70, (int)betMaxBtn.Y + 10, 40, Color.White);
            }
            else if (currentState == GameState.Playing && !IsCatchingUp())
            {
                Raylib.DrawRectangleRec(hitButton, Color.LightGray); DrawUA("ЩЕ", (int)hitButton.X + 64, (int)hitButton.Y + 20, 40, Color.Black);
                Raylib.DrawRectangleRec(standButton, Color.LightGray); DrawUA("ДОСИТЬ", (int)standButton.X + 30, (int)standButton.Y + 20, 40, Color.Black);
            }
            else if (currentState == GameState.GameOver && !IsCatchingUp()) { Raylib.DrawRectangleRec(playAgainBtn, Color.Blue); DrawUA("ЩЕ РАУНД", (int)playAgainBtn.X + 80, (int)playAgainBtn.Y + 30, 40, Color.White); }

            if (currentState == GameState.TotalLoss && !IsCatchingUp())
            {
                Raylib.DrawRectangle(0, 0, 1600, 1200, new Color(0, 0, 0, 220));
                DrawUA("ВИ БАНКРУТ!", 550, 400, 80, Color.Red);
                DrawUA("Казино завжди виграє...", 620, 500, 30, Color.Gray);
                DrawUA("Бажаєте почати нову гру?", 560, 600, 40, Color.White);
                Raylib.DrawRectangleRec(restartBtn, Color.DarkBlue); DrawUA("НОВА ГРА", (int)restartBtn.X + 160, (int)restartBtn.Y + 30, 40, Color.White);
                Raylib.DrawRectangleRec(quitLossBtn, Color.Maroon); DrawUA("ВИЙТИ", (int)quitLossBtn.X + 185, (int)quitLossBtn.Y + 30, 40, Color.White);
            }
            else if (currentState == GameState.Paused || currentState == GameState.Rules)
            {
                Raylib.DrawRectangle(0, 0, 1600, 1200, new Color(0, 0, 0, 180));
                if (currentState == GameState.Paused)
                {
                    DrawUA("--- ПАУЗА ---", 640, 240, 60, Color.Gold);
                    Raylib.DrawRectangleRec(resumeBtn, Color.LightGray); DrawUA("ПРОДОВЖИТИ", (int)resumeBtn.X + 70, (int)resumeBtn.Y + 30, 40, Color.Black);
                    Raylib.DrawRectangleRec(rulesBtn, Color.LightGray); DrawUA("ПРАВИЛА", (int)rulesBtn.X + 115, (int)rulesBtn.Y + 30, 40, Color.Black);
                    Raylib.DrawRectangleRec(quitBtn, Color.Red); DrawUA("ВИЙТИ З ГРИ", (int)quitBtn.X + 85, (int)quitBtn.Y + 30, 40, Color.White);
                }
                else if (currentState == GameState.Rules)
                {
                    DrawUA("--- ПРАВИЛА ---", 620, 120, 60, Color.Gold);
                    DrawUA("1. Мета: Набрати більше очок, ніж дилер, але не більше 21.", 160, 300, 40, Color.White);
                    DrawUA("2. Карти 2-10 мають номінал відповідно до числа.", 160, 400, 40, Color.White);
                    DrawUA("3. Король, Дама та Валет дають по 10 очок.", 160, 500, 40, Color.White);
                    DrawUA("4. Туз дає 1 або 11 очок, залежно від потреби.", 160, 600, 40, Color.White);
                    DrawUA("5. 'Ще' - взяти карту, 'Досить' - залишити поточну суму.", 160, 700, 40, Color.White);
                    Raylib.DrawRectangleRec(backBtn, Color.LightGray);
                    if (isFirstTimeRules) DrawUA("ЗРОЗУМІЛО", (int)backBtn.X + 100, (int)backBtn.Y + 30, 40, Color.Black);
                    else DrawUA("НАЗАД", (int)backBtn.X + 140, (int)backBtn.Y + 30, 40, Color.Black);
                }
            }
            else if (!IsCatchingUp()) { Raylib.DrawRectangleRec(pauseBtn, Color.DarkGray); DrawUA("||", (int)pauseBtn.X + 30, (int)pauseBtn.Y + 20, 40, Color.White); }

            Raylib.EndDrawing();
        }
        Raylib.UnloadFont(uaFont); Raylib.UnloadTexture(bgTexture); Raylib.UnloadTexture(cardsSheet); Raylib.CloseWindow();
    }
}
