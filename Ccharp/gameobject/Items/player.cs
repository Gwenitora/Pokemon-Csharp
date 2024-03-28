using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

internal class Player : GameObject
{
    public string Name { get; private set; }
    public List<Chakimon> ChakimonEquipe { get; set; }

    public Player(float posX, float posY, int idMap, string name)
        : base(posX, posY, 1, idMap)

    {
        ChakimonEquipe = new List<Chakimon>();
        Name = name;
    }

    Player chateigne = new Player(1,1,1,"Chateigne");
    public void PrintInfo()
    {
        Console.WriteLine($"Nom: {Name}");
        Console.WriteLine($"Position: ({PosX}, {PosY})");
        Console.WriteLine($"IdMap: {IdMap}");
        
    }
}


