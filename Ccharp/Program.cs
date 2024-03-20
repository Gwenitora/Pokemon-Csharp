using System.Text.Json;
using System;

class Example
{
    public static void Main()
    {
        /*ascii asc = new ascii();
        asc.test();

        Colored.resetColor();*/

        JsonFileManager m_jsonFileManager = new JsonFileManager();
        string filePath = "chakidex_data.json";


        // --------- debug
        // Load le fichier et recréer la liste de chats
        string text = m_jsonFileManager.LaodFile(filePath); 
        if (text.Length != 0)
        {
            List<Chakimon> chakidex = JsonSerializer.Deserialize<List<Chakimon>>(text);

            foreach (var chakimon in chakidex)
            {
                Console.WriteLine($"Nom : {chakimon.name},                  Type : {chakimon.type}");
            }
        }
        // ---------
    }
}