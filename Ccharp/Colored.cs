using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum allColor
{
    _Reset = 1 << 0,
    _Bright = 1 << 1,
    _Dim = 1 << 2,
    _Underscore = 1 << 3,
    _Blink = 1 << 4,
    _Reverse = 1 << 5,
    _Hidden = 1 << 6,

    TextBlack = 1 << 7,
    TextRed = 1 << 8,
    TextGreen = 1 << 9,
    TextYellow = 1 << 10,
    TextBlue = 1 << 11,
    TextMagenta = 1 << 12,
    TextCyan = 1 << 13,
    TextWhite = 1 << 14,
    TextGray = 1 << 15,

    BgBlack = 1 << 16,
    BgRed = 1 << 17,
    BgGreen = 1 << 18,
    BgYellow = 1 << 19,
    BgBlue = 1 << 20,
    BgMagenta = 1 << 21,
    BgCyan = 1 << 22,
    BgWhite = 1 << 23,
    BgGray = 1 << 24
}

public class Colored
{
    public static string getColor(int col)
    {
        string txt = "";
        List<string> colors = new List<string>(25) {
            "\x1b[0m",
            "\x1b[1m",
            "\x1b[2m",
            "\x1b[4m",
            "\x1b[5m",
            "\x1b[7m",
            "\x1b[8m",

            "\x1b[30m",
            "\x1b[31m",
            "\x1b[32m",
            "\x1b[33m",
            "\x1b[34m",
            "\x1b[35m",
            "\x1b[36m",
            "\x1b[37m",
            "\x1b[90m",

            "\x1b[40m",
            "\x1b[41m",
            "\x1b[42m",
            "\x1b[43m",
            "\x1b[44m",
            "\x1b[45m",
            "\x1b[46m",
            "\x1b[47m",
            "\x1b[100m"
        };
        for (int i = 0; i < 25; i++)
        {
            if (((int)col & (1 << i)) != 0)
            {
                txt += colors[i];
            }
        }
        return txt;
    }
    public static string getColor(allColor col)
    {
        return getColor((int)col);
    }
    public static string getStrictColor(int col)
    {
        return getColor(allColor._Reset) + getColor(col);
    }
    public static string getStrictColor(allColor col)
    {
        return getStrictColor((int)col);
    }
    public static void setColor(int col)
    {
        Console.Write(getColor(col));
    }
    public static void setColor(allColor col)
    {
        Console.Write(getColor(col));
    }
    public static void setStrictColor(int col)
    {
        Console.Write(getStrictColor(col));
    }
    public static void setStrictColor(allColor col)
    {
        Console.Write(getStrictColor(col));
    }
    public static void resetColor()
    {
        Console.Write(getColor(allColor._Reset));
    }
}