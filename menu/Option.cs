using Raylib_cs;
using System.Numerics;

public class Option
{
    private Rectangle menuButton = new Rectangle(300, 400, 200, 50);
    private Rectangle stopMusicButton = new Rectangle(300, 300, 200, 50);

    public void Draw()
    {
        Raylib.DrawRectangleRec(menuButton, Color.LIGHTGRAY);
        Raylib.DrawText("Menu", (int)menuButton.x + 25, (int)menuButton.y + 15, 20, Color.BLACK);

        Raylib.DrawRectangleRec(stopMusicButton, Color.LIGHTGRAY);
        Raylib.DrawText("Stop Music", (int)stopMusicButton.x + 25, (int)stopMusicButton.y + 15, 20, Color.BLACK);
    }
}