using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : ItemPickup
{
    [SerializeField] private Counter gold;
    public override void PickupItem()
    {
        gold.Increment();
    }
}
