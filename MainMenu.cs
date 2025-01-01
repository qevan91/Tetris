using Raylib_cs;
using System.Numerics;

public class MainMenu
{
    // Boutons du menu
    private Rectangle startButton = new Rectangle(300, 200, 200, 50);
    private Rectangle quitButton = new Rectangle(300, 300, 200, 50);

    // Méthode pour afficher le menu principal
    public void Draw()
    {
        Raylib.ClearBackground(Color.DARKGRAY);
        Raylib.DrawText("Menu Principal", 320, 100, 20, Color.WHITE);

        // Dessiner les boutons
        Raylib.DrawRectangleRec(startButton, Color.LIGHTGRAY);
        Raylib.DrawText("Start Game", (int)startButton.x + 50, (int)startButton.y + 15, 20, Color.BLACK);

        Raylib.DrawRectangleRec(quitButton, Color.LIGHTGRAY);
        Raylib.DrawText("Quit Game", (int)quitButton.x + 50, (int)quitButton.y + 15, 20, Color.BLACK);
    }

    // Méthode pour gérer les clics de souris
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
        }

        return null;
    }
}
