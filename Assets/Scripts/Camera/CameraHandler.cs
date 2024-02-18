using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    [SerializeField] private PolygonCollider2D CameraBoundsCollider2D;

    private float OrtographicSize;
    private float TargetOrtographicSize;
    private Camera MainCamera;
    private bool IsEdgeScrollActivated;

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

        IsEdgeScrollActivated = PlayerPrefs.GetInt("EdgeScrolling", 1) == 1;
    }

    private void Start()
    {
        OrtographicSize = VirtualCamera.m_Lens.OrthographicSize;
        TargetOrtographicSize = OrtographicSize;
        MainCamera = Camera.main;
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


        if(IsEdgeScrollActivated)
        {
            float EdgeScrollingSize = 30f;

            if (Input.mousePosition.x > Screen.width - EdgeScrollingSize)
            {
                x = 1f;
            }
            if (Input.mousePosition.x < EdgeScrollingSize)
            {
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - EdgeScrollingSize)
            {
                y = 1f;
            }
            if (Input.mousePosition.y < EdgeScrollingSize)
            {
                y = -1f;
            }
        }
        
        Vector3 MovementDir = new Vector3(x, y).normalized;
        float MoveSpeed = 30.0f;

        Vector3 MovementVector = transform.position + (MovementDir * MoveSpeed * Time.deltaTime);

        MovementVector.x = Mathf.Clamp(MovementVector.x,
            CameraBoundsCollider2D.bounds.min.x + OrtographicSize * MainCamera.aspect,
            CameraBoundsCollider2D.bounds.max.x - OrtographicSize * MainCamera.aspect);

        MovementVector.y = Mathf.Clamp(MovementVector.y,
            CameraBoundsCollider2D.bounds.min.y + OrtographicSize,
            CameraBoundsCollider2D.bounds.max.y - OrtographicSize);

        transform.position = MovementVector;
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

    public void SetEdgeScrolling(bool SetEdgeScrolling)
    {
        IsEdgeScrollActivated = SetEdgeScrolling;
        PlayerPrefs.SetInt("EdgeScrolling", IsEdgeScrollActivated ? 1 : 0);
    }

    public bool GetEdgeScrollingState()
    {
        return IsEdgeScrollActivated;
    }
}
