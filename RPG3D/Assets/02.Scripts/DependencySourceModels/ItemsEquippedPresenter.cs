using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;
using ULB.RPG.DataModels;
using System.Linq;

namespace ULB.RPG.DataDependencySources
{
    public class ItemsEquippedPresenter
    {
        public ItemsEquippedSource source;
        public UnequipCommand unequipCommand;

        #region Equipment Source
        public class ItemsEquippedSource : ObservableCollection<int>
        {
            public ItemsEquippedSource(IEnumerable<int> data)
            {
                foreach (var item in data)
                {
                    Items.Add(item);
                }
            }
        }
        #endregion

        public ItemsEquippedPresenter()
        {
            source = new ItemsEquippedSource(ItemsEquippedDataModel.instance);
            ItemsEquippedDataModel.instance.OnItemAdded += (slotID, item) =>
            {
                source.Add(item);
            };
            ItemsEquippedDataModel.instance.OnItemRemoved += (slotID, item) =>
            {
                source.Remove(item);
            };
            ItemsEquippedDataModel.instance.OnItemChanged += (slotID, item) =>
            {
                source.Change(slotID, item);
            };

            unequipCommand = new UnequipCommand();
        }

        #region Unequip command
        public class UnequipCommand
        {
            private ItemsEquippedDataModel _equipmentDataModel;
            private InventoryDataModel _inventoryDataModel;

            public UnequipCommand()
            {
                _equipmentDataModel = ItemsEquippedDataModel.instance;
                _inventoryDataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(int itemID)
            {
                return _inventoryDataModel.FindIndex(x => x == ItemData.empty) >= 0;
            }

            public void Execute(EquipType equipType, int itemID)
            {
                _equipmentDataModel.Change((int)equipType, -1);
                int slotID = _inventoryDataModel.FindIndex(x => x == ItemData.empty);
                _inventoryDataModel.Change(slotID, new ItemData(itemID, 1));
            }

            public bool TryExecute(EquipType equipType, int itemID)
            {
                int slotID = _inventoryDataModel.FindIndex(x => x == ItemData.empty);

                if (slotID >= 0)
                {
                    _equipmentDataModel.Change((int)equipType, -1);
                    _inventoryDataModel.Change(slotID, new ItemData(itemID, 1));
                    return true;
                }

                return false;
            }
        }
        #endregion

    }
}