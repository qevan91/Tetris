using Raylib_cs;
using System.Numerics;
using NAudio.Wave;

public class MainMenu
{
    private Music musicPlayer = new Music();
    private Rectangle startButton = new Rectangle(300, 200, 200, 50);
    private Rectangle quitButton = new Rectangle(300, 300, 200, 50);
    private Rectangle stopMusicButton = new Rectangle(300, 400, 200, 50);

    public MainMenu()
    {
        musicPlayer.PlayMusic("bin/Debug/Tetris-Soundtrack.wav");
    }
    public void Draw()
    {
        Raylib.ClearBackground(Color.DARKGRAY);
        Raylib.DrawText("Menu Principal", 320, 100, 20, Color.WHITE);

        Raylib.DrawRectangleRec(startButton, Color.LIGHTGRAY);
        Raylib.DrawText("Start Game", (int)startButton.x + 50, (int)startButton.y + 15, 20, Color.BLACK);

        Raylib.DrawRectangleRec(quitButton, Color.LIGHTGRAY);
        Raylib.DrawText("Quit Game", (int)quitButton.x + 50, (int)quitButton.y + 15, 20, Color.BLACK);

        Raylib.DrawRectangleRec(stopMusicButton, Color.LIGHTGRAY);
        Raylib.DrawText("Stop Music", (int)stopMusicButton.x + 25, (int)stopMusicButton.y + 15, 20, Color.BLACK);
    }


    public string HandleInput()
    {
        Vector2 mousePosition = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            if (Raylib.CheckCollisionPointRec(mousePosition, startButton))
            {
                return "Start";
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, quitButton))
            {
                return "Quit";
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, stopMusicButton))
            {
                musicPlayer.StopMusic();
                return "MusicStopped";
            }
        }

        return null;
    }
}