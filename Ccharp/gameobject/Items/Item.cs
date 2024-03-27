using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Data;

public enum ItemCategories
{
    CONSUMABLES,
    CATCHER
}

public class Item : GameObject
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("stat_modifier")]
    public Dictionary<string, int> StatModifier { get; set; } = new Dictionary<string, int>();

    public Item(string _name, float posX, float posY, int idShape, int idMap) : base(posX, posY, idShape, idMap)
    {
        Name = _name;
    }

    public virtual void AddToInventory(GameObject gameObject)
    {
        gameObject.gameObjects.Add(this, "items");
    }

    public virtual void Use(GameObject gameObject)
    {
        gameObject.gameObjects.Remove(this);
    }
}

public class ItemList
{
    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("items")]
    [JsonConverter(typeof(ItemConverter))]
    public List<Item> Items { get; set; }
}


public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ItemList);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        int category = jsonObject["category"].Value<int>();
        JArray itemsArray = (JArray)jsonObject["items"];
        List<Item> items = new List<Item>();

        foreach (JObject itemObject in itemsArray)
        {
            if (category == 0)
            {
                items.Add(itemObject.ToObject<StatBufferItem>());
            }
            else if (category == 1)
            {
                items.Add(itemObject.ToObject<CatcherItem>());
            }
            else
            {
                throw new JsonSerializationException("Unknown category");
            }
        }

        return new ItemList { Category = category, Items = items };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}