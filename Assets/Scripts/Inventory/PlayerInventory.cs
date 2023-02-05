using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ItemType, Item> items = new Dictionary<ItemType, Item>();

    private static PlayerInventory instance;
    public static PlayerInventory Instance => instance;


    public event Action OnToolsChanged;
    public event Action OnMoneyChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("Multiple inventories not allowed");
        }

        var itemsdata = Resources.LoadAll<ItemData>("Items");
        foreach (var data in itemsdata)
        {
            items.Add(data.ItemType, new Item()
            {
                type = data.ItemType,
                data = data,
                amount = 0
            });
        }
    }

    public bool HasItem(ItemType itemType)
    {
        return items.ContainsKey(itemType) && items[itemType].amount > 0;
    }

    public int GetItemCount(ItemType itemType)
    {
        if (!items.ContainsKey(itemType))
        {
            throw new System.Exception($"Item type does not exist. Make sure there is a ItemData asset linked to type {itemType} in the Resources/Items folder");
        }
        return items[itemType].amount;
    }

    public void InsertItem(ItemType itemType, int amount = 1)
    {
        if (!items.ContainsKey(itemType))
        {
            throw new System.Exception($"Item type does not exist. Make sure there is a ItemData asset linked to type {itemType} in the Resources/Items folder");
        }
        SetItemTypeAmount(itemType, items[itemType].amount + amount);
    }

    public void RemoveItem(ItemType itemType, int amount = 1)
    {
        if (!items.ContainsKey(itemType))
        {
            throw new System.Exception($"Item type does not exist. Make sure there is a ItemData asset linked to type {itemType} in the Resources/Items folder");
        }

        SetItemTypeAmount(itemType, items[itemType].amount - amount);
    }

    private void SetItemTypeAmount(ItemType itemType, int value = 1)
    {
        var max = items[itemType].data.Max > 0 ? items[itemType].data.Max : int.MaxValue;
        items[itemType].amount = Mathf.Clamp(value, 0, max);

        if (items[itemType].data.IsTool)
        {
            OnToolsChanged?.Invoke();
        }

        if (items[itemType].data.ItemType == ItemType.Money)
        {
            OnMoneyChanged?.Invoke();
        }
    }

    public List<Item> GetItemList()
    {
        return items
        .Values
        .Where((item) => item.amount > 0)
        .ToList();
    }

    public List<Item> GetToolsList()
    {
        return items
        .Values
        .Where((item) => item.amount > 0)
        .Where((item) => item.data.IsTool)
        .ToList();
    }

}


public class Item
{

    public ItemType type;

    public ItemData data;

    public int amount;
}