using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float ShootTimerMax;

    private float ShootTimer;
    private Enemy TargetEnemy;
    private Vector3 ArrowSpawnPosition;
    private float LookForTargetsTimer;
    private float LookForTargetsTimerMax = 0.2f;
    

    private void Awake()
    {
        ArrowSpawnPosition = transform.Find("ArrowSpawnPosition").position; 
    }
    private void Start()
    {
        LookForTargetsTimer = Random.Range(0f, LookForTargetsTimerMax);
        ShootTimer = ShootTimerMax;
    }
    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleShooting()
    {
        ShootTimer -= Time.deltaTime;
        if (ShootTimer <= 0f)
        {
            ShootTimer = ShootTimerMax;
            if (TargetEnemy != null)
            {
                ArrowProjectile.CreateArrowProjectile(ArrowSpawnPosition, TargetEnemy);
            }
        }
        
        
    }

    private void HandleTargeting()
    {
        LookForTargetsTimer -= Time.deltaTime;
        if (LookForTargetsTimer <= 0)
        {
            LookForTargetsTimer = Random.Range(0f, LookForTargetsTimerMax);
            LookForTargets();
        }
    }

    private void LookForTargets()
    {
        float TargetMaxRadius = 20f;
        Collider2D[] Collider2DArray = Physics2D.OverlapCircleAll(transform.position, TargetMaxRadius);

        foreach (Collider2D Target in Collider2DArray)
        {
            Enemy CollidedEnemy = Target.GetComponent<Enemy>();

            if (CollidedEnemy != null)
            {
                if (TargetEnemy == null)
                {
                    TargetEnemy = CollidedEnemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, CollidedEnemy.transform.position) <
                        Vector3.Distance(transform.position, TargetEnemy.transform.position))
                    {
                        TargetEnemy = CollidedEnemy;
                    }
                }
            }
        }
    }
}
