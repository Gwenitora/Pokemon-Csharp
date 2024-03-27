public class Healthbar : GameObject
{
    private int pv;

    public Healthbar(float posX, float posY, int idShape, int idMap) : base(posX, posY, idShape, idMap)
    {
    }

    public void Init(int _pv)
    {
        this.pv = _pv;
    }
    
    public void Update()
    {
        // code
    }

    public void GetDamage(int damage)
    {
        pv -= damage;
    }

    public void GetHeal(int heal)
    {
        pv += heal;
    }

    public int GetHealth() 
    {
        return pv;     
    }
}