using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HeartDrop : LootDrop
{
    [SerializeField, Range(0f, 1f)] private float maxChance;
    public override bool ShouldDrop(EntitiesHandler entHandler)
    {
        var player = entHandler.player;
        if (player.health.Health < player.health.defaultHealth)
        {
            float chance;
            chance = Mathf.Lerp(maxChance, this.dropChance, player.health.Health / (float)player.health.defaultHealth);
            return Random.Range(0f, 1f) < chance;
        }
        else
        {
            return false;
        }
    }
}
