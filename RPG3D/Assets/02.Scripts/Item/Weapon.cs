using System.Collections;
using System.Collections.Generic;
using ULB.RPG;
using UnityEngine;
using UnityEngine.Experimental.AI;

[RequireComponent(typeof(BoxCollider))]
public class Weapon : Equipment
{
    public enum WeaponType
    {
        None,
        BareHand,
        Sythe,
    }
    public WeaponType weaponType;
    public bool doCast
    {
        get
        {
            return _doCast;
        }
        set
        {
            if (value)
                targetsCasted.Clear();

            _castBound.enabled = value;
            _doCast = value;
        }
    }
    private bool _doCast;
    public Dictionary<int, IDamageable> targetsCasted = new Dictionary<int, IDamageable>();
    private BoxCollider _castBound;
    private LayerMask _damageableMask;

    private void Awake()
    {
        _castBound = GetComponent<BoxCollider>();
        _damageableMask = (1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Enemy"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doCast == false)
            return;

        if ((1 << other.gameObject.layer & _damageableMask) > 0 &&
            targetsCasted.ContainsKey(other.gameObject.GetInstanceID()) == false)
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                targetsCasted.Add(other.gameObject.GetInstanceID(), damageable);
            }
        }
    }
}