using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using ULB.RPG;
using ULB.RPG.DataDependencySources;
using ULB.RPG.DataModels;
using ULB.RPG.UISystems;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemsEquippedUI : MonoBehaviour, IUI
{
    [SerializeField] private List<ItemEquippedSlot> _slotList;
    public Dictionary<EquipType, ItemEquippedSlot> _slots = new Dictionary<EquipType, ItemEquippedSlot>();
    private ItemsEquippedPresenter _presenter;

    public Canvas canvas => _canvas;
    private Canvas _canvas;
    public event Action OnShow;
    public event Action OnHide;
    public void Hide()
    {
        gameObject.SetActive(false);
        OnHide?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnShow?.Invoke();
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        CanvasManager.instance.Register(this);
        _presenter = new ItemsEquippedPresenter();

        // 장착한 아이템 슬롯에 오른쪽 클릭시 이벤트 등록
        foreach (var slot in _slotList)
        {
            _slots.Add(slot.equipType, slot);
            slot.onRightClicked += (equipType, itemID) =>
            {
                if (_presenter.unequipCommand.TryExecute(((Equipment)ItemInfoAssets.instance[itemID].prefab).type, itemID) == false)
                    throw new System.Exception($"[ItemEquippedSlot] : Failed to unequip {equipType} id : {itemID}");
            };
        }

        // 오른손무기 슬롯은 특별히 양손무기 장착중이었으면 장착해제할 때 왼손도 초기화
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

    private void Start()
    {
        StandaloneInputModuleWrapper.main.RegisterGraphicRaycaster(GetComponent<GraphicRaycaster>());
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
