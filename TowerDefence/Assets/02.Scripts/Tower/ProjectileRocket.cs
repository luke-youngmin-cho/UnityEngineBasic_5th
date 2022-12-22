using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRocket : Projectile
{
    [SerializeField] private float _explosionRange = 2.0f;
    protected override void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & TargetLayer) > 0 ||
            (1 << other.gameObject.layer & TouchLayer) > 0)
        {
            ExplosionEffect();

            Collider[] cols = Physics.OverlapSphere(other.transform.position, _explosionRange, TargetLayer);
            Enemy enemy;
            foreach (var col in cols)
            {
                enemy = col.GetComponent<Enemy>();
                enemy.Hp -= Damage * (1.0f - (Vector3.Distance(transform.position, col.transform.position) / _explosionRange));
            }

            ObjectPool.Instance.Return(gameObject);
        }
    }

    private void ExplosionEffect()
    {
        GameObject effect = ObjectPool.Instance.Spawn("RocketExplosionEffect",
                                                      transform.position,
                                                      Quaternion.LookRotation(Vector3.up));
        ParticleSystem particle = effect.GetComponent<ParticleSystem>();
        ObjectPool.Instance.Return(effect, particle.main.duration + particle.main.startLifetime.constantMax);
    }
}
