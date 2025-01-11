using System;
using System.IO;
using System.Text.Json;
using Raylib_cs;

public class KeyBinding
{
    public KeyboardKey MoveLeft { get; set; }
    public KeyboardKey MoveRight { get; set; }
    public KeyboardKey MoveDown { get; set; }
    public KeyboardKey Rotate { get; set; }
    public KeyboardKey Hold { get; set; }
    public KeyboardKey Pause { get; set; }

    public KeyboardKey MoveLeft2 { get; set; }
    public KeyboardKey MoveRight2 { get; set; }
    public KeyboardKey MoveDown2 { get; set; }
    public KeyboardKey Rotate2 { get; set; }
    public KeyboardKey Hold2 { get; set; }

    private static readonly string ConfigFilePath = "keybindings.json";

    public KeyBinding()
    {
        MoveLeft = KeyboardKey.KEY_LEFT;
        MoveRight = KeyboardKey.KEY_RIGHT;
        MoveDown = KeyboardKey.KEY_DOWN;
        Rotate = KeyboardKey.KEY_UP;
        Hold = KeyboardKey.KEY_Q;
        Pause = KeyboardKey.KEY_ENTER;

        MoveLeft2 = KeyboardKey.KEY_A;
        MoveRight2 = KeyboardKey.KEY_D;
        MoveDown2 = KeyboardKey.KEY_S;
        Rotate2 = KeyboardKey.KEY_W;
        Hold2 = KeyboardKey.KEY_E;
    }

    public static KeyBinding LoadBindings()
    {
        if (File.Exists(ConfigFilePath))
        {
            string json = File.ReadAllText(ConfigFilePath);
            return JsonSerializer.Deserialize<KeyBinding>(json);
        }
        return new KeyBinding();
    }

    public void SaveBindings()
    {
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ConfigFilePath, json);
    }
}
