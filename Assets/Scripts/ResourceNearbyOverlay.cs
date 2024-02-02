using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData ResourceData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        int NearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(ResourceData, transform.position - transform.localPosition);

        float Efficency = Mathf.RoundToInt((float)NearbyResourceAmount / ResourceData.MaxResourceAmount * 100.0f);
        transform.Find("Text").GetComponent<TextMeshPro>().SetText(Efficency + "%");

    }

    public void Show(ResourceGeneratorData ResourceData)
    {
        this.ResourceData = ResourceData;
        gameObject.SetActive(true);

        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = ResourceData.ResourceType.ResourceSprite;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
