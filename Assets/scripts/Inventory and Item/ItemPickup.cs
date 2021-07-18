using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ItemPickup : Entity
{
    public Item item;
    protected SpriteRenderer sr;
    protected virtual void Start()
    {
        sr.sprite = item.art;
    }
    public override void TakeTurn()
    {
        if(entHandler.player.position == position)
        {
            Inventory.Instance.AddItem(item);
            entHandler.items.RemoveEntity(this);
        }
    }

}
