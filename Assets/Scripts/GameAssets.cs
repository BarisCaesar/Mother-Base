using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets PrivateInstance;

    public static GameAssets Instance { 
        get 
        {
            if(PrivateInstance == null)
            {
                PrivateInstance = Resources.Load<GameAssets>("GameAssets");
            }
            return PrivateInstance;
        }
    }

    public Transform pfEnemy;
    public Transform pfEnemyDieParticles;
    public Transform pfArrowProjectile;
    public Transform pfBuildingConstruction;
    public Transform pfBuildingPlacedParticles;
    public Transform pfBuildingDestroyedParticles;
}
