using System.Drawing;
using System;
using System.Windows.Input;
using System.Threading;
using System.Security.Cryptography.Xml;

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
    bool emptiedTest;

    public Point CursorPos { get => cursorPos; }

    public InputManager()
    {
        KeyStates.Clear();
        KeyStates.Add(ConsoleKey.LeftArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.RightArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.UpArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.DownArrow, KeyState.INACTIVE);
        KeyStates.Add(ConsoleKey.Spacebar, KeyState.INACTIVE);

        emptiedTest = false;
    }

    public void Test()
    {
        Console.WriteLine("Hello cats !");
        Console.WriteLine("Hello cats !");
    }


    private void GetKeyState()
    {
        PreviousKeyStates = KeyStates;
        key_info = Console.ReadKey();

        KeyStates[ConsoleKey.LeftArrow] = GetKeyStateHelper(ConsoleKey.LeftArrow);
        KeyStates[ConsoleKey.RightArrow] = GetKeyStateHelper(ConsoleKey.RightArrow);
        KeyStates[ConsoleKey.UpArrow] = GetKeyStateHelper(ConsoleKey.UpArrow);
        KeyStates[ConsoleKey.DownArrow] = GetKeyStateHelper(ConsoleKey.DownArrow);
        KeyStates[ConsoleKey.Spacebar] = GetKeyStateHelper(ConsoleKey.Spacebar);
    }

    private KeyState GetKeyStateHelper(ConsoleKey key)
    {
        if (key_info.Key == key && !emptiedTest)
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
            if (PreviousKeyStates[key] == KeyState.RELEASED || PreviousKeyStates[key] == KeyState.INACTIVE)
            {
                return KeyState.INACTIVE;
            }
            else
            {
                return KeyState.RELEASED;
            }
        }
    }
    
    public void SetCursorPos(Map m_map, SceneManager m_scene_manager, Data datas)
    {
        GetKeyState();
        if ((KeyStates[ConsoleKey.LeftArrow] == KeyState.PRESSED || KeyStates[ConsoleKey.LeftArrow] == KeyState.HELD) && m_map.getTile().GetBorder(rotationTile.LEFT).Substring(3, 1) == " ")
        {
            cursorPos.X--;
            if (m_map.getIfGreen())
            {
                m_scene_manager.Fight(datas, datas.GetChakimonList()[1]);
            }
        }
        else if ((KeyStates[ConsoleKey.RightArrow] == KeyState.PRESSED || KeyStates[ConsoleKey.RightArrow] == KeyState.HELD) && m_map.getTile().GetBorder(rotationTile.RIGHT).Substring(3, 1) == " ")
        {
            cursorPos.X++;
            if (m_map.getIfGreen())
            {
                m_scene_manager.Fight(datas, datas.GetChakimonList()[1]);
            }
        }
        else if ((KeyStates[ConsoleKey.UpArrow] == KeyState.PRESSED || KeyStates[ConsoleKey.UpArrow] == KeyState.HELD) && m_map.getTile().GetBorder(rotationTile.DOWN).Substring(3, 1) == " ")
        {
            cursorPos.Y++;
            if (m_map.getIfGreen())
            {
                m_scene_manager.Fight(datas, datas.GetChakimonList()[1]);
            }
        }
        else if ((KeyStates[ConsoleKey.DownArrow] == KeyState.PRESSED || KeyStates[ConsoleKey.DownArrow] == KeyState.HELD) && m_map.getTile().GetBorder(rotationTile.UP).Substring(3, 1) == " ")
        {
            cursorPos.Y--;
            if (m_map.getIfGreen())
            {
                m_scene_manager.Fight(datas, datas.GetChakimonList()[1]);
            }
        }
    }
}