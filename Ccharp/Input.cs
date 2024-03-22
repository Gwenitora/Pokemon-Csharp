using System.Drawing;
using System;
using System.Windows.Input;

public class InputManager
{
    enum KeyState
    {
        PRESSED,
        HELD,
        RELEASED,
        INACTIVE
    };

    Point cursorPos = new Point();
    Dictionary<ConsoleKey, KeyState>KeyStates = new Dictionary<ConsoleKey, KeyState>();
    Dictionary<ConsoleKey, KeyState>PreviousKeyStates = new Dictionary<ConsoleKey, KeyState>();
    ConsoleKeyInfo key_info;

    public void Init()
    {
        KeyStates.Clear();
        KeyStates.Add(ConsoleKey.LeftArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.RightArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.UpArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.DownArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.Spacebar, KeyState.INACTIVE);
    }

    public void Test()
    {
        Console.WriteLine("Hello cats !");
        Console.WriteLine("Hello cats !");
    }


    private void GetKeyState ()
    {
        PreviousKeyStates = KeyStates;
        key_info = Console.ReadKey();

        KeyStates[ConsoleKey.LeftArrow] = GetKeyStateHelper(ConsoleKey.LeftArrow);
        KeyStates[ConsoleKey.RightArrow] = GetKeyStateHelper(ConsoleKey.RightArrow);
        KeyStates[ConsoleKey.UpArrow] = GetKeyStateHelper(ConsoleKey.UpArrow);
        KeyStates[ConsoleKey.DownArrow] = GetKeyStateHelper(ConsoleKey.DownArrow);
        KeyStates[ConsoleKey.Spacebar] = GetKeyStateHelper(ConsoleKey.Spacebar);

        // --- TEST
        /*Console.WriteLine("left arrow");
        Console.WriteLine(KeyStates[ConsoleKey.LeftArrow]);
        Console.WriteLine("right arrow");
        Console.WriteLine(KeyStates[ConsoleKey.RightArrow]);
        Console.WriteLine("up arrow");
        Console.WriteLine(KeyStates[ConsoleKey.UpArrow]);
        Console.WriteLine("down arrow");
        Console.WriteLine(KeyStates[ConsoleKey.DownArrow]);
        Console.WriteLine(ConsoleKey.Spacebar);
        Console.WriteLine(KeyStates[ConsoleKey.Spacebar]);*/

    }

    private KeyState GetKeyStateHelper(ConsoleKey key)
    {
        if (key_info.Key == key)
        {
            if (PreviousKeyStates[key] == KeyState.PRESSED || PreviousKeyStates[key] == KeyState.HELD)
            {
                return KeyState.HELD;
            }
            else
            {
                return KeyState.PRESSED;
            }
        }
        else
        {
            if (PreviousKeyStates[key] == KeyState.PRESSED || PreviousKeyStates[key] == KeyState.HELD)
            {
                return KeyState.RELEASED;
            }
            else
            {
                return KeyState.INACTIVE;
            }
        }
    }

    private Point GetCursorPos()
    {
        (int cursorPosX, int cursorPosY) = Console.GetCursorPosition();
        cursorPos.X = cursorPosX;
        cursorPos.Y = cursorPosY;
        //Console.WriteLine(cursorPos);
        return cursorPos;
    }
    
    public void SetCursorPos()
    {
        GetKeyState();
        GetCursorPos();
        if (KeyStates[ConsoleKey.LeftArrow] == KeyState.PRESSED && cursorPos.X != 0 || KeyStates[ConsoleKey.LeftArrow] == KeyState.HELD && cursorPos.X != 0)
        {
            Console.SetCursorPosition(cursorPos.X - 1, cursorPos.Y);
        }
        else if (KeyStates[ConsoleKey.RightArrow] == KeyState.PRESSED || KeyStates[ConsoleKey.RightArrow] == KeyState.HELD)
        {
            Console.SetCursorPosition(cursorPos.X + 1, cursorPos.Y);
        }
        
        else if (KeyStates[ConsoleKey.UpArrow] == KeyState.PRESSED && cursorPos.Y != 0 || KeyStates[ConsoleKey.UpArrow] == KeyState.HELD && cursorPos.Y !=0)
        {
            Console.SetCursorPosition(cursorPos.X, cursorPos.Y - 1);
        }
        
        else if (KeyStates[ConsoleKey.DownArrow] == KeyState.PRESSED || KeyStates[ConsoleKey.DownArrow] == KeyState.HELD)
        {
            Console.SetCursorPosition(cursorPos.X, cursorPos.Y + 1);
        }
    }
}