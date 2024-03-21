using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

class Example
{
    public static void Main()
    {
        /*ascii asc = new ascii();
        asc.test();

        Colored.resetColor();*/

        JsonFileManager m_jsonFileManager = new JsonFileManager();
        string filePathChakimon = "chakidex_data.json";


        // --------- debug
        // Load le fichier et recréer la liste de chats
        string text = m_jsonFileManager.LaodFile(filePathChakimon); 
        if (text.Length != 0)
        {
            List<Chakimon> chakidex = JsonConvert.DeserializeObject<List<Chakimon>>(text);

            foreach (var chakimon in chakidex)
            {
                Console.WriteLine($"Nom : {chakimon.name},                  Type : {chakimon.type}");
            }
        }


        Console.WriteLine("\n\n\n\n\n");

        string filePathTypes = "typetable_data.json";

        string text2 = m_jsonFileManager.LaodFile(filePathTypes);

        List<TypeTable> typeTables = JsonConvert.DeserializeObject<List<TypeTable>>(text2);
        foreach(var Jhonweakness in typeTables)
        {
            Console.WriteLine($"{Jhonweakness.type}");
            Console.WriteLine($"Calin -> {Jhonweakness.weakness[Type.Calin]}");
            Console.WriteLine($"Joueur -> {Jhonweakness.weakness[Type.Joueur]}");
            Console.WriteLine($"Espiegle -> {Jhonweakness.weakness[Type.Espiegle]}");
            Console.WriteLine($"Vif -> {Jhonweakness.weakness[Type.Vif]}");
            Console.WriteLine($"Dormeur -> {Jhonweakness.weakness[Type.Dormeur]}");
            Console.WriteLine($"Glouton -> {Jhonweakness.weakness[Type.Glouton]}");
            Console.WriteLine($"Solide -> {Jhonweakness.weakness[Type.Solide]}");
            Console.WriteLine();
        }

        // ---------
    }
}