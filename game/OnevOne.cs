using Raylib_cs;
using System;

public class OnevOne
{
    private Tetrimino holdTetrimino1;
    private Tetrimino holdTetrimino2;
    private KeyBinding keyBinding;

    public OnevOne()
    {
        keyBinding = KeyBinding.LoadBindings();

        SetupGrid gameSetup1 = new SetupGrid();
        Tetrimino currentTetrimino1 = new Tetrimino();
        Tetrimino nextTetrimino1 = new Tetrimino();
        int score1 = 0;
        float timer1 = 0.0f;
        float dropTime1 = 0.5f - (score1 / 900.0f);
        bool gameOver1 = false;
        bool alreadyReached1 = false;
        bool haveOldTetrimino1 = false;

        SetupGrid gameSetup2 = new SetupGrid();
        Tetrimino currentTetrimino2 = new Tetrimino();
        Tetrimino nextTetrimino2 = new Tetrimino();
        int score2 = 0;
        float timer2 = 0.0f;
        float dropTime2 = 0.5f - (score2 / 900.0f);
        bool gameOver2 = false;
        bool alreadyReached2 = false;
        bool haveOldTetrimino2 = false;

        while (!Raylib.WindowShouldClose() && !gameOver1 && !gameOver2)
        {
            timer1 += Raylib.GetFrameTime();
            timer2 += Raylib.GetFrameTime();

            if (Raylib.IsKeyPressed(keyBinding.MoveLeft))
            {
                currentTetrimino1.MoveLeft(gameSetup1);
            }
            if (Raylib.IsKeyPressed(keyBinding.MoveRight))
            {
                currentTetrimino1.MoveRight(gameSetup1);
            }
            if (Raylib.IsKeyDown(keyBinding.MoveDown))
            {
                currentTetrimino1.MoveDown(gameSetup1);
            }
            if (Raylib.IsKeyPressed(keyBinding.Rotate))
            {
                currentTetrimino1.Rotate(gameSetup1);
            }
            if (Raylib.IsKeyPressed(keyBinding.Hold))
            {
                if (holdTetrimino1 == null)
                {
                    holdTetrimino1 = currentTetrimino1;
                    currentTetrimino1 = nextTetrimino1;
                    nextTetrimino1 = new Tetrimino();
                }
                else
                {
                    Tetrimino temp = currentTetrimino1;
                    currentTetrimino1 = holdTetrimino1;
                    holdTetrimino1 = temp;
                    currentTetrimino1.X = 3;
                    currentTetrimino1.Y = 0;
                }
                haveOldTetrimino1 = true;
            }

            if (Raylib.IsKeyPressed(keyBinding.MoveLeft2))
            {
                currentTetrimino2.MoveLeft(gameSetup2);
            }
            if (Raylib.IsKeyPressed(keyBinding.MoveRight2))
            {
                currentTetrimino2.MoveRight(gameSetup2);
            }
            if (Raylib.IsKeyDown(keyBinding.MoveDown2))
            {
                currentTetrimino2.MoveDown(gameSetup2);
            }
            if (Raylib.IsKeyPressed(keyBinding.Rotate2))
            {
                currentTetrimino2.Rotate(gameSetup2);
            }
            if (Raylib.IsKeyPressed(keyBinding.Hold2))
            {
                if (holdTetrimino2 == null)
                {
                    holdTetrimino2 = currentTetrimino2;
                    currentTetrimino2 = nextTetrimino2;
                    nextTetrimino2 = new Tetrimino();
                }
                else
                {
                    Tetrimino temp = currentTetrimino2;
                    currentTetrimino2 = holdTetrimino2;
                    holdTetrimino2 = temp;
                    currentTetrimino2.X = 3;
                    currentTetrimino2.Y = 0;
                }
                haveOldTetrimino2 = true;
            }

            if (timer1 >= dropTime1)
            {
                bool locked1 = currentTetrimino1.MoveDown(gameSetup1);
                int linesCleared1 = gameSetup1.ClearFullLines();

                if (linesCleared1 > 0)
                {
                    score1 += linesCleared1 * 100;

                    if (score1 == 3000 && !alreadyReached1)
                    {
                        gameSetup1.InitializeGrid();
                        alreadyReached1 = true;
                    }
                }

                if (locked1)
                {
                    if (currentTetrimino1.Y == 0)
                    {
                        gameOver1 = true;
                    }
                    else
                    {
                        currentTetrimino1 = nextTetrimino1;
                        nextTetrimino1 = new Tetrimino();
                    }
                }
                timer1 = 0.0f;
            }

            if (timer2 >= dropTime2)
            {
                bool locked2 = currentTetrimino2.MoveDown(gameSetup2);
                int linesCleared2 = gameSetup2.ClearFullLines();

                if (linesCleared2 > 0)
                {
                    score2 += linesCleared2 * 100;

                    if (score2 == 3000 && !alreadyReached2)
                    {
                        gameSetup2.InitializeGrid();
                        alreadyReached2 = true;
                    }
                }

                if (locked2)
                {
                    if (currentTetrimino2.Y == 0)
                    {
                        gameOver2 = true;
                    }
                    else
                    {
                        currentTetrimino2 = nextTetrimino2;
                        nextTetrimino2 = new Tetrimino();
                    }
                }
                timer2 = 0.0f;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);
            gameSetup1.DrawGrid1();
            currentTetrimino1.DrawTetrimino1();
            gameSetup2.DrawGrid2();
            currentTetrimino2.DrawTetrimino2();
            Raylib.DrawText($"Score Player 1: {score1}", 1600, 100, 40, Color.BLACK);
            Raylib.DrawText($"Score Player 2: {score2}", 1600, 150, 40, Color.BLACK);
            Raylib.DrawText("Next Player 1:", 1600, 200, 20, Color.BLACK);
            Raylib.DrawText("Next Player 2:", 1600, 300, 20, Color.BLACK);
            DrawTetrimino(nextTetrimino1, 1600, 230);
            DrawTetrimino(nextTetrimino2, 1600, 330);
            if (haveOldTetrimino1)
            {
                DrawTetrimino(holdTetrimino1, 1600, 400);
            }
            if (haveOldTetrimino2)
            {
                DrawTetrimino(holdTetrimino2, 1600, 500);
            }

            Raylib.EndDrawing();
        }

        if (gameOver1 || gameOver2)
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
                new OnevOne();
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