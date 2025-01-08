using System.IO;
using NewtonsoftJson = Newtonsoft.Json;
using System;
using Raylib_cs;

class Save
{
    private const string SaveFileName = "Save.json";

    public static void SaveGame(SetupGrid grid, Tetrimino currentTetrimino, Tetrimino nextTetrimino, int score)
    {
        var saveData = new
        {
            Grid = ConvertTo1D(grid.Grid),
            CurrentTetrimino = new TetriminoData
            {
                Shape = currentTetrimino.Shape,
                Color = new ColorData
                {
                    r = currentTetrimino.Color.r,
                    g = currentTetrimino.Color.g,
                    b = currentTetrimino.Color.b,
                    a = currentTetrimino.Color.a
                },
                X = currentTetrimino.X,
                Y = currentTetrimino.Y
            },
            NextTetrimino = new TetriminoData
            {
                Shape = nextTetrimino.Shape,
                Color = new ColorData
                {
                    r = nextTetrimino.Color.r,
                    g = nextTetrimino.Color.g,
                    b = nextTetrimino.Color.b,
                    a = nextTetrimino.Color.a
                },
                X = nextTetrimino.X,
                Y = nextTetrimino.Y
            },
            Score = score
        };

        string json = NewtonsoftJson.JsonConvert.SerializeObject(saveData);
        File.WriteAllText(SaveFileName, json);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(SaveFileName))
        {
            try
            {
                string json = File.ReadAllText(SaveFileName);
                return NewtonsoftJson.JsonConvert.DeserializeObject<SaveData>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du jeu : {ex.Message}");
                return null;
            }
        }
        else
        {
            Console.WriteLine($"Le fichier de sauvegarde '{SaveFileName}' n'existe pas.");
            return null;
        }
    }

    public static int[,] ConvertTo2D(int[] oneDArray, int rows, int columns)
    {
        int[,] twoDArray = new int[rows, columns];
        for (int i = 0; i < oneDArray.Length; i++)
        {
            int row = i / columns;
            int col = i % columns;
            twoDArray[row, col] = oneDArray[i];
        }
        return twoDArray;
    }

    private static int[] ConvertTo1D(int[,] twoDArray)
    {
        int rows = twoDArray.GetLength(0);
        int columns = twoDArray.GetLength(1);
        int[] oneDArray = new int[rows * columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                oneDArray[i * columns + j] = twoDArray[i, j];
            }
        }
        return oneDArray;
    }
}

class SaveData
{
    public int[] Grid { get; set; }
    public TetriminoData CurrentTetrimino { get; set; }
    public TetriminoData NextTetrimino { get; set; }
    public int Score { get; set; }
}

class TetriminoData
{
    public int[,] Shape { get; set; }
    public ColorData Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

class ColorData
{
    public byte r { get; set; }
    public byte g { get; set; }
    public byte b { get; set; }
    public byte a { get; set; }
}