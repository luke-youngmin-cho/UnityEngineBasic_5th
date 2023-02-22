using System.Collections;
using System.Collections.Generic;
using ULB.RPG.DataDependencySources;
using UnityEngine;

namespace ULB.RPG.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private InventorySlot _slotPrefab;
        private List<InventorySlot> _slots;
        private InventoryPresenter _presenter;

        private void Awake()
        {
            _presenter = new InventoryPresenter();

            _slots = new List<InventorySlot>();
            for (int i = 0; i < _presenter.source.Count; i++)
            {
                _slots.Add(Instantiate(_slotPrefab, _content));
                _slots[i].Set(_presenter.source[i].id, _presenter.source[i].num);
            }
            _presenter.source.OnItemAdded += (slotID, itemData) =>
            {
                if (slotID > _slots.Count - 1)
                {
                    InventorySlot slot = Instantiate(_slotPrefab, _content);                 
                    _slots.Add(slot);
                }
                _slots[slotID].Set(itemData.id, itemData.num);
            };
            _presenter.source.OnItemRemoved += (slotID, itemData) =>
            {
                _slots[slotID].Set(itemData.id, itemData.num);
            };
            _presenter.source.OnItemChanged += (slotID, itemData) =>
            {
                _slots[slotID].Set(itemData.id, itemData.num);
            };
        }
    }
}