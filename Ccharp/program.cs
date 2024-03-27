using Newtonsoft.Json;
using System.Drawing;

class Progam
{
    static List<string> imgToLoad = new List<string>()
    {
        "cat-example.png",
        "star.png"
    };

    static Random rnd = new Random();
    public static Random Rnd { get => rnd; }

    private static void GameLoop()
    {
        Ascii m_ascii = new Ascii();
        JsonFileManager m_jsonFileManager = new JsonFileManager();
        Data datas = new Data();

        foreach (Item item in datas.GetItemList())
        {
            Console.WriteLine(item.GetType());
        }

        var task1 = Task.Run(() => Preload(m_ascii));
        int posX = 0;
        int posY = 0;
        while (true)
        {
            posX += Rnd.Next(-10, 11);
            posY += Rnd.Next(-10, 11);

            // TODO: don't touch next paragraphe
            var _h = Console.WindowHeight;
            var _w = Console.WindowWidth;
            if (_h <= 0 || _w <= 0) continue;
            Console.SetCursorPosition(0, 0);

            var bg = m_ascii.LoadImg(imgToLoad[0]);
            var res = m_ascii.Adding(bg, imgToLoad[1], posX, posY, 30f, 30f);

            // TODO: don't touch next paragraphe
            res = m_ascii.Adding(m_ascii.GetEmptyImage(), res, 0, 0, 100f, 100f);
            Console.Write(res);
        }
    }

    public static void Main()
    {
        Colored.ResetColor();

        JsonFileManager jsonFileManager = new JsonFileManager();
        GameLoop();
        Colored.ResetColor();
    }

    private static void Preload(Ascii m_ascii)
    {
        int minPix = 100 * 100;

        int k = 0;

        foreach (string img in imgToLoad)
        {
            for (int i = 1; i < 375; i++)
            {
                for (int j = 1; j < 750; j++)
                {
                    var size = m_ascii.ResizeImg(img, i, j);
                    var _i = size[0];
                    var _j = size[1];
                    if (_i * _j < minPix) continue;
                    if (!m_ascii.CheckDir(img, _i, _j) || _i * _j < minPix)
                    {
                        k++;
                    }
                }
            }
        }

        for (int i = 0; i < k; i++)
        {
            int r1 = Rnd.Next(imgToLoad.Count());
            int r2 = Rnd.Next(1, 375);
            int r3 = Rnd.Next(1, 750);
            if (m_ascii.CheckDir(imgToLoad[r1], r2, r3) || r2 * r3 < minPix)
            {
                --i;
            } else
            {
                var size = m_ascii.ResizeImg(imgToLoad[r1], r2, r3);
                r2 = size[0];
                r3 = size[1];
                m_ascii.LoadImg(imgToLoad[r1], r2, r3);
            }
        }
    }
}