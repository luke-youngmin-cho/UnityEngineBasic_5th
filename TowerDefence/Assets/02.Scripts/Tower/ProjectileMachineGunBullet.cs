using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMachineGunBullet : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & TargetLayer) > 0)
        {
            ExplosionEffect();
            other.gameObject.GetComponent<Enemy>().Hp -= Damage;
            ObjectPool.Instance.Return(gameObject);
        }
        else if ((1 << other.gameObject.layer & TouchLayer) > 0)
        {
            ExplosionEffect();
            ObjectPool.Instance.Return(gameObject);
        }
    }

    private void ExplosionEffect()
    {
        GameObject effect = ObjectPool.Instance.Spawn("MachineGunBulletExplosionEffect",
                                                      transform.position,
                                                      Quaternion.LookRotation(transform.position - Target.position));
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        ObjectPool.Instance.Return(effect, particle.main.duration + particle.main.startLifetime.constantMax);
    }
}
