using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO ResourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> ResourceTypeTransformDictionary;
    private void Awake()
    {
        ResourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

        ResourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform ResourceTemplate = transform.Find("ResourceTemplate");
        ResourceTemplate.gameObject.SetActive(false);

        int Index = 0;
        foreach (ResourceTypeSO ResourceType in ResourceTypeList.List)
        {
            Transform ResourceTransform = Instantiate(ResourceTemplate, transform);
            ResourceTransform.gameObject.SetActive(true);
            
            float OffsetAmount = -160;
            ResourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(Index*OffsetAmount, 0);

            ResourceTransform.Find("Image").GetComponent<Image>().sprite = ResourceType.ResourceSprite;

            ResourceTypeTransformDictionary[ResourceType] = ResourceTransform;
            
            Index++;

        }
        
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();   
    }
    

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }
    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO ResourceType in ResourceTypeList.List)
        {
            int ResourceAmount = ResourceManager.Instance.GetResourceAmount(ResourceType);
            Transform ResourceTransform = ResourceTypeTransformDictionary[ResourceType];

            ResourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(ResourceAmount.ToString());
        }
        
    }
}
