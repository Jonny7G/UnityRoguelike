using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : SingletonBehaviour<Inventory>
{
    [SerializeField] private int inventorySize;
    [SerializeField] private List<ItemSlot> equipSlots = new List<ItemSlot>();
    [SerializeField] private List<Item> inventory = new List<Item>();
    public void AddItem(Item item)
    {
        if (item.type == ItemType.gold)
        {
            //add to gold counter
        }
        else
        {
            inventory.Add(item);
        }
    }
    public void Equip(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (equipSlots[i].currentItem.type == item.type)
            {
                equipSlots[i].currentItem = item;
                return;
            }
        }
    }
}
public enum ItemType { helmet, chestplate, boots, weapon, ring, amulet, gold }
[System.Serializable]
public class ItemSlot
{
    public Item currentItem = null;
    public ItemType slotType;
}