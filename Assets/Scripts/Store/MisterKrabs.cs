using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MisterKrabs : MonoBehaviour
{

    [SerializeField]
    private List<StoreItem> storeItems;

    [SerializeField]
    private List<AreaSpecialItem> areaItems;

    [SerializeField]
    private int itemsPerShop = 2;

    public List<StoreItem> GetItemsFor(StoryArea area)
    {
        Clean();
        var item = areaItems.Find((areaItem) => areaItem.Area == area)?.Item;
        var itemsNeeded = item == null ? itemsPerShop : itemsPerShop - 1;
        var itemsToTake = Mathf.Clamp(itemsNeeded, 0, storeItems.Count);
        storeItems = storeItems.Shuffle();
        var newItems = storeItems.Take(itemsToTake).ToList();
        if (item != null)
        {
            newItems.Add(item);
        }
        return newItems;
    }

    public void ItemBought(StoreItem item)
    {
        item.Stock -= 1;
    }

    private void Clean()
    {
        storeItems = storeItems.Where((item) => item.Stock != 0).ToList();
        areaItems = areaItems.Where((item) => item.Item.Stock != 0).ToList();
    }

}

[System.Serializable]
public class AreaSpecialItem
{

    [field: SerializeField]
    public StoryArea Area { get; private set; }

    [field: SerializeField]
    public StoreItem Item { get; private set; }

}

[System.Serializable]
public class StoreItem
{

    [field: SerializeField]
    public ItemData Item { get; private set; }

    [field: SerializeField]
    public int Price { get; private set; }

    [field: SerializeField]
    public int Stock { get; set; } = -1;

}
