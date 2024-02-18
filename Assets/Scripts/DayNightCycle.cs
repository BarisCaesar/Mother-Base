using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Gradient LightGradient;
    [SerializeField] private float SecondsPerDay = 30f;

    private Light2D LightSource;
    private float DayTime;
    private float DayTimeSpeed;

    private void Awake()
    {
        LightSource = GetComponent<Light2D>();
        DayTimeSpeed = 1 / SecondsPerDay;
    }

    private void Update()
    {
        DayTime += Time.deltaTime * DayTimeSpeed;
        LightSource.color = LightGradient.Evaluate(DayTime % 1f);
    }
}
