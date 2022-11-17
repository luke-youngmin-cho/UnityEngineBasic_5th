using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNepenthesSeed : Projectile
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _targetLayer;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (1<<other.gameObject.layer == _targetLayer)
        {
            // todo -> damage player
            Destroy(gameObject);
        }
    }
}
