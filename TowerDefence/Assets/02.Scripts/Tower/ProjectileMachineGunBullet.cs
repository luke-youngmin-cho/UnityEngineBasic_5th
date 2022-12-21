using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMachineGunBullet : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & TargetLayer) > 0)
        {
            other.gameObject.GetComponent<Enemy>().Hp -= Damage;
            ObjectPool.Instance.Return(gameObject);
        }
        else if ((1 << other.gameObject.layer & TouchLayer) > 0)
        {
            ObjectPool.Instance.Return(gameObject);
        }
    }
}
