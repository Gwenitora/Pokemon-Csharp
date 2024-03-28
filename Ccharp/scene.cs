using System;
using System.Collections.Generic;

public enum Scene
{
    MainMenu,
    Inventory,
    Game,
    GameOver
}

public class SceneManager
{
    // Dictionnaire associant les sc�nes � leur nom
    private Dictionary<Scene, Action> sceneDictionary;

    // Sc�ne actuelle
    private Scene currentScene;

    public SceneManager()
    {
        sceneDictionary = new Dictionary<Scene, Action>();

        // Ajout des sc�nes et de leurs actions associ�es
        sceneDictionary.Add(Scene.MainMenu, MainMenu);
        sceneDictionary.Add(Scene.Inventory, Inventory);
        sceneDictionary.Add(Scene.Game, Game);
        sceneDictionary.Add(Scene.GameOver, GameOver);

        // D�finition de la sc�ne initiale
        currentScene = Scene.MainMenu;
    }
    // M�thode pour changer de sc�ne
    public void ChangeScene(Scene newScene)
    {
        currentScene = newScene;
    }

    // M�thode pour mettre � jour la sc�ne actuelle
    public void UpdateCurrentScene()
    {
        // V�rification si la sc�ne existe dans le dictionnaire
        if (sceneDictionary.ContainsKey(currentScene))
        {
            // Appel de la m�thode associ�e � la sc�ne actuelle
            sceneDictionary[currentScene].Invoke();
        }
        else
        {
            Console.WriteLine("La sc�ne actuelle n'a pas �t� trouv�e dans le dictionnaire.");
        }
    }

    // M�thode pour r�cup�rer la sc�ne actuelle
    public Scene GetCurrentScene()
    {
        return currentScene;
    }
    
    public void SetCurrentScene(Scene curent_scene)
    {
        currentScene = curent_scene;
    }

    // M�thodes associ�es � chaque sc�ne
    private void MainMenu()
    {
        Console.WriteLine("Vous �tes dans le menu principal.");
        // Logique du menu principal
    }
    
    private void Inventory()
    {
        Console.WriteLine("Vous �tes dans l'inventaire");
        // Logique du menu principal
    }

    private void Game()
    {
        Console.WriteLine("Vous �tes en jeu.");
        // Logique du jeu
    }

    private void GameOver()
    {
        Console.WriteLine("Game Over.");
        // Logique de fin de jeu
    }
}