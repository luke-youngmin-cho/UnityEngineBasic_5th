using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;
using ULB.RPG.DataModels;
using UnityEditor.ShaderGraph.Internal;

namespace ULB.RPG.DataDependencySources
{
    public class InventoryPresenter
    {
        public InventorySource inventorySource;
        public ItemsEquippedSource itemsEquippedSource;
        public AddCommand addCommand;
        public RemoveCommand removeCommand;
        public SwapCommand swapCommand;
        public EquipCommand equipCommand;
        public SpendCommand spendCommand;

        #region Inventory Source
        public class InventorySource : ObservableCollection<ItemData>
        { 
            public InventorySource(IEnumerable<ItemData> data)
            {
                foreach (var item in data)
                {
                    Items.Add(item);
                }
            }
        }
        #endregion

        #region ItemsEquipped Source
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

        public InventoryPresenter()
        {
            inventorySource = new InventorySource(InventoryDataModel.instance);
            InventoryDataModel.instance.OnItemAdded += (slotID, item) =>
            {
                inventorySource.Add(item);
            };
            InventoryDataModel.instance.OnItemRemoved += (slotID, item) =>
            {
                inventorySource.Remove(item);
            };
            InventoryDataModel.instance.OnItemChanged += (slotID, item) =>
            {
                inventorySource.Change(slotID, item);
            };

            itemsEquippedSource = new ItemsEquippedSource(ItemsEquippedDataModel.instance);
            ItemsEquippedDataModel.instance.OnItemAdded += (slotID, item) =>
            {
                itemsEquippedSource.Add(item);
            };
            ItemsEquippedDataModel.instance.OnItemRemoved += (slotID, item) =>
            {
                itemsEquippedSource.Remove(item);
            };
            ItemsEquippedDataModel.instance.OnItemChanged += (slotID, item) =>
            {
                itemsEquippedSource.Change(slotID, item);
            };

            addCommand = new AddCommand();
            removeCommand = new RemoveCommand();
            swapCommand = new SwapCommand();
            equipCommand = new EquipCommand();
            spendCommand = new SpendCommand();
        }

        #region Add command
        public class AddCommand
        {
            public InventoryDataModel _dataModel;

            public AddCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(ItemData item)
            {
                return true;
            }

            public void Execute(ItemData item)
            {
                int index = _dataModel.FindIndex(x => x.id == item.id);
                if (index < 0)
                {
                    _dataModel.Add(item);
                }
                else
                {
                    _dataModel.Change(index, new ItemData(item.id, item.num + _dataModel.Items[index].num));
                }
            }

            public bool TryExecute(ItemData item)
            {
                if (CanExecute(item))
                {
                    Execute(item);
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Remove command
        public class RemoveCommand
        {
            public InventoryDataModel _dataModel;

            public RemoveCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(ItemData item)
            {
                int index = _dataModel.FindIndex(x => x.id == item.id);
                return index >= 0 && item.num <= _dataModel.Items[index].num;
            }

            public void Execute(ItemData item)
            {
                int index = _dataModel.FindIndex(x => x.id == item.id);
                //if (index < 0)
                //{
                //    throw new System.Exception($"[InventoryPresenter] : 존재하지 않는 아이템을 삭제하려고 시도했습니다. {item.id}");
                //}
                //else if (item.num > _dataModel.Items[index].num)
                //{
                //    throw new System.Exception($"[InventoryPresenter] : {item.id} 를 {item.num} 개 삭제하려고 시도했지만 {_dataModel.Items[index].num} 개 밖에 존재하지 않습니다.");
                //}
                //else 
                if (item.num == _dataModel.Items[index].num)
                {
                    _dataModel.Remove(item);
                }
                else
                {
                    _dataModel.Change(index, new ItemData(item.id, item.num + _dataModel.Items[index].num));
                }
            }

            public bool TryExecute(ItemData item)
            {
                if (CanExecute(item))
                {
                    Execute(item);
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Swap command
        public class SwapCommand
        {
            public InventoryDataModel _dataModel;

            public SwapCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(int slot1, int slot2)
            {
                return true;
            }

            public void Execute(int slot1, int slot2)
            {
                ItemData tmp = _dataModel.Items[slot1];
                _dataModel.Change(slot1, _dataModel.Items[slot2]);
                _dataModel.Change(slot2, tmp);
            }

            public bool TryExecute(int slot1, int slot2)
            {
                if (CanExecute(slot1, slot2))
                {
                    Execute(slot1, slot2);
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Equip command

        public class EquipCommand
        {
            InventoryDataModel _inventoryDataModel;
            ItemsEquippedDataModel _itemsEquippedDataModel;

            public EquipCommand()
            {
                _inventoryDataModel = InventoryDataModel.instance;
                _itemsEquippedDataModel = ItemsEquippedDataModel.instance;
            }

            public bool CanExecute(int slotID, int itemID)
            {
                // -> 직업군이 맞는지, 레벨은 충족하는지, 스텟은 충족하는지,
                // 한손무기와방패를장착하고있는데 인벤토리 빈공간없이 양손무기 장착하려고 했는지 등등..체크해야함
                return ItemInfoAssets.instance[itemID].prefab is Equipment;
            }

            public void Execute(int slotID, int itemID)
            {
                // 장착하려는 장비의 타입
                int equipType = (int)((Equipment)ItemInfoAssets.instance[itemID].prefab).type;

                // 이미 장착하고있던 아이템의 ID
                int itemEquippedID = _itemsEquippedDataModel.Items[equipType];
                  
                // 장착하고있던 아이템의 ID 로 인벤토리 슬롯 변경 (장착하고있던거 없으면 비움)
                _inventoryDataModel.Change(slotID,
                                           itemEquippedID < 0 ? ItemData.empty : new ItemData(itemEquippedID, 1));

                // 장착하려던 아이템의 ID 로 장비슬롯 변경
                _itemsEquippedDataModel.Change(equipType, itemID);
            }

            public bool TryExecute(int slotID, int itemID)
            {
                if (CanExecute(slotID, itemID))
                {
                    Execute(slotID, itemID);
                    return true;
                }
                return false;
            }

        }

        #endregion

        #region Spend command
        public class SpendCommand
        {
            public InventoryDataModel _dataModel;

            public SpendCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(int slotID, int itemID)
            {
                return ItemInfoAssets.instance[itemID].prefab is Spend &&
                       _dataModel.Items[slotID].id == itemID && 
                       _dataModel.Items[slotID].num > 0;
            }

            public void Execute(int slotID, int itemID)
            {
                if (_dataModel.Items[slotID].num > 1)
                    _dataModel.Change(slotID, new ItemData(itemID, _dataModel.Items[slotID].num - 1));
                else
                    _dataModel.Change(slotID, ItemData.empty);
                 //((Spend)ItemInfoAssets.instance[itemID].prefab).hpRecoveryAmount 이걸로 플레이어 체력 회복
            }

            public bool TryExecute(int slotID, int itemID)
            {
                if (CanExecute(slotID, itemID))
                {
                    Execute(slotID, itemID);
                    return true;
                }

                return false;
            }
        }
        #endregion
    }
}