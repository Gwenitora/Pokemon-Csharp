class Example
{
    public static void Main()
    {
        Colored.setColor(allColor.BgCyan);
        Console.Write("Test");
        Colored.setColor(allColor.TextRed);
        Console.Write("Test");
        Colored.setStrictColor(allColor.TextGreen);
        Console.Write("Test");
        Colored.setColor((int)allColor._Underscore + (int)allColor.BgMagenta);
        Console.Write("Test");
        Colored.setStrictColor((int)allColor._Dim + (int)allColor.TextRed);
        Console.Write("Test");

        Colored.resetColor();
    }
}