using Raylib_cs;
using System;

public class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1920, 1080, "Tetris");
        Raylib.SetTargetFPS(60);

        MainMenu menu = new MainMenu();
        Option optionMenu = new Option();
        RandomGameMode randomGame = new RandomGameMode();
        bool inOptionMenu = false;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.ClearBackground(Color.DARKGRAY);

            if (inOptionMenu)
            {
                optionMenu.Draw();
                if (!optionMenu.HandleInput())
                {
                    inOptionMenu = false;
                }
            }
            else
            {
                randomGame.UpdateAndDrawGame();
                menu.Draw();

                string menuAction = menu.HandleInput();

                if (menuAction == "Start")
                {
                    GameLoop gameSetup = new GameLoop();
                }
                else if (menuAction == "Game Mode")
                {
                    GameMod.StartGame();
                }
                else if (menuAction == "1v1")
                {
                    OnevOne gameSetup = new OnevOne();
                }
                else if (menuAction == "Option")
                {
                    inOptionMenu = true;
                }
                else if (menuAction == "Quit")
                {
                    Raylib.CloseWindow();
                }
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
