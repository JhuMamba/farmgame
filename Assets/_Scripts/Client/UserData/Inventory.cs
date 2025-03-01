using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<ItemSO, int> m_items = new();
    public Dictionary<ItemSO, int> Items => m_items;
    public Action onInventoryChanged;
    public void AddItem(ItemSO item, int count)
    {
        if (m_items.ContainsKey(item))
        {
            m_items[item] += count;
        }
        else
        {
            m_items.TryAdd(item, count);
        }
        onInventoryChanged?.Invoke();
    }
    public bool RemoveItem(ItemSO item, int count)
    {
        if (m_items.ContainsKey(item) && m_items[item] >= count)
        {
            m_items[item] -= count;
            if (m_items[item] <= 0) m_items.Remove(item);
            onInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }
}
