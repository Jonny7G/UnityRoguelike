using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public abstract class ItemPickup : Entity
{
    SpriteRenderer sr;
    protected virtual void Start()
    {
        sr.sprite = GetItemArt();
    }
    public override void TakeTurn(TurnHandler turnHandler)
    {
        if(turnHandler.entities.player.position == position)
        {
            PickUpItem();
        }
    }
    protected abstract void PickUpItem();
    protected abstract Sprite GetItemArt();
}
