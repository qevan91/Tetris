using Raylib_cs;
using System.Numerics;

public class Option
{
    private Rectangle leftButton = new Rectangle(300, 200, 400, 50);
    private Rectangle rightButton = new Rectangle(300, 300, 400, 50);
    private Rectangle downButton = new Rectangle(300, 400, 400, 50);
    private Rectangle rotateButton = new Rectangle(300, 500, 400, 50);
    private Rectangle holdButton = new Rectangle(300, 600, 400, 50);
    private Rectangle pauseButton = new Rectangle(300, 700, 400, 50);

    private Rectangle leftButton2 = new Rectangle(750, 200, 400, 50);
    private Rectangle rightButton2 = new Rectangle(750, 300, 400, 50);
    private Rectangle downButton2 = new Rectangle(750, 400, 400, 50);
    private Rectangle rotateButton2 = new Rectangle(750, 500, 400, 50);
    private Rectangle holdButton2 = new Rectangle(750, 600, 400, 50);

    private Rectangle backButton = new Rectangle(300, 800, 300, 50);

    private Color normalButtonColor = Color.DARKBLUE;
    private Color hoverButtonColor = Color.MAROON;
    private Color borderColor = Color.WHITE;
    private int borderThickness = 3;

    private KeyBinding keyBinding;

    public Option()
    {
        keyBinding = KeyBinding.LoadBindings();
    }

    public void Draw()
    {
        Raylib.ClearBackground(Color.RAYWHITE);

        Raylib.DrawText("Options", 320, 100, 40, Color.BLACK);

        DrawButton(leftButton, $"Move Left Player 1: {keyBinding.MoveLeft}");
        DrawButton(rightButton, $"Move Right Player 1: {keyBinding.MoveRight}");
        DrawButton(downButton, $"Move Down Player 1: {keyBinding.MoveDown}");
        DrawButton(rotateButton, $"Rotate Player 1: {keyBinding.Rotate}");
        DrawButton(holdButton, $"Hold Player 1: {keyBinding.Hold}");
        DrawButton(pauseButton, $"Pause: {keyBinding.Pause}");

        DrawButton(leftButton2, $"Move Left Player 2: {keyBinding.MoveLeft2}");
        DrawButton(rightButton2, $"Move Right Player 2: {keyBinding.MoveRight2}");
        DrawButton(downButton2, $"Move Down Player 2: {keyBinding.MoveDown2}");
        DrawButton(rotateButton2, $"Rotate Player 2: {keyBinding.Rotate2}");
        DrawButton(holdButton2, $"Hold Player 2: {keyBinding.Hold2}");

        DrawButton(backButton, "Retour");
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

    public bool HandleInput()
    {
        Vector2 mousePosition = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            if (Raylib.CheckCollisionPointRec(mousePosition, leftButton))
            {
                keyBinding.MoveLeft = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, rightButton))
            {
                keyBinding.MoveRight = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, downButton))
            {
                keyBinding.MoveDown = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, rotateButton))
            {
                keyBinding.Rotate = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, holdButton))
            {
                keyBinding.Hold = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, pauseButton))
            {
                keyBinding.Pause = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, leftButton2))
            {
                keyBinding.MoveLeft2 = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, rightButton2))
            {
                keyBinding.MoveRight2 = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, downButton2))
            {
                keyBinding.MoveDown2 = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, rotateButton2))
            {
                keyBinding.Rotate2 = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, holdButton2))
            {
                keyBinding.Hold2 = KeyPressed();
            }
            else if (Raylib.CheckCollisionPointRec(mousePosition, backButton))
            {
                return false;
            }

            keyBinding.SaveBindings();
        }

        return true;
    }

    private KeyboardKey KeyPressed()
    {
        while (!Raylib.WindowShouldClose())
        {
            for (int key = (int)KeyboardKey.KEY_SPACE; key <= (int)KeyboardKey.KEY_KP_EQUAL; key++)
            {
                if (Raylib.IsKeyPressed((KeyboardKey)key))
                {
                    return (KeyboardKey)key;
                }
            }
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DARKGRAY);
            Raylib.DrawText("Press a key", 320, 100, 20, Color.WHITE);
            Raylib.EndDrawing();
        }
        return KeyboardKey.KEY_SPACE;
    }
}