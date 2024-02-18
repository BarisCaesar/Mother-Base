using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingTypeSO BuildingType;
    private HealthSystem BuildingHealthSystem;
    private Transform BuildingDemolishButton;
    private Transform BuildingRepairButton;

    private void Awake()
    {
        BuildingDemolishButton = transform.Find("pfBuildingDemolishButton");
        if (BuildingDemolishButton != null)
        {
            BuildingDemolishButton.gameObject.SetActive(false);
        }

        BuildingRepairButton = transform.Find("pfBuildingRepairButton");
        if (BuildingRepairButton != null)
        {
            BuildingRepairButton.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        BuildingType = GetComponent<BuildingTypeHolder>().BuildingType;

        BuildingHealthSystem = GetComponent<HealthSystem>();

        BuildingHealthSystem.SetMaxHealth(BuildingType.HealthAmountMax, true);
        BuildingHealthSystem.OnDamaged += BuildingHealthSystem_OnDamaged;
        BuildingHealthSystem.OnHealed += BuildingHealthSystem_OnHealed;

        BuildingHealthSystem.OnDied += BuildingHealthSystem_OnDied;

    }

    private void BuildingHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if(BuildingHealthSystem.IsFullHealth())
        {
            HideBuildingRepairButton();
        }
    }

    private void BuildingHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.BuildingDamaged);
        ShowBuildingRepairButton();
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    private void BuildingHealthSystem_OnDied(object sender, System.EventArgs e)
    {
        if(BuildingType.NameString == "HQ")
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.GameOver);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.BuildingDestroyed);
        }

        Instantiate(GameAssets.Instance.pfBuildingDestroyedParticles, transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(10f, .2f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);

        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        ShowBuildingDemolishButton();
    }

    private void OnMouseExit()
    {
        HideBuildingDemolishButton();
    }

    private void ShowBuildingDemolishButton()
    {
        if (BuildingDemolishButton != null)
        {
            BuildingDemolishButton.gameObject.SetActive(true);
        }
    }
    private void HideBuildingDemolishButton()
    {
        if (BuildingDemolishButton != null)
        {
            BuildingDemolishButton.gameObject.SetActive(false);

        }

    }

    private void ShowBuildingRepairButton()
    {
        if (BuildingRepairButton != null)
        {
            BuildingRepairButton.gameObject.SetActive(true);
        }
    }
    private void HideBuildingRepairButton()
    {
        if (BuildingRepairButton != null)
        {
            BuildingRepairButton.gameObject.SetActive(false);

        }

    }
}
