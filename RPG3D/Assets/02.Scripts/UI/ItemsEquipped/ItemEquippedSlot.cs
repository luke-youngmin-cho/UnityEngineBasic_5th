using System.Collections;
using System.Collections.Generic;
using TMPro;
using ULB.RPG.DataModels;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquippedSlot : MonoBehaviour
{
    public enum EquipType
    {
        None,
        RightHandWeapon,
        LeftHandWeapon,
        Head,
        Top,
        Bottom,
        Feet,
        Ring,
        Necklace
    }
    public EquipType equipType;
    [SerializeField] private Image _icon;

    public void Set(int itemID)
    {
        if (itemID >= 0)
        {
            _icon.sprite = ItemInfoAssets.instance[itemID].icon;
        }
    }

    public void Clear()
    {
        _icon.sprite = null;
    }
}
