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
                Console.WriteLine($"Nom : {chakimon.name},                  type : {chakimon._type}");
            }
        }


        Console.WriteLine("\n\n\n\n\n");

        string filePathtypes = "typetable_data.json";

        string text2 = m_jsonFileManager.LaodFile(filePathtypes);

        List<TypeTable> typeTables = JsonConvert.DeserializeObject<List<TypeTable>>(text2);
        foreach(var Jhonweakness in typeTables)
        {
            Console.WriteLine($"{Jhonweakness._type}");
            Console.WriteLine($"Calin -> {Jhonweakness.weakness[type.CALIN]}");
            Console.WriteLine($"Joueur -> {Jhonweakness.weakness[type.JOUEUR]}");
            Console.WriteLine($"Espiegle -> {Jhonweakness.weakness[type.ESPIEGLE]}");
            Console.WriteLine($"Vif -> {Jhonweakness.weakness[type.VIF]}");
            Console.WriteLine($"Dormeur -> {Jhonweakness.weakness[type.DORMEUR]}");
            Console.WriteLine($"Glouton -> {Jhonweakness.weakness[type.GLOUTON]}");
            Console.WriteLine($"Solide -> {Jhonweakness.weakness[type.SOLIDE]}");
            Console.WriteLine();
        }

    }
}