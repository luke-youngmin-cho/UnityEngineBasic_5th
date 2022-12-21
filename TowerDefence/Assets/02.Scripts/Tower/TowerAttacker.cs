using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerAttacker : Tower
{
    [SerializeField] protected float Damage;
    protected abstract void Attack();
}
