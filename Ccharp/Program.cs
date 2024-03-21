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
        string text = m_jsonFileManager.LoadFile(filePathChakimon); 
        if (text.Length != 0)
        {
            List<Chakimon> chakidex = JsonConvert.DeserializeObject<List<Chakimon>>(text);

            foreach (var chakimon in chakidex)
            {
                Console.WriteLine($"Nom : {chakimon.name},       type : {chakimon._type}");
            }
        }


        Console.WriteLine("\n\n\n\n\n");

        string filePathtypes = "typetable_data.json";

        string text2 = m_jsonFileManager.LoadFile(filePathtypes);

        List<TypeTable> typeTables = JsonConvert.DeserializeObject<List<TypeTable>>(text2);
        foreach(var weaknesses in typeTables)
        {
            Console.WriteLine($"{weaknesses._type}");
            Console.WriteLine($"Calin -> {weaknesses.weakness[type.CALIN]}");
            Console.WriteLine($"Joueur -> {weaknesses.weakness[type.JOUEUR]}");
            Console.WriteLine($"Espiegle -> {weaknesses.weakness[type.ESPIEGLE]}");
            Console.WriteLine($"Vif -> {weaknesses.weakness[type.VIF]}");
            Console.WriteLine($"Dormeur -> {weaknesses.weakness[type.DORMEUR]}");
            Console.WriteLine($"Glouton -> {weaknesses.weakness[type.GLOUTON]}");
            Console.WriteLine($"Solide -> {weaknesses.weakness[type.SOLIDE]}");
            Console.WriteLine();
        }
        Console.WriteLine("\n\n\n\n");

        string filePathAttack = "attack_data.json";
        string text3 = m_jsonFileManager.LoadFile(filePathAttack);

        List<Attack> attacks = JsonConvert.DeserializeObject<List<Attack>>(text3);
        foreach(var attack in attacks)
        {
            Console.WriteLine($"{attack.name}");
        }
    }
}