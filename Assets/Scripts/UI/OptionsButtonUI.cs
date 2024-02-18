using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtonUI : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            OptionsUI.Instance.ToggleVisibility();
        }
    }
}
