using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum allColor
{
    _RESET = 1 << 0,
    _BRIGHT = 1 << 1,
    _DIM = 1 << 2,
    _UNDERSCORE = 1 << 3,
    _BLINK = 1 << 4,
    _REVERSE = 1 << 5,
    _HIDDEN = 1 << 6,

    TEXT_BLACK = 1 << 7,
    TEXT_RED = 1 << 8,
    TEXT_GREEN = 1 << 9,
    TEXT_YELLOW = 1 << 10,
    TEXT_BLUE = 1 << 11,
    TEXT_MAGENTA = 1 << 12,
    TEXT_CYAN = 1 << 13,
    TEXT_WHITE = 1 << 14,
    TEXT_GRAY = 1 << 15,

    BG_BLACK = 1 << 16,
    BG_RED = 1 << 17,
    BG_GREEN = 1 << 18,
    BG_YELLOW = 1 << 19,
    BG_BLUE = 1 << 20,
    BG_MAGENTA = 1 << 21,
    BG_CYAN = 1 << 22,
    BG_WHITE = 1 << 23,
    BG_GRAY = 1 << 24,

    _PERSO_COLOR = 1 << 25
}

public class Colored
{
    public static Dictionary<int, List<int>> GetColors() {
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

    public static string GetColor(int col)
    {
        col = col & ~(1 << 26);
        return GetColor(col, 0, 0, 0, 0, 0, 0);
    }
    public static string GetColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
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
    public static string GetColor(allColor col)
    {
        return GetColor((int)col);
    }
    public static string GetColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        return GetColor((int)col, r, g, b, bgR, bgG, bgB);
    }
    public static string GetStrictColor(int col)
    {
        return GetColor(allColor._RESET) + GetColor(col);
    }
    public static string GetStrictColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        return GetColor(allColor._RESET) + GetColor(col, r, g, b, bgR, bgG, bgB);
    }
    public static string GetStrictColor(allColor col)
    {
        return GetStrictColor((int)col);
    }
    public static string GetStrictColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        return GetStrictColor((int)col, r, g, b, bgR, bgG, bgB);
    }
    public static void SetColor(int col)
    {
        Console.Write(GetColor(col));
    }
    public static void SetColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(GetColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void SetColor(allColor col)
    {
        Console.Write(GetColor(col));
    }
    public static void SetColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(GetColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void SetStrictColor(int col)
    {
        Console.Write(GetStrictColor(col));
    }
    public static void SetStrictColor(int col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(GetStrictColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void SetStrictColor(allColor col)
    {
        Console.Write(GetStrictColor(col));
    }
    public static void SetStrictColor(allColor col, int r, int g, int b, int bgR, int bgG, int bgB)
    {
        Console.Write(GetStrictColor(col, r, g, b, bgR, bgG, bgB));
    }
    public static void ResetColor()
    {
        Console.Write(GetColor(allColor._RESET));
    }
    private static string GetColorRgbSequence(int red, int green, int blue, bool isForeground)
    {
        string colorType = isForeground ? "38;2" : "48;2";
        return $"\u001b[{colorType};{red};{green};{blue}m";
    }
}