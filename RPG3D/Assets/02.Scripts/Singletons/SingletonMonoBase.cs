using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBase<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).Name;
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }
    public static T _instance;
}