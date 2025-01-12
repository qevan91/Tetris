using Raylib_cs;
using System;

class GameLoop
{
    private Tetrimino holdTetrimino;
    private KeyBinding keyBinding;

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
                    Raylib.DrawText("Game loaded", 1600, 750, 40, Color.PURPLE);
                }
                else
                {
                    Raylib.DrawText("Cannot save", 1600, 750, 40, Color.RED);
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
            {
                Save.SaveGame(gameSetup, currentTetrimino, nextTetrimino, score);
                Raylib.DrawText("Game saved", 1600, 700, 40, Color.PURPLE);
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
            Raylib.DrawText($"Score: {score}", 1600, 100, 40, Color.BLACK);
            Raylib.DrawText("Next:", 1600, 200, 20, Color.BLACK);
            Raylib.DrawText($"Move Down: {keyBinding.MoveDown}", 1600, 450, 20, Color.GREEN);
            Raylib.DrawText($"Move Left: {keyBinding.MoveLeft}", 1600, 500, 20, Color.GREEN);
            Raylib.DrawText($"Move Right: {keyBinding.MoveRight}", 1600, 550, 20, Color.GREEN);
            Raylib.DrawText($"Rotation: {keyBinding.Rotate}", 1600, 600, 20, Color.GREEN);
            Raylib.DrawText($"Pause: {keyBinding.Pause}", 1600, 650, 20, Color.GREEN);
            Raylib.DrawText($"Stock Button: {keyBinding.Hold}", 1600, 700, 20, Color.GREEN);
            Raylib.DrawText("Quit Button: Echap", 1600, 750, 20, Color.GREEN);
            Raylib.DrawText("Save Button: S", 1600, 800, 20, Color.GREEN);
            Raylib.DrawText("Load Button: L", 1600, 850, 20, Color.GREEN);
            Raylib.DrawText($"Ralentisseur: {dropTime}", 1600, 900, 20, Color.GREEN);
            Raylib.DrawText($"Hold:", 1600, 300, 20, Color.BLACK);
            DrawTetrimino(nextTetrimino, 1600, 230);
            if (haveOldTetrimino)
            {
                DrawTetrimino(holdTetrimino, 1600, 330);
            }

            if (isPaused)
            {
                Raylib.DrawText("Paused", 1600, 150, 40, Color.PURPLE);
            }

            Raylib.EndDrawing();
        }

        if (gameOver)
        {
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
                Console.WriteLine("Quitter le jeu");
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