using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator BuildingResourceGenerator;

    private Transform BarTransfrom;
    private void Start()
    {
        ResourceGeneratorData ResourceData = BuildingResourceGenerator.GetResourceGeneratorData();
        BarTransfrom = transform.Find("Bar");

        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = ResourceData.ResourceType.ResourceSprite;
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(BuildingResourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {       
        BarTransfrom.localScale = new Vector3(1 - BuildingResourceGenerator.GetTimerNormalized(), 1.0f , 1.0f);
    }
}
