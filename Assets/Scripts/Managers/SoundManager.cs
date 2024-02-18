using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum SoundType
    {    
        BuildingDestroyed,
        BuildingDamaged,
        BuildingPlaced,
        EnemyDie,
        EnemyHit,
        EnemyWaveStarting,
        GameOver,
    }

    [SerializeField] Transform pfAudioSource;
    private Dictionary<SoundType, AudioClip> SoundTypeDictionary;
    private float Volume = .5f;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Volume = PlayerPrefs.GetFloat("SoundVolume", .5f);

        SoundTypeDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (SoundType Sound in Enum.GetValues(typeof(SoundType)))
        {
            SoundTypeDictionary[Sound] = Resources.Load<AudioClip>(Sound.ToString());
        }
    }


    public void PlaySound(SoundType Sound)
    {
        AudioSource GameAudioSource = Instantiate(pfAudioSource).GetComponent<AudioSource>();
        GameAudioSource.PlayOneShot(SoundTypeDictionary[Sound], Volume);
    }

    public void IncreaseVolume()
    {
        Volume += .1f;
        Volume = Mathf.Clamp01(Volume);
        PlayerPrefs.SetFloat("SoundVolume", Volume);
    }

    public void DecreaseVolume()
    {
        Volume -= .1f;
        Volume = Mathf.Clamp01(Volume);
        PlayerPrefs.SetFloat("SoundVolume", Volume);
    }

    public float GetVolume()
    {
        return Volume;
    }
}
