using Newtonsoft.Json;

public class Data
{
    private List<Attack> attacks = new List<Attack>();
    private List<Chakimon> chakidex = new List<Chakimon>();
    private List<TypeTable> typeTable = new List<TypeTable>();
    
    private List<Item> itemList = new List<Item>();

    private ChakimonCatched chakimonCatched = new ChakimonCatched();

    string attackPath = "attack_data.json";
    string chakidexPath = "chakidex_data.json";
    string typeTablePath = "typetable_data.json";
    string itemsPath = "object_data.json";

    string chakimonCatchedPath = "chakimon_catched_data.json";

    JsonFileManager fileManager;

    public Data(JsonFileManager jsonFileManager)
    {        
        fileManager = jsonFileManager;

        string attacksText = jsonFileManager.LoadFile(attackPath);
        attacks = JsonConvert.DeserializeObject<List<Attack>>(attacksText);

        string chakidexText = jsonFileManager.LoadFile(chakidexPath);
        chakidex = JsonConvert.DeserializeObject<List<Chakimon>>(chakidexText);
        
        string typeTableText = jsonFileManager.LoadFile(typeTablePath);
        typeTable = JsonConvert.DeserializeObject<List<TypeTable>>(typeTableText);

        string itemsText = jsonFileManager.LoadFile(itemsPath);
        var settings = new JsonSerializerSettings
        {
            Converters = { new ItemConverter() }
        };
        itemList = JsonConvert.DeserializeObject<List<Item>>(itemsText, settings);

        if (jsonFileManager.FoundFile(chakimonCatchedPath))
        {
            string allChakimonCatchedText = jsonFileManager.LoadFile(chakimonCatchedPath, true);
            if (allChakimonCatchedText.Length > 0)
                chakimonCatched = JsonConvert.DeserializeObject<ChakimonCatched>(allChakimonCatchedText);
        }
    }

    public void Save()
    {
        fileManager.SaveToJsonFile(attacks, attackPath);
        fileManager.SaveToJsonFile(chakidex, chakidexPath);
        fileManager.SaveToJsonFile(typeTable, typeTablePath);
        fileManager.SaveToJsonFile(itemList, itemsPath);
        fileManager.SaveToJsonFile(chakimonCatched, chakimonCatchedPath);
    }

    public List<Attack> GetAttackList()
    {
        return attacks;
    }
    public List<TypeTable> GetTypeTableList()
    {
        return typeTable;
    }
    public List<Chakimon> GetChakimonList()
    {
        return chakidex;
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }

    public ChakimonCatched GetTeamList()
    {
        return chakimonCatched;
    }
}