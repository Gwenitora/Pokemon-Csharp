using System.Drawing;
using System.IO;

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
    public List<int> ResizeImg(Bitmap img, int h, int w)
    {
        return ResizeImg(img.Height, img.Width, h, w);
    }
    public List<int> ResizeImg(string imgPath, int h, int w)
    {
        var Path = "../../../img/";
        return ResizeImg(new Bitmap(Path + imgPath), h, w);
    }
    public List<int> ResizeImg(int height, int width)
    {
        return ResizeImg(height, width, Console.WindowHeight, Console.WindowWidth);
    }
    public List<int> ResizeImg(Bitmap img)
    {
        return ResizeImg(img.Height, img.Width, Console.WindowHeight, Console.WindowWidth);
    }
    public List<int> ResizeImg(string imgPath)
    {
        var Path = "../../../img/";
        return ResizeImg(new Bitmap(Path + imgPath), Console.WindowHeight, Console.WindowWidth);
    }

    private List<int> GetColorZone(Bitmap img, int x, int y, int h)
    {
        int w = Math.Max(h / 2, 1);

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
        var size = ResizeImg(imgPath, h, w);
        h = size[0];
        w = size[1];
        var Path = "../../../img/";

        if (CheckDir(imgPath, h, w))
        {
            return File.ReadAllText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt");
        }

        var img = new Bitmap(Path + imgPath);

        var res = LoadImg(img, h, w);

        if (!File.Exists(Path + imgPath + "/" + h + "x" + w + ".txt"))
        {
            File.CreateText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt").Close();
        }
        File.WriteAllText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt", res);

        img.Dispose();

        return res;
    }
    public string LoadImg(Bitmap img, int h, int w)
    {
        string res = "";

        var size = ResizeImg(img, h, w);
        h = size[0];
        w = size[1];

        Console.CursorVisible = false;

        if (h > 0 && w > 0)
        {
            for (int i = 0; i < h + 1; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var col = GetColorZone(
                        img,
                        (int)((float)i / (float)h * (float)img.Height),
                        (int)((float)j / (float)w * (float)img.Width),
                        Math.Max((int)((float)img.Height / (float)h), 1)
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

        return res;
    }
    public string LoadImg(string imgPath)
    {
        return LoadImg(imgPath, Console.WindowHeight, Console.WindowWidth);
    }
    public string LoadImg(Bitmap img)
    {
        return LoadImg(img, Console.WindowHeight, Console.WindowWidth);
    }
    public List<int> GetSize(string img)
    {
        string l = string.Join(" ", img.Split("Y"));
        return new List<int>(2) { l.Split("\n").Count() - 2, l.Split("\n")[0].Split(" ").Count() -2 };
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
                res[i][j] = line.Substring(c, cl + 1);
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
        return res + "\n";
    }

    private string Adding(string img1, string img2Path, float pctX2, float pctY2, float pctH2, float pctW2, bool keepSize, bool inPct)
    {
        var imgDict1 = GetChars(img1);
        var size = GetSize(img1);
        int pctX, pctY;
        float offX = 0, offY = 0;

        if (!inPct)
        {
            if (size[0] < size[1])
            {
                offX += (float)(size[1] - size[0] *2) /2f;
                size[1] = size[0] *2;
            } else
            {
                offY += (float)(size[0] - size[1] /2) /2f;
                size[0] = size[1] /2;
            }
        }

        pctY2 *= -1;
        pctX2 += 50;
        pctY2 += 50;

        pctX = (int)((float)size[1] * pctX2 / 100f);
        pctY = (int)((float)size[0] * pctY2 / 100f);

        pctX += (int)offX;
        pctY += (int)offY;

        size[0] = (int)((float)size[0] * pctH2 / 100f);
        size[1] = (int)((float)size[1] * pctW2 / 100f);

        string img2;
        try
        {
            img2 = LoadImg(img2Path, size[0], size[1]);
            size = ResizeImg(img2Path, size[0], size[1]);
        }
        catch (Exception err)
        {
            img2 = img2Path;
            size = GetSize(img2);
        }
        var imgDict2 = GetChars(img2);
        for (int i = -(int)((float)size[0] / 2f + .5f); i < (int)((float)size[0] / 2f); i++)
        {
            for (int j = -(int)((float)size[1] / 2f + .5f); j < (int)((float)size[1] / 2f); j++)
            {
                var c = imgDict2[i + (int)((float)size[0] / 2f + .5f)][j + (int)((float)size[1] / 2f + .5f)];
                if (c.Contains("Y")) continue;
                if (!keepSize || (imgDict1.ContainsKey(i + pctY) && imgDict1[i + pctY].ContainsKey(j + pctX)))
                {
                    if (!imgDict1.ContainsKey(i + pctY))
                    {
                        imgDict1[i + pctY] = new Dictionary<int, string>();
                    }
                    imgDict1[i + pctY][j + pctX] = c;
                }
            }
        }
        return Recompile(imgDict1);
    }

    public string AddingPct(string img1, string img2Path, float pctX2, float pctY2, float pctH2, float pctW2, bool keepSize = true)
    {
        return Adding(img1, img2Path, pctX2, pctY2, pctH2, pctW2, keepSize, true);
    }
    public string AddingPct(string img1, string img2Path, float pctX2, float pctY2, bool keepSize = true)
    {
        return AddingPct(img1, img2Path, pctX2, pctY2, Console.WindowHeight, Console.WindowWidth, keepSize);
    }
    public string AddingPct(string img1, string img2Path, bool keepSize = true)
    {
        return AddingPct(img1, img2Path, 0, 0, Console.WindowHeight, Console.WindowWidth, keepSize);
    }
    public string Adding(string img1, string img2Path, float X2, float Y2, float H2, float W2, bool keepSize = true)
    {
        return Adding(img1, img2Path, X2, Y2, H2, W2, keepSize, false);
    }
    public string Adding(string img1, string img2Path, float X2, float Y2, bool keepSize = true)
    {
        return Adding(img1, img2Path, X2, Y2, Console.WindowHeight, Console.WindowWidth, keepSize);
    }
    public string Adding(string img1, string img2Path, bool keepSize = true)
    {
        return Adding(img1, img2Path, 0, 0, Console.WindowHeight, Console.WindowWidth, keepSize);
    }

    public string GetEmptyImage(int h, int w)
    {
        var Path = "../../../img/ascii/";
        h++;
        if (CheckDir("EMPTIED-IMG", h, w))
        {
            return File.ReadAllText(Path + "EMPTIED-IMG/" + h + "x" + w + ".txt");
        }

        var res = "";
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                res += Colored.GetStrictColor(allColor.TEXT_BLACK) + "Y";
            }
            res += "\n";
        }
        if (!File.Exists(Path + "EMPTIED-IMG/" + h + "x" + w + ".txt"))
        {
            File.CreateText(Path + "EMPTIED-IMG/" + h + "x" + w + ".txt").Close();
        }
        File.WriteAllText(Path + "EMPTIED-IMG/" + h + "x" + w + ".txt", res);
        return res;
    }
    public string GetEmptyImage()
    {
        return GetEmptyImage(Console.WindowHeight +1, Console.WindowWidth);
    }
}
