using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using ULB.RPG;
using ULB.RPG.DataDependencySources;
using ULB.RPG.DataModels;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsEquippedUI : MonoBehaviour
{
    [SerializeField] private List<ItemEquippedSlot> _slotList;
    public Dictionary<EquipType, ItemEquippedSlot> _slots = new Dictionary<EquipType, ItemEquippedSlot>();
    private ItemsEquippedPresenter _presenter;

    private void Awake()
    {        
        _presenter = new ItemsEquippedPresenter();

        foreach (var slot in _slotList)
        {
            _slots.Add(slot.equipType, slot);
            slot.onRightClicked += (equipType, itemID) =>
            {
                if (_presenter.unequipCommand.TryExecute(equipType, itemID) == false)
                    throw new System.Exception($"[ItemEquippedSlot] : Failed to unequip {equipType} id : {itemID}");
            };
        }

        foreach (int itemID in _presenter.source)
        {
            SetSlot(itemID);
        }

        _presenter.source.OnItemAdded += (equipType, itemID) =>
        {
            SetSlot((EquipType)equipType, itemID);
        };
        _presenter.source.OnItemRemoved += (equipType, itmeID) =>
        {
            
        };
        _presenter.source.OnItemChanged += (equipType, itemID) =>
        {
            SetSlot((EquipType)equipType, itemID);
        };
    }
    private void SetSlot(EquipType equipType, int itemID)
    {
        if (itemID < 0)
        {
            _slots[equipType].Clear();
            return;
        }

        switch (equipType)
        {
            case EquipType.None:
                break;
            case EquipType.RightHandWeapon:
                {
                    _slots[EquipType.RightHandWeapon].Set(itemID, true);
                }
                break;
            case EquipType.LeftHandWeapon:
                {
                    _slots[EquipType.LeftHandWeapon].Set(itemID, true);
                }
                break;
            case EquipType.DoubleHandWeapon:
                {
                    _slots[EquipType.RightHandWeapon].Set(itemID, true);
                    _slots[EquipType.LeftHandWeapon].Set(itemID, false);
                }
                break;
            case EquipType.Head:
                break;
            case EquipType.Top:
                break;
            case EquipType.Bottom:
                break;
            case EquipType.Dress:
                break;
            case EquipType.Feet:
                break;
            case EquipType.Ring:
                break;
            case EquipType.Necklace:
                break;
            default:
                break;
        }
    }

    private void SetSlot(int itemID)
    {
        if (itemID < 0)
            return;

        switch (((Equipment)ItemInfoAssets.instance[itemID].prefab).type)
        {
            case EquipType.None:
                break;
            case EquipType.RightHandWeapon:
                {
                    _slots[EquipType.RightHandWeapon].Set(itemID, true);
                }
                break;
            case EquipType.LeftHandWeapon:
                {
                    _slots[EquipType.LeftHandWeapon].Set(itemID, true);
                }
                break;
            case EquipType.DoubleHandWeapon:
                {
                    _slots[EquipType.RightHandWeapon].Set(itemID, true);
                    _slots[EquipType.LeftHandWeapon].Set(itemID, false);
                }
                break;
            case EquipType.Head:
                break;
            case EquipType.Top:
                break;
            case EquipType.Bottom:
                break;
            case EquipType.Dress:
                break;
            case EquipType.Feet:
                break;
            case EquipType.Ring:
                break;
            case EquipType.Necklace:
                break;
            default:
                break;
        }
    }
}
