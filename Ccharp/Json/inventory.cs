public class Inventory
{
    public List<GameObject> inventory = new List<GameObject>();

    public void AddInInventory(GameObject obj)
    {
        inventory.Add(obj);
    }
    
    public void RemoveOfInventory(GameObject obj) 
    {
        inventory.Remove(obj);
    }
}