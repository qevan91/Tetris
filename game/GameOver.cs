using Raylib_cs;
using System.Numerics;

public class GameOver
    {
    private Rectangle retryButton = new Rectangle(300, 300, 400, 60);
    private Rectangle quitButton = new Rectangle(300, 400, 400, 60);
    private Color normalButtonColor = Color.DARKBLUE;
    private Color hoverButtonColor = Color.MAROON;
    private Color borderColor = Color.WHITE;
    private int borderThickness = 3;

    public GameOver() { }

    public void Draw()
    {
        Raylib.ClearBackground(Color.RAYWHITE);
        Raylib.DrawText("GAME OVER", 300, 100, 80, Color.BLACK);

        DrawButton(retryButton, "RETRY");
        DrawButton(quitButton, "Back to home");
    }

        private void DrawButton(Rectangle button, string text)
    {
        bool isHovered = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), button);
        Color currentButtonColor = isHovered ? hoverButtonColor : normalButtonColor;

        Raylib.DrawRectangleLinesEx(button, borderThickness, borderColor);
        Raylib.DrawRectangleRec(button, currentButtonColor);

        int textWidth = Raylib.MeasureText(text, 30);
        int textX = (int)(button.x + (button.width /2 ) - (textWidth / 2));
        int textY = (int)(button.y + (button.height / 2) - 15);
        Raylib.DrawText(text, textX, textY, 30, Color.WHITE);
    }
        public string HandleInput()
    {
        Vector2 mousePosition = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            if (Raylib.CheckCollisionPointRec(mousePosition, retryButton))
            {
                return "Retry";
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, quitButton))
            {
                return "Quit";
            }
        }

        return null;
    }
}