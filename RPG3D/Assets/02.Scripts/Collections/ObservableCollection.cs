using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULB.RPG.Collections
{
    public class ObservableCollection<T> : IDataCollection<T>
    {
        public List<T> Items;
        public int Count => Items.Count;

        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;
        public event Action<T> OnItemChanged;
        public event Action OnCollectionChanged;

        public void Add(T item)
        {
            Items.Add(item);
            OnItemAdded?.Invoke(item);
            OnCollectionChanged?.Invoke();
        }

        public bool Change(int index, T item)
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

        public bool Change(Predicate<T> match, T item)
        {
            return Change(FindIndex(match), item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        public T Find(Predicate<T> match)
        {
            return Items.Find(match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return Items.FindIndex(match);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (Items.Remove(item))
            {
                OnItemRemoved?.Invoke(item);
                OnCollectionChanged?.Invoke();
                return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}