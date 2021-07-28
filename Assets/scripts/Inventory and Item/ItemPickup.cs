using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public abstract class ItemPickup : Entity
{

    protected virtual void Start()
    {

    }
    public override void TakeTurn()
    {
        if(entHandler.player.position == position)
        {
            PickupItem();
            entHandler.items.RemoveEntity(this);
        }
    }
    //what to do when the item is picked up, itementity is removed after this call
    public abstract void PickupItem();
}
