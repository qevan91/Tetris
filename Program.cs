using Raylib_cs;
using System;

public class Program
{

    enum GameState
    {
        Menu,
        Playing
    }

    static void Main(string[] args)
    {
        Raylib.InitWindow(800, 600, "Tetris");
        Raylib.SetTargetFPS(60);

        Music musicPlayer = new Music();
        musicPlayer.PlayMusic("bin/Debug/Tetris-Soundtrack.wav");
        
        Console.WriteLine("La musique se joue");
        MainMenu mainMenu = new MainMenu();
        SetupGrid gameSetup = new SetupGrid();
        Tetrimino currentTetrimino = new Tetrimino();
        float timer = 0.0f;
        float dropTime = 0.5f;

        GameState currentState = GameState.Menu;

        while (!Raylib.WindowShouldClose())
        {
            if (currentState == GameState.Menu)
            {
                string menuAction = mainMenu.HandleInput();

                if (menuAction == "Start")
                {
                    currentState = GameState.Playing; 
                }
                else if (menuAction == "Quit")
                {
                    break; 
                }
            }
            else if (currentState == GameState.Playing)
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
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DARKGRAY);

            if (currentState == GameState.Menu)
            {
                mainMenu.Draw(); 
            }
            else if (currentState == GameState.Playing)
            {
                gameSetup.DrawGrid();  
                currentTetrimino.DrawTetrimino();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow(); 
    }
}
