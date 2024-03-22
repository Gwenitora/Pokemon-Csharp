using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ascii
{
    private List<string> chars = new List<string>()
    {
        " ",
        ".",
        ":",
        "o",
        "&",
        "8",
        "#",
        "@"
    };

    private string getChar(int val)
    {
        float len = (float)256 / (float)(chars.Count());
        int choose = (int)(val / len);
        return chars[choose];
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
        return Colored.getStrictColor((int)allColor.TextBlack << saved) + getChar((int)moy);
    }

    public void test()
    {
        var h = Console.WindowHeight;
        var w = Console.WindowWidth;
        while (true)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    var I = i * 256 / h;
                    var J = j * 256 / w;
                    var moy = (I + J) / 2;
                    string t = genChar(J, moy, I);
                    Console.Write(t);
                }
                //Console.WriteLine();
            }
            Console.SetCursorPosition(0, 0);
            h = Console.WindowHeight;
            w = Console.WindowWidth;
        }
    }
}