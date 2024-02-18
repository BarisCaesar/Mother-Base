using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    [SerializeField] private List<ResourceAmount> StartingResourceAmountList;

    private Dictionary<ResourceTypeSO, int> ResourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        
        ResourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO ResourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));

        foreach (ResourceTypeSO ResourceType in ResourceTypeList.List)
        {
            ResourceAmountDictionary[ResourceType] = 0;
        }

        foreach(ResourceAmount Resource in StartingResourceAmountList)
        {
            AddResource(Resource.ResourceType, Resource.Amount);
        }
    }
    
    public void AddResource(ResourceTypeSO ResourceType, int Amount)
    {
        ResourceAmountDictionary[ResourceType] += Amount;
        
        if (OnResourceAmountChanged != null)
        {
            OnResourceAmountChanged(this, EventArgs.Empty);
        }
        
    }

    public int GetResourceAmount(ResourceTypeSO ResourceType)
    {
        return ResourceAmountDictionary[ResourceType];
    }

    public bool CanAffordBuilding(ResourceAmount[] ResourceAmountArray)
    {
        foreach (ResourceAmount ResourceCost in ResourceAmountArray)
        {
            if (GetResourceAmount(ResourceCost.ResourceType) >= ResourceCost.Amount)
            { 
                continue; 
            }
            else
            { 
                return false;
            }
        }

        return true;
    }
    
    public void SpendResources(ResourceAmount[] ResourceAmountArray)
    {
        foreach (ResourceAmount ResourceCost in ResourceAmountArray)
        { 
            ResourceAmountDictionary[ResourceCost.ResourceType] -= ResourceCost.Amount;
        }
    }
}
