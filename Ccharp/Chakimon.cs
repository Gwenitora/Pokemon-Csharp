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
    public type Type { get; set; }
    public Dictionary<string, int> Attacks { get; set; }
    public Stats Stats { get; set; } 
    public int Level { get; set; }
    public float pv { get; set; }

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

    public Chakimon(string name, type type, int level) : base(0,0,0,0)
    {
        Name = name;
        Type = type;
        Attacks = new Dictionary<string, int>()
        {
            { "gratouille", 1 },
            { "sieste", 1 },
            { "chatouille", 1 },
            { "Calinerie", 1  }
        };

        Stats = new Stats()
        {
            Attaque = 20,
            Defense = 20,
            Critvalue = 20,
            Vitesse = 20,
            Pvmax = 20,
        };

        Level = level;
        pv = Stats.Pvmax;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            if (-damage + pv >= Stats.Pvmax)
                pv = Stats.Pvmax;
            else 
                pv -= damage;
        }
        else
        {
            if (pv < damage)
                pv = 0;
            else
                pv -= damage;
        }
    }
}
