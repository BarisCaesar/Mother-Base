using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    public static int GetNearbyResourceAmount(ResourceGeneratorData ResourceData, Vector3 Position)
    {
        Collider2D[] Collider2DArray = Physics2D.OverlapCircleAll(Position, ResourceData.ResourceDetectionRadius);

        int NearbyResourceAmount = 0;
        foreach (Collider2D ObjectCollider in Collider2DArray)
        {
            ResourceNode CollidedResourceNode = ObjectCollider.GetComponent<ResourceNode>();
            if (CollidedResourceNode != null)
            {
                if (CollidedResourceNode.ResourceType == ResourceData.ResourceType)
                {
                    NearbyResourceAmount++;     
                }
                if (NearbyResourceAmount >= ResourceData.MaxResourceAmount)
                {
                    break;
                }
                
            }
        }

        return NearbyResourceAmount;
    }
    private ResourceGeneratorData ResourceData;
    private float TimerMax;
    private float Timer;

    private void Awake()
    {
        ResourceData = GetComponent<BuildingTypeHolder>().BuildingType.RGeneratorData;
        TimerMax = ResourceData.TimerMax;
        Timer = TimerMax;
    }

    private void Start()
    {
        int NearbyResourceAmount = GetNearbyResourceAmount(ResourceData, transform.position);

        if (NearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            TimerMax = (ResourceData.TimerMax / 2.0f) + ResourceData.TimerMax *
                (1 - (float) NearbyResourceAmount / ResourceData.MaxResourceAmount);
        }
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0.0f)
        {
            Timer = TimerMax;
            ResourceManager.Instance.AddResource(ResourceData.ResourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return ResourceData;
    }

    public float GetTimerNormalized()
    {
        return Timer / TimerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / TimerMax;
    }
}
