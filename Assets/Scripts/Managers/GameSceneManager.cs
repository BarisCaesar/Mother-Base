using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager 
{
    public enum Scene
    {
        GameScene,
        MainMenuScene
    }
    public static void Load(Scene CurrentScene)
    {
        SceneManager.LoadScene(CurrentScene.ToString());
    }
}
