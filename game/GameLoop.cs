using Raylib_cs;
using System;

class GameLoop
{
    public static void StartGame()
    {
        new GameLoop();
    }

    public GameLoop()
    {
        SetupGrid gameSetup = new SetupGrid();
        Tetrimino currentTetrimino = new Tetrimino();
        Tetrimino nextTetrimino = new Tetrimino();
        int score = 0;
        float timer = 0.0f;
        float dropTime = 0.5f;
        bool gameOver = false;
        bool isPaused = false;

        while (!Raylib.WindowShouldClose() && !gameOver)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_L))
            {
                Console.WriteLine("Tentative de chargement");
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
                    Console.WriteLine("Game Loaded");
                }
                else
                {
                    Console.WriteLine("Nan ça fonctionne pas");
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
            {
                Save.SaveGame(gameSetup, currentTetrimino, nextTetrimino, score);
                Console.WriteLine("Game Saved");
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                isPaused = !isPaused;
            }

            if (!isPaused)
            {
                timer += Raylib.GetFrameTime();

                dropTime = Math.Max(0.1f, 0.5f - (score / 1000.0f));

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
                {
                    currentTetrimino.MoveLeft(gameSetup);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
                {
                    currentTetrimino.MoveRight(gameSetup);
                }
                if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                {
                    currentTetrimino.MoveDown(gameSetup);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                {
                    currentTetrimino.Rotate(gameSetup);
                }

                if (timer >= dropTime)
                {
                    bool locked = currentTetrimino.MoveDown(gameSetup);
                    int linesCleared = gameSetup.ClearFullLines();

                    if (linesCleared > 0)
                    {
                        score += linesCleared * 100;

                        if (score >= 1000)
                        {
                            gameSetup.InitializeGrid();
                            Console.WriteLine("Grille effacée car le score a atteint 500");
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
            DrawNextTetrimino(nextTetrimino, 1600, 230);


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
        bool exit = false;
        while (!exit)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);
            Raylib.DrawText("Game Over!", 350, 250, 40, Color.RED);
            Raylib.DrawText("Voulez-vous rejouer ?", 350, 300, 20, Color.RED);
            Raylib.DrawText("1. Oui", 350, 350, 20, Color.RED);
            Raylib.DrawText("2. Non", 350, 400, 20, Color.RED);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                Console.WriteLine("1");
                StartGame();
                exit = true;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
            {
                Console.WriteLine("2");
                Raylib.EndDrawing();
                exit = true;
            }

            Raylib.EndDrawing();
        }
    }

    private void DrawNextTetrimino(Tetrimino nextTetrimino, int offsetX, int offsetY)
    {
        for (int row = 0; row < nextTetrimino.Shape.GetLength(0); row++)
        {
            for (int col = 0; col < nextTetrimino.Shape.GetLength(1); col++)
            {
                if (nextTetrimino.Shape[row, col] == 1)
                {
                    Raylib.DrawRectangle(offsetX + col * Tetrimino.CellSize, offsetY + row * Tetrimino.CellSize, Tetrimino.CellSize, Tetrimino.CellSize, nextTetrimino.Color);
                    Raylib.DrawRectangleLines(offsetX + col * Tetrimino.CellSize, offsetY + row * Tetrimino.CellSize, Tetrimino.CellSize, Tetrimino.CellSize, Color.BLACK);
                }
            }
        }
    }
}
