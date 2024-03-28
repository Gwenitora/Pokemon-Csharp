using System.Drawing;
using System.Text.Json.Serialization;

public enum type
{
    CALIN,
    JOUEUR,
    ESPIEGLE, 
    VIF, 
    DORMEUR,
    GLOUTON,
    SOLIDE
}

public enum weakness
{
    RESISTANT,
    NEUTRAL,
    WEAK
}

public class TypeTable
{
    public type _type { get; set; }
    public Dictionary<type, weakness> weakness { get; protected set; } = new Dictionary<type, weakness>();
}

public class Attack
{
    public string name { set; get; } = string.Empty;
    public type _type { set; get; }
    public float damage { set; get; }
    public int pp { set; get; }
    public float precision { set; get; }
    public int critical { set; get; }
    public bool isHeal { set; get; }
}

public class Stats
{
    public float Attaque { get; set; }
    public float Defense { get; set; }
    public float Vitesse { get; set; }
    public float Critvalue { get; set; }
    public float Pvmax { get; set; }
}


public class Chakimon : GameObject
{
    public string Name { get; set; }
    public int Type { get; set; }
    public Dictionary<string, int> Attacks { get; set; }
    public Stats Stats { get; set; } // Assure que cette propriété est bien définie
    public int Level { get; set; }

    [JsonConstructor]
    public Chakimon() : base(0, 0, 0, 0) { }


    public Chakimon(float posX, float posY, int idShape, int idMap)
    : base(posX, posY, idShape, idMap) { }
    public void PrintInfoChakimon()
    {
        Console.WriteLine($"Nom: {Name}");
        Console.WriteLine($"Position: ({PosX}, {PosY})");
        Console.WriteLine($"IdMap: {IdMap}");
        Console.WriteLine($"pv: {Stats.Pvmax}");
        Console.WriteLine($"level: {Level}");
        Console.WriteLine($"attaque: {Stats.Attaque}");
    }

    public Chakimon(Chakimon chakimon,int level)
        : base(0,0,0,0)
    {
        Stats.Attaque = Stats.Attaque + level;
        Stats.Defense = Stats.Defense + level;
        Stats.Vitesse = Stats.Vitesse + level;
        Stats.Pvmax = Stats.Pvmax + level;
        foreach (var attack in chakimon.Attacks)
        {
            if (attack.Value == level)
            {
                Attacks.Add(attack.Key, attack.Value);
            }
        }
    }
}

