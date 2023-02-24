using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;
using ULB.RPG.DataModels;
using System.Linq;

namespace ULB.RPG.DataDependencySources
{
    public class EquipmentPresenter
    {
        public EquipmentSource source;
        public UnequipCommand unequipCommand;

        #region Equipment Source
        public class EquipmentSource : ObservableCollection<int>
        {
            public EquipmentSource(IEnumerable<int> data)
            {
                foreach (var item in data)
                {
                    Items.Add(item);
                }
            }
        }
        #endregion

        public EquipmentPresenter()
        {
            source = new EquipmentSource(EquipmentDataModel.instance);
            EquipmentDataModel.instance.OnItemAdded += (slotID, item) =>
            {
                source.Add(item);
            };
            EquipmentDataModel.instance.OnItemRemoved += (slotID, item) =>
            {
                source.Remove(item);
            };
            EquipmentDataModel.instance.OnItemChanged += (slotID, item) =>
            {
                source.Change(slotID, item);
            };

            unequipCommand = new UnequipCommand();
        }

        #region Unequip command
        public class UnequipCommand
        {
            private EquipmentDataModel _equipmentDataModel;
            private InventoryDataModel _inventoryDataModel;

            public UnequipCommand()
            {
                _equipmentDataModel = EquipmentDataModel.instance;
                _inventoryDataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(int itemID)
            {
                return _inventoryDataModel.FindIndex(x => x == ItemData.empty) >= 0;
            }

            public void Execute(int itemID)
            {
                _equipmentDataModel.Remove(itemID);
                int slotID = _inventoryDataModel.FindIndex(x => x == ItemData.empty);
                _inventoryDataModel.Change(slotID, new ItemData(itemID, 1));
            }

            public bool TryExecute(int itemID)
            {
                int slotID = _inventoryDataModel.FindIndex(x => x == ItemData.empty);

                if (slotID >= 0)
                {
                    _equipmentDataModel.Remove(itemID);
                    _inventoryDataModel.Change(slotID, new ItemData(itemID, 1));
                    return true;
                }

                return false;
            }
        }
        #endregion

    }
}