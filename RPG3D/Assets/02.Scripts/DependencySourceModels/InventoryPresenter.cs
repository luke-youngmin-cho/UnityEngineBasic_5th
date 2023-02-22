using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;
using ULB.RPG.DataModels;
using System.Linq;

namespace ULB.RPG.DataDependencySources
{
    public class InventoryPresenter
    {
        public InventorySource source;
        public AddCommand addCommand;
        public RemoveCommand removeCommand;
        public SwapCommand swapCommand;

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

        public InventoryPresenter()
        {
            source = new InventorySource(InventoryDataModel.instance);
            InventoryDataModel.instance.OnItemAdded += (slotID, item) =>
            {
                source.Add(item);
            };
            InventoryDataModel.instance.OnItemRemoved += (slotID, item) =>
            {
                source.Remove(item);
            };
            InventoryDataModel.instance.OnItemChanged += (slotID, item) =>
            {
                source.Change(slotID, item);
            };

            addCommand = new AddCommand();
            removeCommand = new RemoveCommand();
            swapCommand = new SwapCommand();
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
    }
}