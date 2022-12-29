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
            Debug.Log($"[BuffBuring] : {subject} 가 불타오르고 있습니다. 현재 체력 {subject.Hp}");
            _timeMark = Time.time;
        }
    }
}
