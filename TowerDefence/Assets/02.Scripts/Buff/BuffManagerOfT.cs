using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager<T>
    where T : MonoBehaviour
{
    private T _subject;
    private Dictionary<IBuff<T>, Coroutine> _buffs;

    public BuffManager(T subject)
    {
        _subject = subject;
        _buffs = new Dictionary<IBuff<T>, Coroutine>();
    }

    public void ActiveBuff(IBuff<T> buff, float duration)
    {
        if (_subject.gameObject.activeSelf == false)
            return;

        _buffs.Add(buff,_subject.StartCoroutine(E_ActiveBuff(buff, duration)));
    }

    IEnumerator E_ActiveBuff(IBuff<T> buff, float duration)
    {
        buff.OnActive(_subject);
        float timeMark = Time.time;
        while (Time.time - timeMark < duration)
        {
            buff.OnDuration(_subject);
            yield return null;
        }
        buff.OnDeactive(_subject);
        _buffs.Remove(buff);
    }

    public void ActiveBuff(IBuff<T> buff, Func<bool> deactiveCondition)
    {
        if (_subject.gameObject.activeSelf == false)
            return;

        if (deactiveCondition == null)
            return;

        _buffs.Add(buff, _subject.StartCoroutine(E_ActiveBuff(buff, deactiveCondition)));
    }

    IEnumerator E_ActiveBuff(IBuff<T> buff, Func<bool> deactiveCondition)
    {
        buff.OnActive(_subject);
        while (deactiveCondition.Invoke() == false)
        {
            buff.OnDuration(_subject);
            yield return null;
        }
        buff.OnDeactive(_subject);
        _buffs.Remove(buff);
    }

    public void DeactiveAllBuffs()
    {
        foreach (KeyValuePair<IBuff<T>, Coroutine> buffPair in _buffs)
        {
            if (buffPair.Value != null)
                _subject.StopCoroutine(buffPair.Value);
        }
        _buffs.Clear();
    }
}
