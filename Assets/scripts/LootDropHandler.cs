using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropHandler : MonoBehaviour
{
    [SerializeField] private List<LootDrop> lootDrops;
    public Item GetDrop()
    {
        foreach (var drop in lootDrops)
        {
            if (drop.ShouldDrop())
            {
                return drop.item;
            }
        }
        return null;
    }
}
[System.Serializable]
public class LootDrop
{
    public Item item;
    [Range(0f, 1f)] public float dropChance;
    public bool ShouldDrop()
    {
        return Random.Range(0f, 1f) <= dropChance;
    }
}