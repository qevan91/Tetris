using Raylib_cs;
using System.Numerics;
using System;

public class MainMenu
{
    private Rectangle startButton = new Rectangle(300, 200, 200, 50);
    private Rectangle gameModeButton = new Rectangle(300, 270, 200, 50); 
    private Rectangle OvOButton = new Rectangle(300, 340, 200, 50);
    private Rectangle optionButton = new Rectangle(300, 410, 200, 50);
    private Rectangle quitButton = new Rectangle(300, 480, 200, 50);

    private Color normalButtonColor = Color.DARKBLUE;
    private Color hoverButtonColor = Color.MAROON;
    private Color borderColor = Color.WHITE;
    private int borderThickness = 3;

    public MainMenu() { }

    public void Draw()
    {
        Raylib.DrawText("Menu Principal", 320, 100, 30, Color.BLACK);

        DrawButton(startButton, "Start Game");
        DrawButton(gameModeButton, "Game Mode");
        DrawButton(OvOButton, "1v1");
        DrawButton(optionButton, "Option");
        DrawButton(quitButton, "Quit");
    }

    private void DrawButton(Rectangle button, string text)
    {
        bool isHovered = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), button);
        Color currentButtonColor = isHovered ? hoverButtonColor : normalButtonColor;

        Raylib.DrawRectangleLinesEx(button, borderThickness, borderColor);
        Raylib.DrawRectangleRec(button, currentButtonColor);

        int textWidth = Raylib.MeasureText(text, 20);
        int textX = (int)(button.x + (button.width / 2) - (textWidth / 2));
        int textY = (int)(button.y + (button.height / 2) - 10);
        Raylib.DrawText(text, textX, textY, 20, Color.WHITE);
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
            else if (Raylib.CheckCollisionPointRec(mousePosition, gameModeButton))
            {
                Console.WriteLine("GameMode démarré");
                GameMod.StartGame();
                return "GameMode";
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, OvOButton))
            {
                return "1v1";
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, optionButton))
            {
                return "Option";
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, quitButton))
            {
                return "Quit";
            }
        }

        return null;
    }
}
