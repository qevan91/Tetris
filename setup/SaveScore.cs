using System.IO;
using System.Text.Json;

public class SaveScore
{
    private static string filePath = "SaveScore.json";
    public int BestScore { get; private set; }

    public SaveScore()
    {
        BestScore = LoadBestScore();
    }

    private int LoadBestScore()
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            SaveData data = JsonSerializer.Deserialize<SaveData>(jsonString);
            return data.BestScore;
        }
        return 0;
    }

    public void SaveBestScore(int newScore)
    {
        if (newScore > BestScore)
        {
            BestScore = newScore;
            SaveData data = new SaveData { BestScore = BestScore };
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonString);
        }
    }

    private class SaveData
    {
        public int BestScore { get; set; }
    }
}