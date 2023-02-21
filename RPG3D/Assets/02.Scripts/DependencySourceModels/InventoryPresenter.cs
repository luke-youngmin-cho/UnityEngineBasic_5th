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
            InventoryDataModel.instance.OnItemAdded += (item) =>
            {
                source.Add(item);
            };
            InventoryDataModel.instance.OnItemRemoved += (item) =>
            {
                source.Remove(item);
            };
            InventoryDataModel.instance.OnItemChanged += (item) =>
            {
                source.Change(x => x.id == item.id, item);
            };
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
    }
}