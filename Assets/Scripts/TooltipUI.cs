using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    
    [SerializeField] private RectTransform CanvasRectTransform;
    
    private RectTransform UIRectTransform;
    private TextMeshProUGUI UIText;
    private RectTransform BackgroundRectTransform;
    private TooltipTimer Timer;
    

    private void Awake()
    {
        Instance = this;
        
        UIRectTransform = GetComponent<RectTransform>();
        UIText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        BackgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();
        
        if (Timer != null)
        {
            Timer.Count -= Time.deltaTime;
            if (Timer.Count <= 0)
            {
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 AnchoredPosition = Input.mousePosition / CanvasRectTransform.localScale.x;

        if (AnchoredPosition.x + BackgroundRectTransform.rect.width > CanvasRectTransform.rect.width)
        {
            AnchoredPosition.x = CanvasRectTransform.rect.width - BackgroundRectTransform.rect.width;
        }
        if (AnchoredPosition.y + BackgroundRectTransform.rect.height > CanvasRectTransform.rect.height)
        {
            AnchoredPosition.y = CanvasRectTransform.rect.height - BackgroundRectTransform.rect.height;
        }
        UIRectTransform.anchoredPosition = AnchoredPosition;
        
    }

    private void SetText(string TooltipText)
    {
        UIText.SetText(TooltipText);
        UIText.ForceMeshUpdate();

        Vector2 TextSize = UIText.GetRenderedValues(false);
        Vector2 Padding = new Vector2(8, 8);
        BackgroundRectTransform.sizeDelta = TextSize + Padding;
    }

    public void Show(string TooltipText, TooltipTimer Timer = null)
    {
        this.Timer = Timer;
        gameObject.SetActive(true);
        SetText(TooltipText);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float Count;
    }
}
