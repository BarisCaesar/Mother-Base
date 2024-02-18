using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Building BuildingObject;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingTypeSO BuildingType = BuildingObject.GetComponent<BuildingTypeHolder>().BuildingType;
            foreach(ResourceAmount RequiredAmount in BuildingType.ConstructionResourceCostArray)
            {
                ResourceManager.Instance.AddResource(RequiredAmount.ResourceType, Mathf.FloorToInt(RequiredAmount.Amount * .6f));
            }

            Destroy(BuildingObject.gameObject);
        });
    }
}
