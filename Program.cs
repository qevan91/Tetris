using Raylib_cs;
using System;

public class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1920, 1080, "Tetris");
        Raylib.SetTargetFPS(60);

        MainMenu menu = new MainMenu();

        while (!Raylib.WindowShouldClose())
        {
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
            else if (menuAction == "MusicStopped")
            {
                Console.WriteLine("Musique coupée");
                Console.WriteLine("Menu Coupe");

            }

            Raylib.BeginDrawing();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}