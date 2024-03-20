using System.Drawing;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

class Progam
{
    private async static void GameLoop()
    {
        ascii asc = new ascii();
        var task1 = Task.Run(() => Preload(asc));
        while (true)
        {
            var _h = Console.WindowHeight;
            var _w = Console.WindowWidth;
            if (_h <= 0 || _w <= 0) continue;
            Console.SetCursorPosition(0, 0);
            Console.Write(asc.loadImg("cat-example.png"));
        }
    }

    public static void Main()
    {
        Colored.resetColor();
        GameLoop();
        Colored.resetColor();
    }

    private async static void Preload(ascii asc)
    {
        List<string> imgToLoad = new List<string>()
        {
            "cat-example.png"
        };

        int minPix = 100 * 100;

        foreach (string img in imgToLoad)
        {
            for (int i = 1; i < 375; i++)
            {
                for (int j = 1; j < 750; j++)
                {
                    if (i * j < minPix) continue;

                    asc.loadImg(img, i, j);
                }
            }
        }
    }
}