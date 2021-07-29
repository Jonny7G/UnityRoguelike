using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class LootDrop : ScriptableObject
{
    [SerializeField,Range(0f, 1f)] protected float dropChance;
    public ItemPickup item;
    public virtual bool ShouldDrop(EntitiesHandler entHandler)
    {
        return Random.Range(0f, 1f) <= dropChance;
    }
}
