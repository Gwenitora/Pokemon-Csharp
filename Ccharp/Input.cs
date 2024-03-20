using System.Drawing;
using System;
using System.Windows.Input;

public class InputManager
{
    enum KeyState
    {
        Pressed,
        Held,
        Released,
        Inactive
    };

    Point cursorPos = new Point();
    Dictionary<char, KeyState>KeyStates;

    public void Test()
    {
        Console.WriteLine("Hello cats !");
        Console.WriteLine("Hello cats !");
        Point cursorPos = GetCursorPos();
    }

    public void GetKeyState ()
    {
        //Console.ReadLine();
        //char keyPressed 
        //return keyPressed;
    }

    public Point GetCursorPos()
    {
        (int cursorPosX, int cursorPosY) = Console.GetCursorPosition();
        cursorPos.X = cursorPosX;
        cursorPos.Y = cursorPosY;
        Console.WriteLine(cursorPos);
        return cursorPos;
    }
    
    public void SetCursorPos()
    {
        
        //char keyPressed = GetKeyStates();
        //if (keyPressed == 'Z')
        {
        }
        Console.SetCursorPosition(0, 0);
    }
}