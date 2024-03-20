using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class ascii
{
    private string chars = " ";

    public ascii()
    {
        var startCount = chars.Count();
        for (int i = 1; i <= startCount; i++)
        {
            chars += chars.Substring(startCount - i, 1);
        }
    }

    private string getChar(int val)
    {
        val = Math.Min(val, 255);
        val = Math.Max(val, 0);
        float len = (float)256 / (float)(chars.Count());
        int choose = (int)(val / len);
        return chars.Substring(Math.Abs(choose), 1);
    }

    private string genChar(float r, float g, float b)
    {
        float moy = (r + g + b) / (float)3;
        float dist = 1 << 15;
        int saved = 0;
        var colors = Colored.getColors();
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
        return Colored.getStrictColor((int)allColor._PersoCol + (int)allColor.TextBlack, 0, 0, 0, (int)r, (int)g, (int)b) + getChar((int)moy);
    }

    public List<int> resizeImg(int height, int width, int h, int w)
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

    private List<int> getColorZone(Bitmap img, int x, int y, int h)
    {
        int w = h / 2;

        float r = 0, g = 0, b = 0, l = 0;

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

        return new List<int>(3) {(int)(r / l), (int)(g / l), (int)(b / l)};
    }

    private bool checkDir(string imgPath, int h, int w)
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
            File.CreateText(Path + imgPath + "/" + h + "x" + w + ".txt").Close();
            return false;
        }
        return true;
    }

    public string loadImg(string imgPath, int h, int w)
    {
        w = Math.Min(w, 750);
        w = Math.Max(w, 1);
        h = Math.Min(h, 375);
        h = Math.Max(h, 1);
        var Path = "../../../img/";
        string res = "";

        if (checkDir(imgPath, h, w))
        {
            return File.ReadAllText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt");
        }

        var img = new Bitmap(Path + imgPath);

        var size = resizeImg(img.Height, img.Width, h, w);
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
                        res += Colored.getColor(allColor._Reset) + " ";
                        continue;
                    }
                    var col = getColorZone(
                        img,
                        (int)(((float)i - (float)offsetH) / (float)height * (float)img.Height),
                        (int)(((float)j - (float)offsetW) / (float)width * (float)img.Width),
                        (int)((float)img.Height / (float)h)
                    );
                    string t = genChar(col[0], col[1], col[2]);

                    res += t;
                }
                res += "\n";
            }
        }

        File.WriteAllText(Path + "ascii/" + imgPath + "/" + h + "x" + w + ".txt", res);

        return res;
    }
    public string loadImg(string imgPath)
    {
        return loadImg(imgPath, Console.WindowHeight, Console.WindowWidth);
    }
}