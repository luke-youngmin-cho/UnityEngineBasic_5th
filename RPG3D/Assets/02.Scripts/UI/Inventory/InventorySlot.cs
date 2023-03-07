using System;
using System.Collections;
using TMPro;
using ULB.RPG.DataModels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ULB.RPG.UI
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        public int slotID;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _numText;
        public event Action<int> onUse;
        public event Action<int> onControl;
        private int _itemID;
        private int _num;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                onUse?.Invoke(_itemID);
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                onControl?.Invoke(_itemID);
            }
        }

        public void Set(int itemID, int num)
        {
            if (itemID >= 0)
            {
                _itemID = itemID;
                _num = num;
                _icon.sprite = ItemInfoAssets.instance[itemID].icon;
                _numText.text = num.ToString();
            }
            else
            {
                _icon.sprite = null;
                _numText.text = string.Empty;
            }
        }        

        public void Clear()
        {
            _icon.sprite = null;
        }
    }
}