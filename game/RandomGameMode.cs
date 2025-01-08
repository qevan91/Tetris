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
    private bool gameOver;
    private Random random;

    public RandomGameMode()
    {
        gameSetup = new SetupGrid();
        currentTetrimino = new Tetrimino();
        nextTetrimino = new Tetrimino();
        score = 0;
        timer = 0.0f;
        dropTime = 0.5f;
        gameOver = false;
        random = new Random();
    }

    public void UpdateAndDrawGame()
    {
        timer += Raylib.GetFrameTime();

        if (random.NextDouble() < 0.2)
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

                if (score >= 500)
                {
                    dropTime = 0.3f;
                }
            }

            if (locked)
            {
                if (currentTetrimino.Y == 0)
                {
                    gameOver = true;
                    gameSetup.InitializeGrid();
                }
                else
                {
                    currentTetrimino = nextTetrimino;
                    nextTetrimino = new Tetrimino();
                }
            }

            timer = 0.0f;
        }

        gameSetup.DrawGrid();
        currentTetrimino.DrawTetrimino();
        Raylib.DrawText($"Score: {score}", 1024 + 350, 0 + 250, 40, Color.BLACK);
    }
}