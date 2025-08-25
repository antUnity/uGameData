using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IndexedGameData
{
    [Serializable]
    public class IndexedRegistry<TIndex, TValue> where TValue : IIndexed<TIndex>, new()
    {
        // Fields
        [SerializeField] private List<TValue> items = new();

        private readonly Dictionary<TIndex, int> itemsIndex = new();

        // Properties
        // Public
        public TValue this[TIndex index]
        {
            get
            {
                if (!Contains(index))
                    throw new Exception($"Index `{index}` not found in list `{typeof(TValue)}`");

                if (itemsIndex[index] >= items.Count)
                    throw new Exception($"Index `{index}` is out of range in list `{typeof(TValue)}`");

                return items[itemsIndex[index]];
            }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public List<TValue> Items
        {
            get { return items.ToList(); }
        }

        // Methods
        // Public

        public void AddOrUpdate(TValue item)
        {
            if (!TryAddItem(item))
                items[itemsIndex[item.Index]] = item;
        }

        public void Clear()
        {
            items.Clear();
            itemsIndex.Clear();
        }

        public bool Contains(TIndex index)
        {
            if (index == null)
                return false;

            return itemsIndex.ContainsKey(index);
        }

        public int GetItemPositionByIndex(TIndex index)
        {
            if (Contains(index))
                return itemsIndex[index];
            else
                return -1;
        }

        public void Remove(TIndex index)
        {
            if (!Contains(index))
                return;

            int i = itemsIndex[index];
            items.RemoveAt(i);
            itemsIndex.Remove(index);

            UpdateListIndex(i);
        }

        public void UpdateListIndex(int start = 0)
        {
            if (items.Count == 0)
            {
                itemsIndex.Clear();
                return;
            }

            if (start == 0)
                itemsIndex.Clear();

            for (int i = start; i < items.Count; i++)
            {
                TIndex index = items[i].Index;

                if (index == null || index.Equals(default))
                    continue;

                if (start != 0)
                    itemsIndex.Remove(index);

                if (!itemsIndex.ContainsKey(index))
                    itemsIndex.Add(index, i);
                else
                {
                    items[i] = new();
                    Debug.LogError($"Discarded item with duplicate index [{index}] at position {i} in IndexedList<{typeof(TIndex)}, {typeof(TValue)}>");
                }
            }
        }

        // Private

        private bool TryAddItem(TValue item)
        {
            TIndex index = item.Index;

            if (itemsIndex.ContainsKey(index))
                return false;

            items.Add(item);
            itemsIndex.Add(index, items.Count - 1);

            return true;
        }
    }
}