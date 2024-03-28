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
    // Dictionnaire associant les scènes à leur nom
    private Dictionary<Scene, Action> sceneDictionary;

    // Scène actuelle
    private Scene currentScene;

    public SceneManager()
    {
        sceneDictionary = new Dictionary<Scene, Action>();

        // Ajout des scènes et de leurs actions associées
        sceneDictionary.Add(Scene.MainMenu, MainMenu);
        sceneDictionary.Add(Scene.Inventory, Inventory);
        sceneDictionary.Add(Scene.Game, Game);
        sceneDictionary.Add(Scene.GameOver, GameOver);

        // Définition de la scène initiale
        currentScene = Scene.MainMenu;
    }
    // Méthode pour changer de scène
    public void ChangeScene(Scene newScene)
    {
        currentScene = newScene;
    }

    // Méthode pour mettre à jour la scène actuelle
    public void UpdateCurrentScene()
    {
        // Vérification si la scène existe dans le dictionnaire
        if (sceneDictionary.ContainsKey(currentScene))
        {
            // Appel de la méthode associée à la scène actuelle
            sceneDictionary[currentScene].Invoke();
        }
        else
        {
            Console.WriteLine("La scène actuelle n'a pas été trouvée dans le dictionnaire.");
        }
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
    private void MainMenu()
    {
        Console.WriteLine("Vous êtes dans le menu principal.");
        // Logique du menu principal
    }
    
    private void Inventory()
    {
        Console.WriteLine("Vous êtes dans l'inventaire");
        // Logique du menu principal
    }

    private void Game()
    {
        Console.WriteLine("Vous êtes en jeu.");
        // Logique du jeu
    }

    private void GameOver()
    {
        Console.WriteLine("Game Over.");
        // Logique de fin de jeu
    }
}