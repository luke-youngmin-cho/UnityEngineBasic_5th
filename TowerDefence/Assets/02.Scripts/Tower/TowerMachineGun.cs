using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMachineGun : TowerAttackerWithProjectile
{
    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < FirePoints.Length; i++)
        {
            GameObject bullet = ObjectPool.Instance.Spawn("MachineGunBullet", FirePoints[i].position);
            bullet.GetComponent<ProjectileMachineGunBullet>().SetUp(true, 10.0f, Damage, TargetLayer, Target);
        }
    }
}
