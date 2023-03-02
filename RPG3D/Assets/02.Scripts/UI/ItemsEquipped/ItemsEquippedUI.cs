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

        // ������ ������ ���Կ� ������ Ŭ���� �̺�Ʈ ���
        foreach (var slot in _slotList)
        {
            _slots.Add(slot.equipType, slot);
            slot.onRightClicked += (equipType, itemID) =>
            {
                if (_presenter.unequipCommand.TryExecute(((Equipment)ItemInfoAssets.instance[itemID].prefab).type, itemID) == false)
                    throw new System.Exception($"[ItemEquippedSlot] : Failed to unequip {equipType} id : {itemID}");
            };
        }

        // �����չ��� ������ Ư���� ��չ��� �������̾����� ���������� �� �޼յ� �ʱ�ȭ
        //_slots[EquipType.RightHandWeapon].onRightClicked += (equipType, itemID) =>
        //{
        //    if (((Equipment)ItemInfoAssets.instance[itemID].prefab).type == EquipType.DoubleHandWeapon)
        //    {
        //        _slots[EquipType.LeftHandWeapon].Clear();
        //    }
        //};

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
            ClearSlot(equipType);
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

    private void ClearSlot(EquipType equipType)
    {
        switch (equipType)
        {
            case EquipType.None:
                break;
            case EquipType.DoubleHandWeapon:
                {
                    _slots[EquipType.RightHandWeapon].Clear();
                    _slots[EquipType.LeftHandWeapon].Clear();
                }
                break;
            case EquipType.RightHandWeapon:
            case EquipType.LeftHandWeapon:
            case EquipType.Head:
            case EquipType.Top:
            case EquipType.Bottom:
            case EquipType.Dress:
            case EquipType.Feet:
            case EquipType.Ring:
            case EquipType.Necklace:
            default:
                {
                    _slots[equipType].Clear();
                }
                break;
        }
    }
}
