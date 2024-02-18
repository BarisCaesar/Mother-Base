using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    public static GameOverUI Instance { get; private set; }

    [SerializeField] private EnemyWaveManager WaveManager;
    private void Awake()
    {
        Instance = this;

        Hide();

        transform.Find("RetryButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find("WavesSurvivedText").GetComponent<TextMeshProUGUI>().
            SetText("You Have Survived " + WaveManager.GetWaveCount() + " Waves!");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
