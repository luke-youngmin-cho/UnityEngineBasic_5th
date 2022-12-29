using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSlow<T> : IBuff<T>
    where T : ISpeed
{
    private float _gain;

    public BuffSlow(float gain)
    {
        _gain = gain;
    }   

    public void OnActive(T subject)
    {
        subject.Speed /= _gain;
        Debug.Log($"[BuffSlow] : {subject} 가 {_gain} 만큼 느려짐, 현재 속도 : {subject.Speed}");
    }

    public void OnDeactive(T subject)
    {
        subject.Speed *= _gain;
        Debug.Log($"[BuffSlow] : {subject} 의 슬로우 디버프 해제됨. 현재 속도 : {subject.Speed}");
    }

    public void OnDuration(T subject)
    {
    }
}
