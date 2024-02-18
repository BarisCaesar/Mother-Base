using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction CreateBuildingConstruction(Vector3 Position, BuildingTypeSO BuildingType)
    {
        Transform BuildingConstructionTransform = Instantiate(GameAssets.Instance.pfBuildingConstruction, Position, Quaternion.identity);

        BuildingConstruction BuildingConstructionObject = BuildingConstructionTransform.GetComponent<BuildingConstruction>();

        BuildingConstructionObject.SetBuildingType(BuildingType);
    

        return BuildingConstructionObject;
    }

    private float ConstructionTimerMax;
    private float ConstructionTimer;
    private BuildingTypeSO BuildingType;
    private BoxCollider2D Collider;
    private BuildingTypeHolder BuildingTypeHolderObject;
    private Material BuildingConstructionMaterial;

    private void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        BuildingTypeHolderObject = GetComponent<BuildingTypeHolder>();
        BuildingConstructionMaterial = transform.Find("Sprite").GetComponent<SpriteRenderer>().material;

        Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        ConstructionTimer -= Time.deltaTime;

        BuildingConstructionMaterial.SetFloat("_Progress", GetProgressNormalized());

        if(ConstructionTimer <= 0)
        {
            Instantiate(BuildingType.Prefab, transform.position, Quaternion.identity);
            Instantiate(GameAssets.Instance.pfBuildingPlacedParticles, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.SoundType.BuildingPlaced);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO BuildingType)
    {
        this.BuildingType = BuildingType;
        this.BuildingTypeHolderObject.BuildingType = BuildingType;    
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = BuildingType.BuildingSprite;

        this.ConstructionTimerMax = BuildingType.ConstructionTimerMax;
        ConstructionTimer = ConstructionTimerMax;

        Collider.offset = BuildingType.Prefab.GetComponent<BoxCollider2D>().offset;
        Collider.size = BuildingType.Prefab.GetComponent<BoxCollider2D>().size;
    }

    public float GetProgressNormalized()
    {
        return 1f - (ConstructionTimer / ConstructionTimerMax);
    }

}
