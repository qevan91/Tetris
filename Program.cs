using Raylib_cs;
using System;

public class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1920, 1080, "Tetris");
        Raylib.SetTargetFPS(60);

        MainMenu menu = new MainMenu();
        RandomGameMode randomGame = new RandomGameMode();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.ClearBackground(Color.DARKGRAY);

            randomGame.UpdateAndDrawGame();

            menu.Draw();

            string menuAction = menu.HandleInput();

            if (menuAction == "Start")
            {
                Console.WriteLine("Jeu démarré");
                GameLoop gameSetup = new GameLoop();
            }
            else if (menuAction == "Quit")
            {
                Raylib.CloseWindow();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}