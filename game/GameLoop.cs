using Raylib_cs;
using System;

class GameLoop
{
    public GameLoop()
    {
        SetupGrid gameSetup = new SetupGrid();
        Tetrimino currentTetrimino = new Tetrimino();
        Tetrimino nextTetrimino = new Tetrimino();
        float timer = 0.0f;
        float dropTime = 0.5f;
        bool gameOver = false;
        int score = 0;

        while (!Raylib.WindowShouldClose() && !gameOver)
        {
            timer += Raylib.GetFrameTime();

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

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            gameSetup.DrawGrid();
            currentTetrimino.DrawTetrimino();

            Raylib.DrawText(string.Format("Score: {0}", score), 350, 250, 40, Color.BLACK);

            Raylib.DrawText("Next:", 350, 320, 20, Color.BLACK);
            DrawNextTetrimino(nextTetrimino, 350, 350);

            Raylib.EndDrawing();
        }

        if (gameOver)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);
            Raylib.DrawText("Game Over!", 350, 250, 40, Color.RED);
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
