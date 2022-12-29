using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Object Pooling :
/// 필요한 객체들을 Pool에 미리 생성해놓고 필요할때마다 가져다쓰고 파괴해야할 경우에는 파괴하지않고 비활성화해서 Pool 에 반납
/// 이렇게 하는 이유는 객체가 파괴될때 가비지컬렉션이 일어나고, 가비지컬렉션은 프레임드롭을 유발할 수 있기 때문.
/// </summary>
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

    [Serializable]
    public struct Element
    {
        public string Name;
        public GameObject Prefab;
        public int Num;

        public Element(string name, GameObject prefab, int num)
        {
            Name = name;
            Prefab = prefab;
            Num = num;
        }
    }

    private List<Element> _elements = new List<Element>();
    private Dictionary<string, Queue<GameObject>> _queueDictionary = new Dictionary<string, Queue<GameObject>>();

    public void AddElement(Element element) => _elements.Add(element);

    /// <summary>
    /// 등록되어있는 모든 element 들을 생성하는 함수
    /// 해당 element 에 대한 대기열이 없으면 생성해서 대기열에 생성된 각 element들을 등록해준다.
    /// </summary>
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

    /// <summary>
    /// 미리 생성되어있던 객체를 특정 위치에 소환하는 함수 (객체를 풀에서 꺼내쓰는 함수)
    /// 만약 대기열이 비어있으면 새로 생성을 해서라도 소환해줌.
    /// </summary>
    public GameObject Spawn(string name, Vector3 pos)
    {
        if (_queueDictionary.ContainsKey(name) == false)
            return null;

        // 대기열이 비어있으면 (더이상 소환할 수 있는 객체가 없을 경우) 추가로 생성
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

    public GameObject Spawn(string name, Vector3 pos, Quaternion rotation)
    {
        if (_queueDictionary.ContainsKey(name) == false)
            return null;

        // 대기열이 비어있으면 (더이상 소환할 수 있는 객체가 없을 경우) 추가로 생성
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
        go.transform.rotation = rotation;
        go.SetActive(true);
        return go;
    }

    /// <summary>
    /// 다쓴 객체를 Pool 에 반납하는 함수
    /// </summary>
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
        RearrangeSiblings(go);
    }

    public void  Return(GameObject go, float delay)
    {
        StartCoroutine(E_Return(go, delay));
    }

    IEnumerator E_Return(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(go);
    }

    private GameObject InstantiateElement(Element element)
    {
        GameObject go = Instantiate(element.Prefab, transform);
        go.SetActive(false);
        go.name = element.Name;
        _queueDictionary[element.Name].Enqueue(go);
        RearrangeSiblings(go);
        return go;
    }

    private void RearrangeSiblings(GameObject go)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == go.name)
            {
                go.transform.SetSiblingIndex(i);
                return;
            }
        }
        go.transform.SetAsLastSibling();
    }
}

