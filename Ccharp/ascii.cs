using System.Drawing;

public class Ascii
{
    string chars = " ";

    public Ascii()
    {
        var startCount = chars.Count();
        for (int i = 1; i <= startCount; i++)
        {
            chars += chars.Substring(startCount - i, 1);
        }
    }

    private string GetChar(int val)
    {
        val = Math.Min(val, 255);
        val = Math.Max(val, 0);
        float len = (float)256 / (float)(chars.Count());
        int choose = (int)(val / len);
        return chars.Substring(Math.Abs(choose), 1);
    }

    private string GenChar(float r, float g, float b)
    {
        float moy = (r + g + b) / (float)3;
        float dist = 1 << 15;
        int saved = 0;
        var colors = Colored.GetColors();
        for (int i = 0; i < colors.Count(); i++)
        {
            var difR = r - colors[i][0];
            var difG = g - colors[i][1];
            var difB = b - colors[i][2];
            float _dist = (float)Math.Sqrt(difR * difR + difG * difG + difB * difB);
            if (_dist > dist) continue;
            dist = _dist;
            saved = i;
        }
        return Colored.GetStrictColor((int)allColor._PERSO_COLOR + (int)allColor.TEXT_BLACK, 0, 0, 0, (int)r, (int)g, (int)b) + GetChar((int)moy);
    }

    public List<int> ResizeImg(int height, int width, int h, int w)
    {
        width *= 2;
        var ratio = ((float)height) / ((float)width);

        var r = ((float)h) / ((float)w);

        int endH;
        int endW;

        if (ratio > r)
        {
            endW = (int)((float)width / (float)height * (float)h);
            endH = h;
        } else
        {
            endH = (int)((float)height / (float)width * (float)w);
            endW = w;
        }
        return new List<int>(4) { endH, endW, (h - endH) / 2, (w - endW) / 2};
    }

    private List<int> GetColorZone(Bitmap img, int x, int y, int h)
    {
        int w = h / 2;

        float r = 0, g = 0, b = 0, a = 0, l = 0;

        List<int> list = new List<int>(6) {x, y, x+w, y+h, 0, 0};

        for (int i = 0; i < Math.Min(h, 10); i++)
        {
            for (int j = 0; j < Math.Min(w, 20); j++)
            {
                if (x + j >= img.Height || y + i >= img.Width) continue;
                var col = img.GetPixel(y + i, x + j);
                r += col.R;
                g += col.G;
                b += col.B;
                a += col.A;
                l++;

                if (x + j > list[4])
                {
                    list[4] = x + j;
                }
                if (y + i > list[5])
                {
                    list[5] = y + i;
                }
            }
        }

        return new List<int>(3) {(int)(r / l), (int)(g / l), (int)(b / l), (int)(a / l) };
    }

    public bool CheckDir(string imgPath, int h, int w)
    {
        var Path = "../../../img/ascii/";

        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path + imgPath + "/");
        }
        if (!Directory.Exists(Path + imgPath + "/"))
        {
            Directory.CreateDirectory(Path + imgPath + "/");
        }
        if (!File.Exists(Path + imgPath + "/" + h + "x" + w + ".txt"))
        {
            return false;
        }
        if (File.ReadAllText(Path + imgPath + "/" + h + "x" + w + ".txt") == "")
        {
            return false;
        }
        return true;
    }

    public string LoadImg(string imgPath, int h, int w)
    {
        h = Math.Min(h, 375);
        h = Math.Max(h, 1);
        w = Math.Min(w, 750);
        w = Math.Max(w, 1);
        var Path = "../../../img/";
        string res = "";

        if (CheckDir(imgPath, h, w))
        {
            return File.ReadAllText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt");
        }

        var img = new Bitmap(Path + imgPath);

        var size = ResizeImg(img.Height, img.Width, h, w);
        var height = size[0];
        var width = size[1];
        var offsetH = size[2];
        var offsetW = size[3];

        Console.CursorVisible = false;

        if (h > 0 && w > 0)
        {
            for (int i = 0; i < h + 1; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (
                        offsetH > i || i > offsetH + height ||
                        offsetW > j || j > offsetW + width
                    )
                    {
                        res += Colored.GetStrictColor(allColor.TEXT_BLACK) + "Y";
                        continue;
                    }
                    var col = GetColorZone(
                        img,
                        (int)(((float)i - (float)offsetH) / (float)height * (float)img.Height),
                        (int)(((float)j - (float)offsetW) / (float)width * (float)img.Width),
                        (int)((float)img.Height / (float)h)
                    );

                    string t;
                    if (col[3] < 100)
                    {
                        t = Colored.GetStrictColor(allColor.TEXT_BLACK) + "Y";
                    } else
                    {
                        t = GenChar(col[0], col[1], col[2]);
                    }

                    res += t;
                }
                res += "\n";
            }
        }

        if (!File.Exists(Path + imgPath + "/" + h + "x" + w + ".txt"))
        {
            File.CreateText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt").Close();
        }
        File.WriteAllText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt", res);

        img.Dispose();

        return res;
    }
    public string LoadImg(string imgPath)
    {
        return LoadImg(imgPath, Console.WindowHeight, Console.WindowWidth);
    }
    public List<int> GetSize(string img)
    {
        string l = string.Join(" ", img.Split("Y"));
        return new List<int>(2) { l.Split("\n").Count() -2, l.Split("\n")[0].Split(" ").Count() -1 };
    }
    public Dictionary<int, Dictionary<int, string>> GetChars(string img)
    {
        var size = GetSize(img);
        var l = img.Split("\n");

        var res = new Dictionary<int, Dictionary<int, string>>();

        for (int i = 0; i < size[0]; i++)
        {
            var line = l[i];
            int c = 0;
            for (int j = 0; j < size[1]; j++)
            {
                int cl = 1;
                while (!(line.Substring(c + cl, 1) == " ") && !(line.Substring(c + cl, 1) == "Y")) cl++;
                if (!res.ContainsKey(i))
                {
                    res[i] = new Dictionary<int, string>();
                }
                res[i][j] = line.Substring(c, cl +1);
                c += cl + 1;
            }
        }

        return res;
    }
    public string Recompile(Dictionary<int, Dictionary<int, string>> dictImg)
    {
        string res = "";
        var firstStep = new Dictionary<int, string>();
        for (int i = 0; i < dictImg.Count(); i++)
        {
            firstStep[i] = "";
            foreach (var j in dictImg[i])
            {
                firstStep[i] += j.Value;
            }
        }
        foreach (var j in firstStep)
        {
            res += j.Value + "\n";
        }
        return res;
    }
    public string Adding(string img1, string imgPath2, float pctX2, float pctY2, float pctH2, float pctW2)
    {
        var imgDict1 = GetChars(img1);
        var size = GetSize(img1);
        int pctX = (int)((float)size[1] * pctX2 / 100f);
        int pctY = (int)((float)size[0] * pctY2 / 100f);
        size[0] = (int)((float)size[0] * pctH2 / 100f);
        size[1] = (int)((float)size[1] * pctW2 / 100f);

        string img2 = LoadImg(imgPath2, size[0], size[1]);
        var imgDict2 = GetChars(img2);
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                var c = imgDict2[i][j];
                if (c.Contains("Y")) continue;
                imgDict1[i + pctY][j + pctX] = c;
            }
        }
        return Recompile(imgDict1);
    }
    public string Adding(string img1, string imgPath2, float pctX2, float pctY2)
    {
        return Adding(img1, imgPath2, pctX2, pctY2, Console.WindowHeight, Console.WindowWidth);
    }
    public string Adding(string img1, string imgPath2)
    {
        return Adding(img1, imgPath2, 0, 0, Console.WindowHeight, Console.WindowWidth);
    }
}