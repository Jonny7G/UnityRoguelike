using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ItemPickup
{
    public Item item;

    protected override Sprite GetItemArt()
    {
        return item.art;
    }

    protected override void PickUpItem()
    {
        Inventory.Instance.AddItem(item);
    }
}
