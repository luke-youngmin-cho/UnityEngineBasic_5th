using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using ULB.RPG.DataModels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemEquippedSlot : MonoBehaviour, IPointerClickHandler
{
    public EquipType equipType;
    [SerializeField] private Image _icon;
    private Color _origin;
    public event Action<EquipType, int> onRightClicked;
    private int _itemID;
    private bool _active = true;

    public void Set(int itemID, bool active)
    {
        if (itemID >= 0)
        {
            _icon.sprite = ItemInfoAssets.instance[itemID].icon;
            _icon.color = active ? _origin : Color.white * 0.5f;
        }
        else
        {
            _icon.sprite = null;
        }

        _itemID = itemID;
        _active = active;
    }

    public void Clear()
    {
        _icon.sprite = null;
        _itemID = -1;
        _active = true;
    }

    private void Awake()
    {
        _origin = _icon.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_active &&
            eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClicked?.Invoke(equipType, _itemID);
        }
    }
}
