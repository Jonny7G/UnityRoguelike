using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : ItemPickup
{
    public int healAmount = 1;
    [Range(0f, 1f)] public float minChance, maxChance;
    protected override void Start()
    {
        base.Start();
    }
    public override void PickupItem()
    {
        entHandler.player.health.Heal(healAmount);
    }
}
