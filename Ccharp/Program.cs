class Example
{
    public static void Main()
    {
        InputManager m_input = new InputManager();
        m_input.Init();
        m_input.Test();
        while (true)
        {    
            m_input.SetCursorPos();        
        }
        //ascii asc = new ascii();
        //asc.test();

        //Colored.resetColor();
    }
}