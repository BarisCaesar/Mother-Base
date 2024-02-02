using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler OnMouseEnter;
    public event EventHandler OnMouseExit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnMouseEnter != null)
        {
            OnMouseEnter(this, EventArgs.Empty);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnMouseExit != null)
        {
            OnMouseExit(this, EventArgs.Empty);    
        }
        
    }
}
