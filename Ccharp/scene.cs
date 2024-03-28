using System;
using System.Collections.Generic;

public enum Scene
{
    MainMenu,
    Inventory,
    Fight,
    Game,
    GameOver
}

public class SceneManager
{

    // Scène actuelle
    private Scene currentScene;

    public SceneManager()
    {
        // Définition de la scène initiale
        currentScene = Scene.MainMenu;
    }

    // Méthode pour récupérer la scène actuelle
    public Scene GetCurrentScene()
    {
        return currentScene;
    }

    public void SetCurrentScene(Scene curent_scene)
    {
        currentScene = curent_scene;
    }

    // Méthodes associées à chaque scène
    public void MainMenu()
    {
        Console.WriteLine("Vous êtes dans le menu principal.");
        // Logique du menu principal
    }

    public void Inventory()
    {
        Console.WriteLine("Vous êtes dans l'inventaire");
    }
    
    public void Fight(Data data, Chakimon chakimon)
    {
        FightScene fs = new FightScene(data, chakimon);
        fs.FightSceneGameLoop();

    }

    public void Game(Map map, Ascii ascii, InputManager input)
    {
        Console.WriteLine("Vous êtes en jeu.");
        // TODO: don't touch next paragraphe
        var res = map.GetDraw(ascii.GetEmptyImage(), ascii, input.CursorPos.X, input.CursorPos.Y);
        Console.Write(res);
        Console.SetCursorPosition(0, 0);
        input.SetCursorPos(map);
    }

    public void GameOver()
    {
        Console.WriteLine("Game Over.");
        // Logique de fin de jeu
    }
}