using Raylib_cs;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Fenêtre Raylib en .NET 5.0");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);
            Raylib.DrawText("T triste", 150, 200, 20, Color.DARKGRAY);
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}