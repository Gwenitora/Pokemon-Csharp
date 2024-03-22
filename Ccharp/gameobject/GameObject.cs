using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

public abstract class GameObject
{
    public float PosX { get; protected set; }
    public float PosY { get; protected set; }
    public int IdShape { get; protected set; }
    public int IdMap { get; protected set; }

    public GameObject(float posX, float posY, int idShape, int idMap)
    {
        PosX = posX;
        PosY = posY;
        IdShape = idShape;
        IdMap = idMap;
    }

    public void SetPosition(float x, float y)
    {
        PosX = x;
        PosY = y;
    }

    public void ResetIdMap(int newIdMap)
    {
        IdMap = newIdMap;
    }

    public void ResetIdShape(int newIdShape)
    {
        IdShape = newIdShape;
    }

}








//menuobject: il faut savoir a quoi il ressemble , savoir ou ils seront rangé donc quel liste ils integres , position sur la map , 