using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControl : MonoBehaviour
{
    private AudioSource GameAudioSource;

    private void Awake()
    {
        GameAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!GameAudioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
