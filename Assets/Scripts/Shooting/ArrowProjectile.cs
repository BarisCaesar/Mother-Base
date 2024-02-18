using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public static ArrowProjectile CreateArrowProjectile(Vector3 Position, Enemy Target)
    {
        Transform ArrowProjectileTransform = Instantiate(GameAssets.Instance.pfArrowProjectile, Position, Quaternion.identity);

        ArrowProjectile Projectile = ArrowProjectileTransform.GetComponent<ArrowProjectile>();

        Projectile.SetTarget(Target);

        return Projectile;
    }

    private Enemy TargetEnemy;
    private Vector3 LastMoveDirection;
    private float ProjectileLiveTime = 2f;

    private void Update()
    {
        Vector3 MoveDirection;
        if(TargetEnemy != null)
        {
            MoveDirection = (TargetEnemy.transform.position - transform.position).normalized;
            LastMoveDirection = MoveDirection;
        }
        else
        {
            MoveDirection = LastMoveDirection;
        }

        float MoveSpeed = 20f;
        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(MoveDirection));

        ProjectileLiveTime -= Time.deltaTime;
        if(ProjectileLiveTime <= 0) 
        {
            Destroy(gameObject);
        }

    }
    private void SetTarget(Enemy TargetEnemy)
    {
        this.TargetEnemy = TargetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy CollidedEnemy = collision.GetComponent<Enemy>();

        if(CollidedEnemy != null )
        {
            int DamageAmount = 10;
            CollidedEnemy.GetComponent<HealthSystem>().Damage(DamageAmount);
            Destroy(gameObject);
        }
    }
}
