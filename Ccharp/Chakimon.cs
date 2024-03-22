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


public class Chakimon
{
    public string name { get; set; } = string.Empty;
    public type _type { get; set; }
    public Dictionary<string, int> attacks { get; protected set; } = new Dictionary<string, int>();
}
