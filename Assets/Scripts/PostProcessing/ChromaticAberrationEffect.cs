using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberrationEffect : MonoBehaviour
{
    public static ChromaticAberrationEffect Instance { get; private set; }

    private Volume EffectVolume;
    private float DecreaseSpeed;

    private void Awake()
    {
        if (Instance != null && this != Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DecreaseSpeed = 1f;
        EffectVolume = GetComponent<Volume>();       
    }
    private void Update()
    {
        if(EffectVolume.weight > 0 )
        {
            EffectVolume.weight -= DecreaseSpeed * Time.deltaTime;   
        }   
    }

    public void SetWeight( float Weight )
    {
        EffectVolume.weight = Weight; 
    }
}
