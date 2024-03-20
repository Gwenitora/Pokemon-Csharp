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
    BgGray = 1 << 24,

    _PersoCol = 1 << 25
}

public class Colored
{
    public static Dictionary<int, List<int>> getColors() {
        var colors = new Dictionary<int, List<int>>(9);
        colors[0] = new List<int>(3) {0, 0, 0};
        colors[1] = new List<int>(3) {197, 15, 31};
        colors[2] = new List<int>(3) {19, 161, 14};
        colors[3] = new List<int>(3) {193, 156, 0};
        colors[4] = new List<int>(3) {0, 55, 218};
        colors[5] = new List<int>(3) {136, 23, 152};
        colors[6] = new List<int>(3) {58, 150, 221};
        colors[7] = new List<int>(3) {204, 204, 204};
        colors[8] = new List<int>(3) {118, 118, 118};
        return colors;
    }

    public static string getColor(int col)
    {
        col = col & ~(1 << 26);
        return getColor(col, 0, 0, 0, 0, 0, 0);
    }
    public static string getColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
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
        if (((int)col & (1 << 25)) != 0)
        {
            if (r != 0 || g != 0 || b != 0)
            {
                txt += GetColorRgbSequence(r, g, b, true);
            }
            if (bgR != 0 || bgG != 0 || bgB != 0)
            {
                txt += GetColorRgbSequence(bgR, bgG, bgB, false);
            }
        }
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
    public static string getColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        return getColor((int)col, r, g, b, bgR, bgG, bgB);
    }
    public static string getStrictColor(int col)
    {
        return getColor(allColor._Reset) + getColor(col);
    }
    public static string getStrictColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        return getColor(allColor._Reset) + getColor(col, r, g, b, bgR, bgG, bgB);
    }
    public static string getStrictColor(allColor col)
    {
        return getStrictColor((int)col);
    }
    public static string getStrictColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        return getStrictColor((int)col, r, g, b, bgR, bgG, bgB);
    }
    public static void setColor(int col)
    {
        Console.Write(getColor(col));
    }
    public static void setColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(getColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void setColor(allColor col)
    {
        Console.Write(getColor(col));
    }
    public static void setColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(getColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void setStrictColor(int col)
    {
        Console.Write(getStrictColor(col));
    }
    public static void setStrictColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(getStrictColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void setStrictColor(allColor col)
    {
        Console.Write(getStrictColor(col));
    }
    public static void setStrictColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(getStrictColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void resetColor()
    {
        Console.Write(getColor(allColor._Reset));
    }
    private static string GetColorRgbSequence(int red, int green, int blue, bool isForeground)
    {
        string colorType = isForeground ? "38;2" : "48;2";
        return $"\u001b[{colorType};{red};{green};{blue}m";
    }
}