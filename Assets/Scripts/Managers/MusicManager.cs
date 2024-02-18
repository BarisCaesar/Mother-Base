using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    private float Volume = .5f;
    private AudioSource GameAudioSource;

    private void Awake()
    {
        Volume = PlayerPrefs.GetFloat("MusicVolume", .5f);

        GameAudioSource = GetComponent<AudioSource>();
        GameAudioSource.volume = Volume;
    }
    public void IncreaseVolume()
    {
        Volume += .1f;
        Volume = Mathf.Clamp01(Volume);

        GameAudioSource.volume = Volume;
        PlayerPrefs.SetFloat("MusicVolume", Volume);
    }

    public void DecreaseVolume()
    {
        Volume -= .1f;
        Volume = Mathf.Clamp01(Volume);

        GameAudioSource.volume = Volume;
        PlayerPrefs.SetFloat("MusicVolume", Volume);
    }

    public float GetVolume()
    {
        return Volume;
    }
}
