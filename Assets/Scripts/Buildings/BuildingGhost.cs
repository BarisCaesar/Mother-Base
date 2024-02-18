using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject SpriteGameObject;
    private ResourceNearbyOverlay CurrentResourceNearbyOverlay;

    private void Awake()
    {
        SpriteGameObject = transform.Find("Sprite").gameObject;
        CurrentResourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();

    }

    private void Show(Sprite GhostSprite)
    {
        SpriteGameObject.SetActive(true);
        SpriteGameObject.GetComponent<SpriteRenderer>().sprite = GhostSprite;
    }

    private void Hide()
    {
        SpriteGameObject.SetActive(false);
    }
    
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.ActiveBuildingType != null)
        {
            Show(e.ActiveBuildingType.BuildingSprite);

            if (e.ActiveBuildingType.HasResourceGeneratorData)
            {
                CurrentResourceNearbyOverlay.Show(e.ActiveBuildingType.RGeneratorData);
            }
            else
            {
                CurrentResourceNearbyOverlay.Hide();
            }
            
        }
        else
        {
            Hide();
            CurrentResourceNearbyOverlay.Hide();
        }
           
    }
    
}
