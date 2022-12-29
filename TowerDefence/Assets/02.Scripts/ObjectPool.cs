using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Object Pooling :
/// �ʿ��� ��ü���� Pool�� �̸� �����س��� �ʿ��Ҷ����� �����پ��� �ı��ؾ��� ��쿡�� �ı������ʰ� ��Ȱ��ȭ�ؼ� Pool �� �ݳ�
/// �̷��� �ϴ� ������ ��ü�� �ı��ɶ� �������÷����� �Ͼ��, �������÷����� �����ӵ���� ������ �� �ֱ� ����.
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
    /// ��ϵǾ��ִ� ��� element ���� �����ϴ� �Լ�
    /// �ش� element �� ���� ��⿭�� ������ �����ؼ� ��⿭�� ������ �� element���� ������ش�.
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
    /// �̸� �����Ǿ��ִ� ��ü�� Ư�� ��ġ�� ��ȯ�ϴ� �Լ� (��ü�� Ǯ���� �������� �Լ�)
    /// ���� ��⿭�� ��������� ���� ������ �ؼ��� ��ȯ����.
    /// </summary>
    public GameObject Spawn(string name, Vector3 pos)
    {
        if (_queueDictionary.ContainsKey(name) == false)
            return null;

        // ��⿭�� ��������� (���̻� ��ȯ�� �� �ִ� ��ü�� ���� ���) �߰��� ����
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

        // ��⿭�� ��������� (���̻� ��ȯ�� �� �ִ� ��ü�� ���� ���) �߰��� ����
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
    /// �پ� ��ü�� Pool �� �ݳ��ϴ� �Լ�
    /// </summary>
    public void Return(GameObject go)
    {
        if (_queueDictionary.ContainsKey(go.name) == false)
        {
            Debug.LogError($"[ObjectPool] : {go.name} �̰� �� �����Ծ�? �� �̰� �������� ���µ�");
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

