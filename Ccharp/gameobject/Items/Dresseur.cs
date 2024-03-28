using Newtonsoft.Json;
public class Dresseur : GameObject
{
    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("chakimon_team")]
    public List<Chakimon> ChakimonTeam { get; set; } = new List<Chakimon>();

    public Dresseur(float posX, float posY, int idMap, string name)
        : base(posX, posY, 1, idMap)
    {
        Name = name;
    }
}
