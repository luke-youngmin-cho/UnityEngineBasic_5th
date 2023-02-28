using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;

namespace ULB.RPG.DataModels
{

    [Serializable]
    public class ItemsEquippedDataModel : SingletonBase<ItemsEquippedDataModel>, IDataCollection<int>
    {
        public List<int> Items;

        public event Action<int, int> OnItemAdded;
        public event Action<int, int> OnItemRemoved;
        public event Action<int, int> OnItemChanged;
        public event Action OnCollectionChanged;

        public int Count => Items.Count;
        private string _path = Application.persistentDataPath + "/ItemsEquippedData.json";

        public ItemsEquippedDataModel()
        {
            Items = new List<int>();
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
                foreach (int index in Enum.GetValues(typeof(EquipType)))
                {
                    Items.Add(-1);
                }  

                Save();
            }
            else
            {
                Items = JsonUtility.FromJson<ItemsEquippedDataModel>(System.IO.File.ReadAllText(_path)).Items;
            }
        }

        public void Add(int item)
        {
            Items.Add(item);
            Save();
            OnItemAdded?.Invoke(Count - 1, item);
            OnCollectionChanged?.Invoke();
        }

        public bool Remove(int item)
        {
            int index = Items.IndexOf(item);
            if (index >= 0)
            {
                Items.RemoveAt(index);
                Save();
                OnItemRemoved?.Invoke(index, item);
                OnCollectionChanged?.Invoke();
                return true;
            }

            return false;
        }

        public bool Change(int index, int item)
        {
            if (index >= 0 &&
                index < Count)
            {
                Items[index] = item;
                Save();
                OnItemChanged?.Invoke(index, item);
                OnCollectionChanged?.Invoke();
                return true;
            }

            return false;
        }

        public bool Change(Predicate<int> match, int item)
        {
            return Change(FindIndex(match), item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(int item)
        {
            return Items.Contains(item);
        }

        public int Find(Predicate<int> match)
        {
            return Items.Find(match);
        }

        public int FindIndex(Predicate<int> match)
        {
            return Items.FindIndex(match);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}