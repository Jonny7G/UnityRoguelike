using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinItem : ItemPickup
{
    [SerializeField] private Sprite art;
    protected override Sprite GetItemArt()
    {
        return art;
    }

    protected override void PickUpItem()
    {
        //add score
    }
}
