using Raylib_cs;
using System;

class GameLoop
{
    public GameLoop()
    {
        SetupGrid gameSetup = new SetupGrid();
        Tetrimino currentTetrimino = new Tetrimino();
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
                gameSetup.ClearFullLines();

                if (locked)
                {
                    if (currentTetrimino.Y == 0)
                    {
                        gameOver = true;
                    }
                    else
                    {
                        score += 1;
                        currentTetrimino = new Tetrimino();
                    }
                }

                timer = 0.0f;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);
            gameSetup.DrawGrid();
            currentTetrimino.DrawTetrimino();
            Raylib.DrawText(string.Format("Integer value: {0}", score), 350, 250, 40, Color.BLACK);
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
}
