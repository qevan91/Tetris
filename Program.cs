using Raylib_cs;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Tetris");
        Raylib.SetTargetFPS(60);

        SetupGrid gameSetup = new SetupGrid();
        Tetrimino currentTetrimino = new Tetrimino();
        float timer = 0.0f;
        float dropTime = 0.5f;

        while (!Raylib.WindowShouldClose())
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
                currentTetrimino.MoveDown(gameSetup);
                gameSetup.ClearFullLines();
                timer = 0.0f;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            gameSetup.DrawGrid();
            currentTetrimino.DrawTetrimino();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}