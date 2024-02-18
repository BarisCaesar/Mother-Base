using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private HealthSystem BuildingHealthSystem;
    [SerializeField] private ResourceTypeSO GoldResourceType;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int MissingHealth = BuildingHealthSystem.GetHealthAmountMax() - BuildingHealthSystem.GetHealthAmount();
            int RepairCost = MissingHealth / 2;

            ResourceAmount[] RepairCostArray = new ResourceAmount[] 
            {
                new ResourceAmount{ ResourceType = GoldResourceType, Amount = RepairCost } 
            };
            if(ResourceManager.Instance.CanAffordBuilding(RepairCostArray))
            {
                ResourceManager.Instance.SpendResources(RepairCostArray);
                BuildingHealthSystem.HealFull();
            }
            else
            {
                TooltipUI.Instance.Show("Cannot Afford Repair Cost!", new TooltipUI.TooltipTimer { Count = 2f});
            }
        });
    }
}
