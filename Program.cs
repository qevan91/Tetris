using Raylib_cs;
using System.Threading;
class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Menu avec Raylib en C#");
        Raylib.SetTargetFPS(60);

        string[] options = { "Jouer", "Option", "Quitter" };
        int selectedIndex = 0;
        bool menuActive = true;

        while (!Raylib.WindowShouldClose() && menuActive)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            DrawMenu(options, selectedIndex);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                menuActive = ExecuteOption(selectedIndex);
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    static void DrawMenu(string[] options, int selectedIndex)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Raylib.DrawText($"> {options[i]}", 350, 200 + i * 50, 30, Color.GREEN);
            }
            else
            {
                Raylib.DrawText(options[i], 350, 200 + i * 50, 30, Color.DARKGRAY);
            }
        }
    }

    static bool ExecuteOption(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                Raylib.ClearBackground(Color.RAYWHITE);
                Raylib.BeginDrawing();
                Raylib.DrawText("Vous avez choisi de jouer !", 200, 300, 20, Color.DARKGRAY);
                Raylib.EndDrawing();
                Thread.Sleep(2000);
                return true;

            case 1:
                Raylib.ClearBackground(Color.RAYWHITE);
                Raylib.BeginDrawing();
                Raylib.DrawText("Options du jeu (en développement) !", 150, 300, 20, Color.DARKGRAY);
                Raylib.EndDrawing();
                Thread.Sleep(2000);
                return true;

            case 2:
                return false;
        }

        return true;
    }
}
