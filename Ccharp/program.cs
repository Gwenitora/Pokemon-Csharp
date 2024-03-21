class Progam
{
    static List<string> imgToLoad = new List<string>()
    {
        "cat-example.png",
        "star.png"
    };

    static Random rnd = new Random();
    public static Random Rnd { get => rnd; }

    private async static void GameLoop()
    {
        Ascii m_ascii = new Ascii();
        var task1 = Task.Run(() => Preload(m_ascii));
        int posX = 35;
        int posY = 35;
        while (true)
        {
            var _h = Console.WindowHeight;
            var _w = Console.WindowWidth;
            if (_h <= 0 || _w <= 0) continue;
            Console.SetCursorPosition(0, 0);
            var bg = m_ascii.LoadImg(imgToLoad[0]);
            m_ascii.GetChars(bg);
            var res = m_ascii.Adding(bg, imgToLoad[1], posX, posY, 30f, 30f);
            Console.Write(res);

            posX += Rnd.Next(-10, 11);
            posY += Rnd.Next(-10, 11);
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
            int r1 = Rnd.Next(imgToLoad.Count());
            int r2 = Rnd.Next(1, 375);
            int r3 = Rnd.Next(1, 750);
            if (asc.CheckDir(imgToLoad[r1], r2, r3) || r2 * r3 < minPix)
            {
                --i;
            } else
            {
                asc.LoadImg(imgToLoad[r1], r2, r3);
            }
        }
    }
}