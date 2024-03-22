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

    public void SaveToJsonFile(object items, string file_path)
    {
        string json_string = JsonConvert.SerializeObject(items);
        File.WriteAllText(file_path, json_string);
    }

}
