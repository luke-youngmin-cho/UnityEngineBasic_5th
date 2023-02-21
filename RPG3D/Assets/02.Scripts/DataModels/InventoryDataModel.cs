using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;

namespace ULB.RPG.DataModels
{
    [Serializable]
    public struct ItemData
    {
        public static ItemData empty => new ItemData(-1, 0);
        public int id;
        public int num;

        public ItemData(int id, int num)
        {
            this.id = id;
            this.num = num;
        }
    }

    [Serializable]
    public class InventoryDataModel : SingletonBase<InventoryDataModel>, IDataCollection<ItemData>
    {
        public List<ItemData> Items;

        public event Action<ItemData> OnItemAdded;
        public event Action<ItemData> OnItemRemoved;
        public event Action<ItemData> OnItemChanged;
        public event Action OnCollectionChanged;

        public int Count => Items.Count;
        private string _path = Application.persistentDataPath + "/InventoryData.json";

        public InventoryDataModel()
        {
            Items = new List<ItemData>();
        }

        protected override void Init()
        {
            base.Init();
            Load();
        }

        public void Save()
        {
            System.IO.File.WriteAllText(_path, JsonUtility.ToJson(this));
        }

        public void Load()
        {
            if (System.IO.File.Exists(_path) == false)
            {
                for (int i = 0; i < 32; i++)
                {
                    Items.Add(ItemData.empty);
                }
                Save();
            }
            else
            {
                Items = JsonUtility.FromJson<InventoryDataModel>(System.IO.File.ReadAllText(_path)).Items;
            }
        }

        public void Add(ItemData item)
        {
            Items.Add(item);
            Save();
            OnItemAdded?.Invoke(item);
            OnCollectionChanged?.Invoke();
        }

        public bool Remove(ItemData item)
        {
            if (Items.Remove(item))
            {
                Save();
                OnItemRemoved?.Invoke(item);
                OnCollectionChanged?.Invoke();
                return true;
            }

            return false;
        }

        public bool Change(int index, ItemData item)
        {
            if (index >= 0 &&
                index < Count)
            {
                Items[index] = item;
                OnItemChanged?.Invoke(item);
                OnCollectionChanged?.Invoke();
                return true;
            }

            return false;
        }

        public bool Change(Predicate<ItemData> match, ItemData item)
        {
            return Change(FindIndex(match), item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(ItemData item)
        {
            return Items.Contains(item);
        }

        public ItemData Find(Predicate<ItemData> match)
        {
            return Items.Find(match);
        }

        public int FindIndex(Predicate<ItemData> match)
        {
            return Items.FindIndex(match);
        }

        public IEnumerator<ItemData> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}