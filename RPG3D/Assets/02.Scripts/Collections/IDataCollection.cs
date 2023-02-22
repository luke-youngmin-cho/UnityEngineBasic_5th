using System;
using System.Collections;
using System.Collections.Generic;
using ULB.RPG.DataModels;
using UnityEngine;

namespace ULB.RPG.Collections
{
    public interface IDataCollection<T> : IEnumerable<T>
    {
        int Count { get; }

        event Action<int, T> OnItemAdded;
        event Action<int, T> OnItemRemoved;
        event Action<int, T> OnItemChanged;
        event Action OnCollectionChanged;

        void Add(T item);
        bool Remove(T item);
        bool Change(int index, T item);
        bool Change(Predicate<T> match, T item);
        void Clear();
        bool Contains(T item);
        T Find(Predicate<T> match);
        int FindIndex(Predicate<T> match);
    }
}