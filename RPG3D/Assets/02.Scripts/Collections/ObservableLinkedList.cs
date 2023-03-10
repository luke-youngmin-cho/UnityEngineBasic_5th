using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObservableLinkedList<T> : INotifyCollectionChanged<T>, IEnumerable<T>
{
    protected LinkedList<T> Items = new LinkedList<T>();
    public event Action<T> ItemAdded;
    public event Action<T> ItemRemoved;
    public event Action CollectionChanged;

    public LinkedListNode<T> First => Items.First;
    public LinkedListNode<T> Last => Items.Last;
    public int Count => Items.Count;

    public void AddFirst(T item)
    {
        Items.AddFirst(item);
        ItemAdded?.Invoke(item);
        CollectionChanged?.Invoke();
    }

    public void AddLast(T item)
    {
        Items.AddLast(item);
        ItemAdded?.Invoke(item);
        CollectionChanged?.Invoke();
    }

    public void AddBefore(T target, T item)
    {
        Items.AddBefore(Items.Find(target), item);
        ItemAdded?.Invoke(item);
        CollectionChanged?.Invoke();
    }

    public void AddAfter(T target, T item)
    {
        Items.AddAfter(Items.Find(target), item);
        ItemAdded?.Invoke(item);
        CollectionChanged?.Invoke();
    }

    public bool Remove(T item)
    {
        if (Items.Remove(item))
        {
            ItemRemoved?.Invoke(item);
            CollectionChanged?.Invoke();
            return true;
        }

        return false;
    }

    public void RemoveFirst()
    {
        T value = Items.First.Value;
        Items.RemoveFirst();
        ItemRemoved?.Invoke(value);
        CollectionChanged?.Invoke();
    }
    public void RemoveLast()
    {
        T value = Items.Last.Value;
        Items.RemoveLast();
        ItemRemoved?.Invoke(value);
        CollectionChanged?.Invoke();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Items.GetEnumerator();
    }
}
