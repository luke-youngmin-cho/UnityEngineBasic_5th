using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRocketLauncher : TowerAttackerWithProjectile
{
    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < FirePoints.Length; i++)
        {
            GameObject rocket = ObjectPool.Instance.Spawn("Rocket", FirePoints[i].position);
            rocket.GetComponent<ProjectileRocket>().SetUp(false, 5.0f, Damage, TargetLayer, Target);
        }
    }
}
