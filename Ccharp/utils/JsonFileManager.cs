using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class JsonFileManager
{
    public string LoadFile(string path, bool data = false)
    {
        if (data)
        {
            return File.ReadAllText($"../../../data/{path}");
        }
        return File.ReadAllText($"../../../Json/{path}");
    }

    public void SaveToJsonFile<T>(List<T> items, string file_path)
    {
        string json_string = JsonConvert.SerializeObject(items);
        File.WriteAllText($"../../../data/{file_path}", $"{json_string}");
        Console.WriteLine($"Données enregistrées avec succès dans le fichier JSON :  + {file_path}");
    }

    public bool FoundFile(string path)
    {
        return File.Exists(path);
    }
}
