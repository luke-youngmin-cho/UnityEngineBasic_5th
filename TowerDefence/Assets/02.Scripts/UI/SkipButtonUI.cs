using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkipButtonUI : MonoBehaviour
{
    [SerializeField] private Button _button;

    public void AddListener(UnityAction action) => _button.onClick.AddListener(action);
    public void RemoveAllListener() => _button.onClick.RemoveAllListeners();
}
