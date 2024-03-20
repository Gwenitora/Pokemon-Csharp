using System.Drawing;

class Example
{
    public static void Main()
    {
        ascii asc = new ascii();
        while (true)
        {
            var _h = Console.WindowHeight;
            var _w = Console.WindowWidth;
            if (_h <= 0 || _w <= 0) continue;
            Console.SetCursorPosition(0, 0);
            Console.Write(asc.loadImg("cat-example.png"));
        }

        Colored.resetColor();
    }
}