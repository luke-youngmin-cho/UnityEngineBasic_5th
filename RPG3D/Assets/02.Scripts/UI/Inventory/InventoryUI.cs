using System.Collections;
using System.Collections.Generic;
using ULB.RPG.DataDependencySources;
using UnityEngine;
using UnityEngine.UI;

namespace ULB.RPG.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventoryItemController _inventoryItemController;
        [SerializeField] private Transform _content;
        [SerializeField] private InventorySlot _slotPrefab;
        private List<InventorySlot> _slots;
        private InventoryPresenter _presenter;


        private void Awake()
        {
            _presenter = new InventoryPresenter();

            _slots = new List<InventorySlot>();
            for (int i = 0; i < _presenter.inventorySource.Count; i++)
            {
                _slots.Add(Instantiate(_slotPrefab, _content));
                _slots[i].Set(_presenter.inventorySource[i].id, _presenter.inventorySource[i].num);
                int slotID = i;
                _slots[i].slotID = slotID; 
                _slots[i].onUse += (itemID) =>
                {
                    if (_presenter.spendCommand.TryExecute(slotID, itemID))
                        Debug.Log($"[InventorySlot] : {slotID} 번째 슬롯의 아이템 {itemID} 를 소비했습니다.");
                    else if (_presenter.equipCommand.TryExecute(slotID, itemID))
                        Debug.Log($"[InventorySlot] : {slotID} 번째 슬롯의 아이템 {itemID} 를 장착했습니다.");
                };

                _slots[i].onControl += (itemID) =>
                {
                    _inventoryItemController.StartControl(slotID);
                };

            }
            _presenter.inventorySource.OnItemAdded += (slotID, itemData) =>
            {
                if (slotID > _slots.Count - 1)
                {
                    InventorySlot slot = Instantiate(_slotPrefab, _content);                 
                    _slots.Add(slot);
                }
                _slots[slotID].Set(itemData.id, itemData.num);
            };
            _presenter.inventorySource.OnItemRemoved += (slotID, itemData) =>
            {
                _slots[slotID].Set(itemData.id, itemData.num);
            };
            _presenter.inventorySource.OnItemChanged += (slotID, itemData) =>
            {
                _slots[slotID].Set(itemData.id, itemData.num);
            };
        }

        private void Start()
        {
            StandaloneInputModuleWrapper.main.RegisterGraphicRaycaster(GetComponent<GraphicRaycaster>());
        }
    }
}