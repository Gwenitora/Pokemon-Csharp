using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Data;

public enum ItemCategories
{
    CONSUMABLES = 0,
    CATCHER
}


public class Item : GameObject
{
    [JsonProperty("category")]
    public ItemCategories Category { get; set; }
 
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("stat_modifier")]
    public Dictionary<string, int> StatModifier { get; set; } = new Dictionary<string, int>();


    public Item() : base(0, 0, 0, 0) { }

    public virtual void AddToInventory(GameObject gameObject)
    {
        gameObject.gameObjects.Add(this, "items");
    }

    public virtual void Use(GameObject gameObject)
    {
        gameObject.gameObjects.Remove(this);
    }
}

public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Item);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        int category = jsonObject["category"].Value<int>();
        string name = jsonObject["name"].Value<string>();
        var statModifier = jsonObject["stat_modifier"].ToObject<Dictionary<string, int>>();

        Item item;

        if (category == 0)
        {
            item = new StatBufferItem(); // Crée un StatBufferItem si la catégorie est 0
        }
        else if (category == 1)
        {
            item = new CatcherItem(); // Crée un CatcherItem si la catégorie est 1
        }
        else
        {
            throw new JsonSerializationException("Unknown category");
        }

        // Initialise les propriétés communes
        item.Category = (ItemCategories)category;
        item.Name = name;
        item.StatModifier = statModifier;

        return item;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}