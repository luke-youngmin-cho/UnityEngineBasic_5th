using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using ULB.RPG.DataDependencySources;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsEquippedUI : MonoBehaviour
{
    [SerializeField] private List<ItemEquippedSlot> _slotList;
    public Dictionary<ItemEquippedSlot.EquipType, ItemEquippedSlot> _slots = new Dictionary<ItemEquippedSlot.EquipType, ItemEquippedSlot>();
    private ItemsEquippedPresenter _presenter;

    private void Awake()
    {
        foreach (var slot in _slotList)
        {
            _slots.Add(slot.equipType, slot);
        }
        _presenter = new ItemsEquippedPresenter();
        _presenter.source.OnItemAdded += (equipType, itemID) =>
        {
            _slots[(ItemEquippedSlot.EquipType)equipType].Set(itemID);
        };
        _presenter.source.OnItemRemoved += (equipType, itmeID) =>
        {
            _slots[(ItemEquippedSlot.EquipType)equipType].Clear();
        };
        _presenter.source.OnItemChanged += (equipType, itemID) =>
        {
            _slots[(ItemEquippedSlot.EquipType)equipType].Set(itemID);
        };
    }
}
