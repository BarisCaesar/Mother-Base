using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;

    [SerializeField] private SoundManager GameSoundManager;
    [SerializeField] private MusicManager GameMusicManager;

    private TextMeshProUGUI SoundText;
    private TextMeshProUGUI MusicText;
    private void Awake()
    {
        if(Instance != null && this != Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        SoundText = transform.Find("SoundVolumeText").GetComponent<TextMeshProUGUI>();
        MusicText = transform.Find("MusicVolumeText").GetComponent<TextMeshProUGUI>();

        transform.Find("SoundPlusButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSoundManager.IncreaseVolume();
            UpdateText();

        });

        transform.Find("SoundMinusButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSoundManager.DecreaseVolume();
            UpdateText();
        });

        transform.Find("MusicPlusButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameMusicManager.IncreaseVolume();
            UpdateText();
        });

        transform.Find("MusicMinusButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameMusicManager.DecreaseVolume();
            UpdateText();
        });

        transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool Set) =>
        {
            CameraHandler.Instance.SetEdgeScrolling(Set);
        });

        
    }

    private void Start()
    {
        gameObject.SetActive(false);
        UpdateText();

        transform.Find("EdgeScrollingToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrollingState());
    }

    private void UpdateText()
    {
        SoundText.SetText(Mathf.RoundToInt(GameSoundManager.GetVolume() * 10).ToString());
        MusicText.SetText(Mathf.RoundToInt(GameMusicManager.GetVolume() * 10).ToString());
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        
        if(gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
