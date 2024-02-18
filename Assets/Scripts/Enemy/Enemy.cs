using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy CreateEnemy(Vector3 Position)
    {
        Transform EnemyTransform = Instantiate(GameAssets.Instance.pfEnemy, Position, Quaternion.identity);

        return EnemyTransform.GetComponent<Enemy>();
    }

    private Transform TargetBuildingTransform;
    private Rigidbody2D EnemyRigidBody;
    private HealthSystem EnemyHealthSystem;
    private float LookForTargetsTimer;
    private float LookForTargetsTimerMax = 0.2f;

    private void Start()
    {

        EnemyRigidBody = GetComponent<Rigidbody2D>();

        if(BuildingManager.Instance.GetHQBuilding() != null)
        {
            TargetBuildingTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
        
        EnemyHealthSystem = GetComponent<HealthSystem>();

        EnemyHealthSystem.OnDamaged += EnemyHealthSystem_OnDamaged;
        EnemyHealthSystem.OnDied += EnemyHealthSystem_OnDied;

        LookForTargetsTimer = Random.Range(0f, LookForTargetsTimerMax);

    }

    private void EnemyHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.EnemyHit);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
        ChromaticAberrationEffect.Instance.SetWeight(.5f);
    }

    private void EnemyHealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.EnemyDie);
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(.5f);
        Instantiate(GameAssets.Instance.pfEnemyDieParticles, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building CollidedBuilding = collision.gameObject.GetComponent<Building>();
        if (CollidedBuilding != null)
        {
            collision.gameObject.GetComponent<HealthSystem>().Damage(10);
            EnemyHealthSystem.Damage(999);
        }

    }

    private void HandleMovement()
    {
        if (TargetBuildingTransform != null)
        {
            Vector3 MoveDirection = (TargetBuildingTransform.position - transform.position).normalized;
            float MoveSpeed = 6.0f;
            EnemyRigidBody.velocity = MoveDirection * MoveSpeed;
        }
        else
        {
            EnemyRigidBody.velocity = Vector2.zero;
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
            Building TargetBuilding = Target.GetComponent<Building>();

            if (TargetBuilding != null)
            {
                if (TargetBuildingTransform == null)
                {
                    TargetBuildingTransform = TargetBuilding.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, TargetBuilding.transform.position) <
                        Vector3.Distance(transform.position, TargetBuildingTransform.position))
                    {
                        TargetBuildingTransform = TargetBuilding.transform;
                    }
                }
            }
        }

        if (TargetBuildingTransform == null)
        {
            if(BuildingManager.Instance.GetHQBuilding() != null)
            {
                TargetBuildingTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
            
        }
    }



}
