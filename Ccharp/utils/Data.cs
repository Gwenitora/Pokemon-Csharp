using Newtonsoft.Json;

public class Data
{
    private List<Attack> attacks = new List<Attack>();
    private List<Chakimon> chakidex = new List<Chakimon>();
    private List<TypeTable> typeTable = new List<TypeTable>();
    private List<Item> items = new List<Item>();

    string attackPath = "attack_data.json";
    string chakidexPath = "chakidex_data.json";
    string typeTablePath = "typetable_data.json";
    string itemsPath = "fight_object_data.json";

    public Data()
    {
        JsonFileManager jsonFileManager = new JsonFileManager();
        
        string attacksText = jsonFileManager.LoadFile(attackPath);
        attacks = JsonConvert.DeserializeObject<List<Attack>>(attacksText);

        string chakidexText = jsonFileManager.LoadFile(chakidexPath);
        chakidex = JsonConvert.DeserializeObject<List<Chakimon>>(chakidexText);
        
        string typeTableText = jsonFileManager.LoadFile(typeTablePath);
        typeTable = JsonConvert.DeserializeObject<List<TypeTable>>(typeTableText);

        string itemsText = jsonFileManager.LoadFile(itemsPath);
        items = JsonConvert.DeserializeObject<List<Item>>(itemsText);
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
        return items;
    }
}