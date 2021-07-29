using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropHandler : MonoBehaviour
{
    [SerializeField] private List<LootDrop> lootDrops;
    public void DropLoot(LiveEntity entity)
    {
        var item = GetDrop(entity);
        if (item != null)
        {
            item.SetPosition(entity.position);
            entity.entHandler.AddItemEntity(item);
        }
    }
    public ItemPickup GetDrop(LiveEntity entity)
    {
        foreach (var drop in lootDrops)
        {
            if (drop.ShouldDrop(entity.entHandler))
            {
                return Instantiate(drop.item);
            }
        }
        return null;
    }
}
