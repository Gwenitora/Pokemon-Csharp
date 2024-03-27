using System.Xml.Linq;

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


internal class Chakimon : GameObject
{
    public string Name { get; set; }
    public int Type { get; set; }
    public Dictionary<string, int> Attacks { get; set; }
    public Stats Stats { get; set; } // Assure que cette propriété est bien définie
    public int Level { get; set; }

    // Constructeur révisé pour illustration
    
        public Chakimon(float posX, float posY, int idShape, int idMap,string name, float maxPv, int level, float attack, float defence, float speed, float critValue)
        : base(posX, posY, idShape, idMap)
    {
        Name = name;
        Stats = new Stats
        {
            Pvmax = maxPv,
            Attaque = attack,
            Defense = defence,
            Vitesse = speed,
            Critvalue = critValue
        };
        Level = level;
    }
    public void PrintInfoChakimon()
    {
        Console.WriteLine($"Nom: {Name}");
        Console.WriteLine($"Position: ({PosX}, {PosY})");
        Console.WriteLine($"IdMap: {IdMap}");
        Console.WriteLine($"pv: {Stats.Pvmax}");
        Console.WriteLine($"level: {Level}");
        Console.WriteLine($"attaque: {Stats.Attaque}");


    }

}



