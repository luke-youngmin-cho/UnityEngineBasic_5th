using System.Collections;
using TMPro;
using ULB.RPG.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace ULB.RPG.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _num; 

        public void Set(int itemID, int num)
        {
            if (itemID >= 0)
            {
                _icon.sprite = ItemInfoAssets.instance[itemID].icon;
                _num.text = num.ToString();
            }
        }
    }
}