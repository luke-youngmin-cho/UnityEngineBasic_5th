using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBase<T> : MonoBehaviour
    where T : SingletonMonoBase<T>
{
    public static T instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    _instance.Init();
                }
            }
            
            return _instance;
        }
    }
    public static T _instance;
    private static volatile object _lock = new object();

    protected virtual void Init() { }
}