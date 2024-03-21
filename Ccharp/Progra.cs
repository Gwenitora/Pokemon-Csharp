class Progam
{
    static List<string> imgToLoad = new List<string>()
    {
        "cat-example.png",
        "star.png"
    };

    private async static void GameLoop()
    {
        Ascii asc = new Ascii();
        var task1 = Task.Run(() => Preload(asc));
        while (true)
        {
            var _h = Console.WindowHeight;
            var _w = Console.WindowWidth;
            if (_h <= 0 || _w <= 0) continue;
            Console.SetCursorPosition(0, 0);
            var bg = asc.LoadImg(imgToLoad[0]);
            asc.GetChars(bg);
            var res = asc.Adding(bg, imgToLoad[1], 25f, 25f, 50f, 50f);
            Console.Write(res);
        }
    }

    public static void Main()
    {
        Colored.ResetColor();
        GameLoop();
        Colored.ResetColor();
    }

    private async static void Preload(Ascii asc)
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
            if (asc.CheckDir(imgToLoad[r1], r2, r3) || r1 * r2 < minPix)
            {
                --i;
            } else
            {
                asc.LoadImg(imgToLoad[r1], r2, r3);
            }
        }
    }
}