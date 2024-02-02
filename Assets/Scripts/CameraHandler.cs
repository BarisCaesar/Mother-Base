using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;

    private float OrtographicSize;
    private float TargetOrtographicSize;

    private void Start()
    {
        OrtographicSize = VirtualCamera.m_Lens.OrthographicSize;
        TargetOrtographicSize = OrtographicSize;
    }

    private void Update()
    {
        HandleMovement();
        
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 MovementDir = new Vector3(x, y).normalized;
        float MoveSpeed = 30.0f;
        transform.position += MovementDir * MoveSpeed * Time.deltaTime; 
    }

    private void HandleZoom()
    {
        float ZoomAmount = 2.0f;
        float MinOrtographicSize = 10.0f;
        float MaxOrtographicSize = 30.0f;
        TargetOrtographicSize += -Input.mouseScrollDelta.y * ZoomAmount;
        TargetOrtographicSize = Mathf.Clamp(TargetOrtographicSize, MinOrtographicSize, MaxOrtographicSize);

        float ZoomSpeed = 5.0f;
        OrtographicSize = Mathf.Lerp(OrtographicSize, TargetOrtographicSize, Time.deltaTime * ZoomSpeed);
        VirtualCamera.m_Lens.OrthographicSize = OrtographicSize; 
        
    }
}
