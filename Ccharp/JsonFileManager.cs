using System.Text.Json;

public class JsonFileManager
{
    public string LaodFile(string path)
    {
        string text = File.ReadAllText($"./{path}");
        return text;
    }
    public void SaveToJsonFile<T>(List<T> items, string filePath)
    {
        string jsonString = JsonSerializer.Serialize(items);
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine("Données enregistrées avec succès dans le fichier JSON : " + filePath);
    }

}
