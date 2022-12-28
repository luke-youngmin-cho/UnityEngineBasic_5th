using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where 제한자
// 타입을 특정하는 제한자
public class BuffBurning<T> : IBuff<T>
    where T : IDamageable
{
    private float _damage;
    private float _period;
    private float _timeMark;

    public BuffBurning(float damage, float period)
    {
        _damage = damage;
        _period = period;
    }

    public void OnActive(T subject)
    {
        _timeMark = Time.time;
    }

    public void OnDeactive(T subject)
    {
    }

    public void OnDuration(T subject)
    {
        if (Time.time - _timeMark > _period)
        {
            subject.Damage(_damage);
            _timeMark = Time.time;
        }
    }
}
