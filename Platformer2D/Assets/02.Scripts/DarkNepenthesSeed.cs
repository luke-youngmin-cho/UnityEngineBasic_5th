using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNepenthesSeed : Projectile
{
    [SerializeField] private int _damage = 30;
    [SerializeField] private LayerMask _targetLayer;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (1 << collision.gameObject.layer == _targetLayer)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.Invincible == false)
            {
                player.Hurt(_damage, false);
                player.Knockback();
            }
        }
    }
}
