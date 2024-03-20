using System.Xml.Linq;

class Example
{
    public static void Main()
    {
        Player joueur = new Player(5, 5, 1, "Hero");


        // Appel de la méthode PrintInfo pour afficher les informations du joueur
        joueur.PrintInfo();

        Console.ReadKey(); // Attendre une touche pour voir le résultat

    }
}