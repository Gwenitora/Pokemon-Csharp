public class Item : GameObject
{
    public string name { get; set; }
    public Dictionary<type, int> stat_modifier { get; set; } = new Dictionary<type, int>();

    public Item(string _name, float posX, float posY, int idShape, int idMap) : base(posX, posY, idShape, idMap)
    {
        name = _name;
    }
    public virtual void AddToInventory()
    {
        GameObjectManager.gameObjects.Add(this, "items");
    }
    public virtual void Use()
    {
        GameObjectManager.gameObjects.Remove(this);   
    }
}