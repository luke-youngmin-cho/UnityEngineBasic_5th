using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<ObjectPool>("ObjectPool"));
            return _instance;
        }
    }

    public struct Element
    {
        public string Name;
        public GameObject Prefab;
        public int Num;
    }

    private List<Element> _elements = new List<Element>();
    private Dictionary<string, Queue<GameObject>> _queueDictionary = new Dictionary<string, Queue<GameObject>>();

    public void AddElement(Element element) => _elements.Add(element);

    public void InstantiateAllElements()
    {
        foreach (Element element in _elements)
        {
            if (_queueDictionary.ContainsKey(element.Name) == false)
                _queueDictionary.Add(element.Name, new Queue<GameObject>());

            for (int i = 0; i < element.Num; i++)
                InstantiateElement(element);
        }
    }

    public GameObject Spawn(string name, Vector3 pos)
    {
        if (_queueDictionary.ContainsKey(name) == false)
            return null;

        if (_queueDictionary[name].Count <= 0)
        {
            Element element = _elements.Find(e => e.Name == name);
            for (int i = 0; i < Math.Ceiling(Math.Log10(element.Num)); i++)
            {
                InstantiateElement(element);
            }
        }

        GameObject go = _queueDictionary[name].Dequeue();
        go.transform.SetParent(null);
        go.transform.position = pos;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject go)
    {
        if (_queueDictionary.ContainsKey(go.name) == false)
        {
            Debug.LogError($"[ObjectPool] : {go.name} 이거 왜 가져왔어? 나 이거 빌려준적 없는데");
            return;
        }

        go.transform.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        _queueDictionary[go.name].Enqueue(go);
        go.SetActive(false);
    }


    private GameObject InstantiateElement(Element element)
    {
        GameObject go = Instantiate(element.Prefab, transform);
        go.SetActive(false);
        go.name = element.Name;
        _queueDictionary[element.Name].Enqueue(go);
        return go;
    }
}

