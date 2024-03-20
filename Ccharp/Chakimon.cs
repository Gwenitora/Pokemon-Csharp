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

public class Chakimon
{
    public string name { get; set; } = string.Empty;
    public Type type { get; set; }

    //public string description { get; set; } = string.Empty;
}