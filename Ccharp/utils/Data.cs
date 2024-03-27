using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Data
{
    private List<Attack> attacks = new List<Attack>();
    private List<Chakimon> chakidex = new List<Chakimon>();
    private List<TypeTable> typeTable = new List<TypeTable>();
    
    private List<ItemList> itemList = new List<ItemList>();
    private List<Item> items = new List<Item>();

    private List<Team> team = new List<Team>();
    private List<AllCatchedChakimon> allCatchedChakimons = new List<AllCatchedChakimon>();

    string attackPath = "attack_data.json";
    string chakidexPath = "chakidex_data.json";
    string typeTablePath = "typetable_data.json";
    /*string itemsPath = "object_data.json";*/

    string teamPath = "team_data.json";
    string allChakimonCatchedPath = "all_catched_chakimon_data.json";

    public Data()
    {
        JsonFileManager jsonFileManager = new JsonFileManager();
        
        string attacksText = jsonFileManager.LoadFile(attackPath);
        attacks = JsonConvert.DeserializeObject<List<Attack>>(attacksText);

        string chakidexText = jsonFileManager.LoadFile(chakidexPath);
        chakidex = JsonConvert.DeserializeObject<List<Chakimon>>(chakidexText);
        
        string typeTableText = jsonFileManager.LoadFile(typeTablePath);
        typeTable = JsonConvert.DeserializeObject<List<TypeTable>>(typeTableText);

        /*string itemsText = jsonFileManager.LoadFile(itemsPath);
        itemList = JsonConvert.DeserializeObject<List<ItemList>>(itemsText);

        foreach (ItemList item in itemList)
        {
            foreach (Item _item in item.Items) 
            {
                items.Add(_item);
            }
        }*/

        string teamText = jsonFileManager.LoadFile(teamPath, true);
        team = JsonConvert.DeserializeObject<List<Team>>(teamText);

        string allChakimonText = jsonFileManager.LoadFile(allChakimonCatchedPath, true);
        allCatchedChakimons = JsonConvert.DeserializeObject<List<AllCatchedChakimon>>(allChakimonText);
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

    public List<Team> GetTeamList()
    {
        return team;
    }
    public List<AllCatchedChakimon> GetAllCatchedChakimons()
    {
        return allCatchedChakimons;
    }
}