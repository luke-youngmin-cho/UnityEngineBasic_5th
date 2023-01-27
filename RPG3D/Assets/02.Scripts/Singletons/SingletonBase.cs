using System; 
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Unity.VisualScripting;

public class SingletonBase<T>
    where T : SingletonBase<T>
{
    public static T instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    //ConstructorInfo constructorInfo = typeof(T).GetConstructor(new Type[] { });
                    //_instance = (T)constructorInfo.Invoke(new object[] { });

                    _instance = (T)Activator.CreateInstance(typeof(T));

                    Debug.Log($"{_instance} is created");
                }
            }
            return _instance;
        }
    }
    private static T _instance;
    private static volatile object _lock = new object();
}

public class Test : SingletonBase<Test>
{

}
