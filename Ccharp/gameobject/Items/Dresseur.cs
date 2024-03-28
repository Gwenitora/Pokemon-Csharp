using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Dresseur : GameObject
{
    public string Name { get; private set; }
    public List<Chakimon> ChakimonEquipe { get; set; }

    public Dresseur(float posX, float posY, int idMap, string name)
        : base(posX, posY, 1, idMap)
    {
        Name = name;
        ChakimonEquipe = new List<Chakimon>();
    }
}
