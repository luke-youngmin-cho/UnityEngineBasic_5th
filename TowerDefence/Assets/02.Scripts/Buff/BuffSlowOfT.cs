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
        Debug.Log($"[BuffSlow] : {subject} �� {_gain} ��ŭ ������, ���� �ӵ� : {subject.Speed}");
    }

    public void OnDeactive(T subject)
    {
        subject.Speed *= _gain;
        Debug.Log($"[BuffSlow] : {subject} �� ���ο� ����� ������. ���� �ӵ� : {subject.Speed}");
    }

    public void OnDuration(T subject)
    {
    }
}
