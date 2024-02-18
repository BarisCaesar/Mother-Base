using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction CurrentBuildingConstruction;
    private Image ConstructionProgressImage;

    private void Awake()
    {
        ConstructionProgressImage = transform.Find("Mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        ConstructionProgressImage.fillAmount = CurrentBuildingConstruction.GetProgressNormalized();
    }
}
