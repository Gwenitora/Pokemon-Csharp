public enum Type
{
    Calin,
    Joueur,
    Espiegle, 
    Vif, 
    Dormeur,
    Glouton,
    Solide
}

public enum Weakness
{
    Resistant,
    Neutral,
    Weak
}

public class TypeTable
{
    public Type type { get; set; }
    public Dictionary<Type, Weakness> weakness { get; protected set; } = new Dictionary<Type, Weakness>();
}


public class Chakimon
{
    public string name { get; set; } = string.Empty;
    public Type type { get; set; }
    //public string description { get; set; } = string.Empty;
}
