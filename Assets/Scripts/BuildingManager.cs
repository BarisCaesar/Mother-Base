using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    
    private Camera MainCamera;
    private BuildingTypeSO ActiveBuildingType;
    private BuildingTypeListSO BuildingTypeList;

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO ActiveBuildingType;
    }
    private void Awake()
    {
        Instance = this;
        
        BuildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }

    private void Start()
    {
        MainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (ActiveBuildingType != null)
            {
                if(CanSpawnBuilding(ActiveBuildingType, UtilsClass.GetMouseWorldPosition(), out string ErrorMessage))
                {
                    if (ResourceManager.Instance.CanAffordBuilding(ActiveBuildingType.ConstructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(ActiveBuildingType.ConstructionResourceCostArray);
                        Instantiate(ActiveBuildingType.Prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);  
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Cannot afford " + ActiveBuildingType.GetConstructionResourceCostString(), new TooltipUI.TooltipTimer{Count = 2.0f});    
                    }
                   
                }
                else
                {
                    TooltipUI.Instance.Show(ErrorMessage, new TooltipUI.TooltipTimer {Count = 2.0f});
                }
            }
        }

    }

    private bool CanSpawnBuilding(BuildingTypeSO BuildingType, Vector3 Position, out string ErrorMessage)
    { 
        BoxCollider2D BuildingCollider = BuildingType.Prefab.GetComponent<BoxCollider2D>();
        Collider2D[] Collider2DArray = Physics2D.OverlapBoxAll(Position + (Vector3)BuildingCollider.offset, BuildingCollider.size, 0);

        bool IsAreaClear = Collider2DArray.Length == 0;
        if (!IsAreaClear)
        {
            ErrorMessage = "Area is not clear";
            return false;
        }

        Collider2DArray = Physics2D.OverlapCircleAll(Position, BuildingType.MinConstructionRadius);
        foreach (Collider2D ObjectCollider in Collider2DArray)
        {
            BuildingTypeHolder CollidedBuilding = ObjectCollider.GetComponent<BuildingTypeHolder>();

            if (CollidedBuilding != null)
            {
                if (CollidedBuilding.BuildingType == BuildingType)
                {
                    ErrorMessage = "Too close to another building of same type";
                    return false;
                }
            }
        }

        float MaxConstructionRadius = 25.0f;
        
        Collider2DArray = Physics2D.OverlapCircleAll(Position, MaxConstructionRadius);
        foreach (Collider2D ObjectCollider in Collider2DArray)
        {
            BuildingTypeHolder CollidedBuilding = ObjectCollider.GetComponent<BuildingTypeHolder>();

            if (CollidedBuilding != null)
            {
                ErrorMessage = "";
                return true;
            }
        }

        ErrorMessage = "Too far from any other building";

        return false;
    }
    
    public void SetActiveBuildingType(BuildingTypeSO BuildingType)
    {
        ActiveBuildingType = BuildingType;

        if (OnActiveBuildingTypeChanged != null)
        {
            OnActiveBuildingTypeChanged(this, new OnActiveBuildingTypeChangedEventArgs{ActiveBuildingType = this.ActiveBuildingType});
        }
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return ActiveBuildingType;
    }
    
}
