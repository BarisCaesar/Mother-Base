using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string NameString;
    public Transform Prefab;
    public bool HasResourceGeneratorData;
    public Sprite BuildingSprite;
    public ResourceGeneratorData RGeneratorData;
    public float MinConstructionRadius;
    public ResourceAmount[] ConstructionResourceCostArray;
    public int HealthAmountMax;
    public float ConstructionTimerMax;

    public string GetConstructionResourceCostString()
    {
       string Str = "";
       foreach (ResourceAmount ResourceCost in ConstructionResourceCostArray)
       {
           Str += "<color=#" + ResourceCost.ResourceType.ColorValueHex + ">" + ResourceCost.ResourceType.NameShort + 
               ResourceCost.Amount + 
                 "</color> ";
       }

       return Str;
    }
}
