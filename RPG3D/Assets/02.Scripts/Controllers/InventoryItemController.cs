using System.Collections;
using System.Collections.Generic;
using ULB.RPG;
using ULB.RPG.Controllers;
using ULB.RPG.DataModels;
using ULB.RPG.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    private int _slotID;
    private int _itemID;
    private int _itemNum;
    private InventoryDataModel _inventoryDataModel;
    private ItemsEquippedDataModel _itemsEquippedDataModel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (StandaloneInputModuleWrapper.main.TryGetGameObjectPointed<GraphicRaycaster>(StandaloneInputModule.kMouseLeftId,
                                                                                            out GameObject result,
                                                                                            1 << gameObject.layer))
            {
                // 타 인벤토리 슬롯 클릭시 스왑
                if (result.TryGetComponent(out InventorySlot inventorySlot))
                {
                    _inventoryDataModel.Change(_slotID, new ItemData(_inventoryDataModel.Items[inventorySlot.slotID].id,
                                                                     _inventoryDataModel.Items[inventorySlot.slotID].num));
                    _inventoryDataModel.Change(inventorySlot.slotID, new ItemData(_itemID,
                                                                                  _itemNum));
                }
                // 장비 슬롯 클릭시 아이템 장착
                else if (result.TryGetComponent(out ItemEquippedSlot itemEquippedSlot))
                {
                    int equipType = (int)itemEquippedSlot.equipType;

                    if (_itemsEquippedDataModel.Items[equipType] < 0 ?
                        _inventoryDataModel.Change(_slotID, ItemData.empty) :
                        _inventoryDataModel.Change(_slotID, new ItemData(_itemsEquippedDataModel.Items[equipType], 1)))
                    {
                        _itemsEquippedDataModel.Change(equipType, _itemID);
                    }
                }
            }
            else
            {
                int itemID = _inventoryDataModel.Items[_slotID].id;
                int equipType = (int)((Equipment)ItemInfoAssets.instance[itemID].prefab).type;
                _inventoryDataModel.Change(_slotID, ItemData.empty);
                ItemController itemController = new GameObject().AddComponent<ItemController>();
                itemController.Set(ItemInfoAssets.instance[itemID], 1);
            }
        }

        CancelControl();
    }

    public void StartControl(int slotID)
    {
        _slotID = slotID;
        _itemID = _inventoryDataModel.Items[_slotID].id;
        _itemNum = _inventoryDataModel.Items[_slotID].num;

        if (ItemInfoAssets.instance[_itemID] != null)
            _icon.sprite = ItemInfoAssets.instance[_itemID].icon;

        gameObject.SetActive(true);
    }

    public void CancelControl()
    {
        _slotID = -1;
        _itemID = -1;
        _itemNum = -1;
        _icon.sprite = null;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _inventoryDataModel = InventoryDataModel.instance;
        _itemsEquippedDataModel = ItemsEquippedDataModel.instance;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
