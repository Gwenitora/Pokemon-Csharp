using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class JsonFileManager
{
    public string LaodFile(string path)
    {
        return File.ReadAllText($"../../../Json/{path}");
    }

    public void SaveToJsonFile<T>(List<T> items, string filePath)
    {
        string jsonString = JsonConvert.SerializeObject(items);
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine("Données enregistrées avec succès dans le fichier JSON : " + filePath);
    }

}
