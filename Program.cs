using Raylib_cs;
using System;

public class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(800, 600, "Tetris");
        Raylib.SetTargetFPS(60);

        MainMenu menu = new MainMenu();

        while (!Raylib.WindowShouldClose())
        {
            menu.Draw();
            string menuAction = menu.HandleInput();

            if (menuAction == "Start")
            {
                Console.WriteLine("Jeu démarré");
            }
            else if (menuAction == "Quit")
            {
                Raylib.CloseWindow();
            }
            else if (menuAction == "MusicStopped")
            {
                Console.WriteLine("Musique coupée");
            }

            Raylib.BeginDrawing();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
