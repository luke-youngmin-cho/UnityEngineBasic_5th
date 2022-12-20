using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class ObjectPoolRegister : MonoBehaviour
{
    [SerializeField] private List<Element> _elements;

    private void Awake()
    {
        foreach (Element element in _elements)
        {
            if (string.IsNullOrEmpty(element.Name))
                Instance.AddElement(new Element(element.Name, element.Prefab, element.Num));
            else
                Instance.AddElement(element);
        }
    }
}
