using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin CinemachineMultiChannelPerlin;

    private float Timer;
    private float TimerMax;
    private float StartingIntensity;

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
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineMultiChannelPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if(Timer < TimerMax)
        {
            Timer += Time.deltaTime;
            float Amplitude = Mathf.Lerp(StartingIntensity, 0f, Timer / TimerMax);
            CinemachineMultiChannelPerlin.m_AmplitudeGain = Amplitude;
        }
        
    }

    public void ShakeCamera(float Intensity, float TimerMax)
    {
        this.TimerMax = TimerMax;
        this.StartingIntensity = Intensity;
        Timer = 0f;
        CinemachineMultiChannelPerlin.m_AmplitudeGain = Intensity;
    }
}
