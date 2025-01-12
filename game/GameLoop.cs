using Raylib_cs;
using System;

class GameLoop
{
    private Tetrimino holdTetrimino;
    private KeyBinding keyBinding;
    private SaveScore saveScore;

    public GameLoop()
    {
        keyBinding = KeyBinding.LoadBindings();
        SetupGrid gameSetup = new SetupGrid();
        Tetrimino currentTetrimino = new Tetrimino();
        Tetrimino nextTetrimino = new Tetrimino();
        int score = 0;
        float timer = 0.0f;
        float dropTime = 0.5f;
        bool gameOver = false;
        bool isPaused = false;
        bool alreadyReached = false;
        bool haveOldTetrimino = false;
        saveScore = new SaveScore();
        int bestScore = saveScore.BestScore;

        while (!Raylib.WindowShouldClose() && !gameOver)
        {
            dropTime = 0.5f - score / 20000.0f;
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_L))
            {
                var loadedData = Save.LoadGame();
                if (loadedData != null)
                {
                    gameSetup.Grid = Save.ConvertTo2D(loadedData.Grid, SetupGrid.Rows, SetupGrid.Columns);
                    currentTetrimino = new Tetrimino(
                        loadedData.CurrentTetrimino.Shape,
                        new Color(loadedData.CurrentTetrimino.Color.r, loadedData.CurrentTetrimino.Color.g, loadedData.CurrentTetrimino.Color.b, loadedData.CurrentTetrimino.Color.a),
                        loadedData.CurrentTetrimino.X,
                        loadedData.CurrentTetrimino.Y
                    );
                    nextTetrimino = new Tetrimino(
                        loadedData.NextTetrimino.Shape,
                        new Color(loadedData.NextTetrimino.Color.r, loadedData.NextTetrimino.Color.g, loadedData.NextTetrimino.Color.b, loadedData.NextTetrimino.Color.a),
                        loadedData.NextTetrimino.X,
                        loadedData.NextTetrimino.Y
                    );
                    score = loadedData.Score;
                    Raylib.DrawText("Game loaded", 840, 900, 40, Color.PURPLE);
                }
                else
                {
                    Raylib.DrawText("Cannot save", 840, 900, 40, Color.RED);
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
            {
                Save.SaveGame(gameSetup, currentTetrimino, nextTetrimino, score);
                Raylib.DrawText("Game saved", 840, 900, 40, Color.PURPLE);
            }

            if (Raylib.IsKeyPressed(keyBinding.Pause))
            {
                isPaused = !isPaused;
            }

            if (!isPaused)
            {
                timer += Raylib.GetFrameTime();

                if (Raylib.IsKeyPressed(keyBinding.MoveLeft))
                {
                    currentTetrimino.MoveLeft(gameSetup);
                }
                if (Raylib.IsKeyPressed(keyBinding.MoveRight))
                {
                    currentTetrimino.MoveRight(gameSetup);
                }
                if (Raylib.IsKeyDown(keyBinding.MoveDown))
                {
                    currentTetrimino.MoveDown(gameSetup);
                }
                if (Raylib.IsKeyPressed(keyBinding.Rotate))
                {
                    currentTetrimino.Rotate(gameSetup);
                }
                if (Raylib.IsKeyPressed(keyBinding.Hold))
                {
                    if (holdTetrimino == null)
                    {
                        holdTetrimino = currentTetrimino;
                        currentTetrimino = nextTetrimino;
                        nextTetrimino = new Tetrimino();
                    }
                    else
                    {
                        Tetrimino temp = currentTetrimino;
                        currentTetrimino = holdTetrimino;
                        holdTetrimino = temp;
                        currentTetrimino.X = 3;
                        currentTetrimino.Y = 0;
                    }
                    haveOldTetrimino = true;
                }

                if (timer >= dropTime)
                {
                    bool locked = currentTetrimino.MoveDown(gameSetup);
                    int linesCleared = gameSetup.ClearFullLines();

                    if (linesCleared > 0)
                    {
                        score += linesCleared * 100;

                        if (score == 3000 && !alreadyReached)
                        {
                            gameSetup.InitializeGrid();
                            alreadyReached = true;
                            Raylib.DrawText("Screen clear GG", 960, 200, 20, Color.PURPLE);
                        }
                    }

                    if (locked)
                    {
                        if (currentTetrimino.Y == 0)
                        {
                            gameOver = true;
                        }
                        else
                        {
                            currentTetrimino = nextTetrimino;
                            nextTetrimino = new Tetrimino();
                        }
                    }
                    timer = 0.0f;
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);
            gameSetup.DrawGrid();
            currentTetrimino.DrawTetrimino();
            Raylib.DrawText($"Score: {score}", 840, 100, 40, Color.BLACK);
            Raylib.DrawText($"Best Score: {bestScore}", 840, 150, 40, Color.BLACK);
            Raylib.DrawText("Next:", 1250, 450, 50, Color.BLACK);
            Raylib.DrawText($"Move Down: {keyBinding.MoveDown}", 50, 350, 20, Color.GREEN);
            Raylib.DrawText($"Move Left: {keyBinding.MoveLeft}", 50, 400, 20, Color.GREEN);
            Raylib.DrawText($"Move Right: {keyBinding.MoveRight}", 50, 450, 20, Color.GREEN);
            Raylib.DrawText($"Rotation: {keyBinding.Rotate}", 50, 500, 20, Color.GREEN);
            Raylib.DrawText($"Stock Button: {keyBinding.Hold}", 50, 550, 20, Color.GREEN);
            Raylib.DrawText("Quit Button: Echap", 1600, 350, 20, Color.GREEN);
            Raylib.DrawText("Save Button: S", 1600, 400, 20, Color.GREEN);
            Raylib.DrawText("Load Button: L", 1600, 450, 20, Color.GREEN);
            Raylib.DrawText($"Ralentisseur: {dropTime}", 1600, 500, 20, Color.GREEN);
            Raylib.DrawText($"Pause: {keyBinding.Pause}", 1600, 550, 20, Color.GREEN);
            Raylib.DrawText($"Hold:", 600, 450, 50, Color.BLACK);
            DrawTetrimino(nextTetrimino, 1250, 550);
            if (haveOldTetrimino)
            {
                DrawTetrimino(holdTetrimino, 600, 550);
            }

            if (isPaused)
            {
                Raylib.DrawText("Paused", 1600, 150, 40, Color.PURPLE);
            }

            Raylib.EndDrawing();
        }

        if (gameOver)
        {
            saveScore.SaveBestScore(score);
            ShowGameOverScreen();
        }
    }

    private void ShowGameOverScreen()
    {
        GameOver gameOverScreen = new GameOver();
        bool exit = false;

        while (!exit && !Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            string action = gameOverScreen.HandleInput();
            gameOverScreen.Draw();
            Raylib.EndDrawing();

            if (action == "Retry")
            {
                new GameLoop();
                exit = true;
            }
            else if (action == "Quit")
            {
                exit = true;
            }
        }
    }

    private void DrawTetrimino(Tetrimino Tetrimino, int offsetX, int offsetY)
    {
        for (int row = 0; row < Tetrimino.Shape.GetLength(0); row++)
        {
            for (int col = 0; col < Tetrimino.Shape.GetLength(1); col++)
            {
                if (Tetrimino.Shape[row, col] == 1)
                {
                    Raylib.DrawRectangle(offsetX + col * Tetrimino.CellSize, offsetY + row * Tetrimino.CellSize, Tetrimino.CellSize, Tetrimino.CellSize, Tetrimino.Color);
                    Raylib.DrawRectangleLines(offsetX + col * Tetrimino.CellSize, offsetY + row * Tetrimino.CellSize, Tetrimino.CellSize, Tetrimino.CellSize, Color.BLACK);
                }
            }
        }
    }
}