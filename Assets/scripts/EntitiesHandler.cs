using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesHandler : MonoBehaviour
{
    public List<Entity> allEntities;

    [SerializeField] private EntitySpawner spawner;
    [SerializeField] private Entity player;

    public void LoadEntities(MapData map)
    {
        allEntities.Add(player);
        allEntities = spawner.SpawnEntities(map, player);
        player.SetPosition(map.entrance);
        //need to subscribe to entity health ondeath event so we can remove it
        //could also move player to door position here
    }
    public void RemoveEntities()
    {
        for (int i = allEntities.Count - 1; i >= 0; i--)
        {
            Destroy(allEntities[i].gameObject);
        }
        allEntities.Clear();
    }
    public Entity GetEntity(Vector2Int tilePos)
    {
        foreach (var entity in allEntities)
        {
            if (entity.position == tilePos)
            {
                return entity;
            }
        }
        return null;
    }
    public bool HasEntity(Vector2Int tilePos)
    {
        foreach (var entity in allEntities)
        {
            if (entity.position == tilePos)
            {
                return true;
            }
        }
        return false;
    }
}
