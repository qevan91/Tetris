using Raylib_cs;
using System;

class RandomGameMode
{
    private SetupGrid gameSetup;
    private Tetrimino currentTetrimino;
    private Tetrimino nextTetrimino;
    private int score;
    private float timer;
    private float dropTime;
    private Random random;
    private bool gameOver;

    public RandomGameMode()
    {
        gameSetup = new SetupGrid();
        currentTetrimino = new Tetrimino();
        nextTetrimino = new Tetrimino();
        score = 0;
        timer = 0.0f;
        dropTime = 0.5f;
        random = new Random();
        gameOver = false;
    }

    public void UpdateAndDrawGame()
    {
        gameOver = false;
        timer += Raylib.GetFrameTime();

        if (random.NextDouble() < 0.05)
        {
            if (random.Next(2) == 0)
                currentTetrimino.MoveLeft(gameSetup);
            else
                currentTetrimino.MoveRight(gameSetup);
        }

        if (random.NextDouble() < 0.0005)
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

        if (gameOver)
        {
            gameSetup.InitializeGrid();
            currentTetrimino = nextTetrimino;
        }

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RAYWHITE);
        gameSetup.DrawGrid();
        currentTetrimino.DrawTetrimino();
        Raylib.DrawText($"Score: {score}", 1600, 100, 40, Color.BLACK);
    }
}