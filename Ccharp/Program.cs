using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

class Progam
{
    static List<string> imgToLoad = new List<string>()
    {
        "cat-example.png",
        "star.png"
    };

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
            var bg = asc.loadImg(imgToLoad[0]);
            asc.getChars(bg);
            var res = asc.adding(bg, imgToLoad[1], 25f, 25f, 50f, 50f);
            //var res = asc.recompile(asc.getChars(bg));
            Console.Write(res);
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
        int minPix = 100 * 100;

        int k = 0;

        foreach (string img in imgToLoad)
        {
            for (int i = 1; i < 375; i++)
            {
                for (int j = 1; j < 750; j++)
                {
                    if (i * j < minPix) continue;
                    k++;
                }
            }
        }

        for (int i = 0; i < k; i++)
        {
            int r1 = (new Random()).Next(imgToLoad.Count());
            int r2 = (new Random()).Next(0, 375);
            int r3 = (new Random()).Next(0, 750);
            if (asc.checkDir(imgToLoad[r1], r2, r3) || r1 * r2 < minPix)
            {
                --i;
            } else
            {
                asc.loadImg(imgToLoad[r1], r2, r3);
            }
        }
    }
}