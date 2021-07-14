using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesHandler : MonoBehaviour
{
    public LiveEntity player;
    public EntityCollection<LiveEntity> liveEntities;
    public EntityCollection<ItemPickup> Items;

    [SerializeField] private EntitySpawner spawner;

    public void LoadEntities(MapData map)
    {
        liveEntities.LoadEntities(spawner.SpawnEntities(map, player));
        liveEntities.AddEntity(player);
        player.SetPosition(map.entrance);
        //need to subscribe to entity health ondeath event so we can remove it
    }
    public void RemoveEntities()
    {
        liveEntities.RemoveEntity(player);
        liveEntities.RemoveEntities();
        Items.RemoveEntities();
    }
    public void MoveTurn(TurnHandler trnHandler)
    {
        liveEntities.TakeTurns(trnHandler);
        Items.TakeTurns(trnHandler);
    }
}
public class EntityCollection<T> where T : Entity
{
    public List<T> entities;

    public void LoadEntities(List<T> entities)
    {
        entities.AddRange(entities);
    }
    public void AddEntity(T entity)
    {
        entities.Add(entity);
    }public void RemoveEntity(T entity)
    {
        entities.Remove(entity);
    }
    private void RemoveObjects()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Object.Destroy(entities[i].gameObject);
        }
        entities.Clear();
    }
    public void RemoveEntities()
    {
        RemoveObjects();
    }
    public void TakeTurns(TurnHandler turnHandler)
    {
        foreach (var entity in entities)
        {
            entity.TakeTurn(turnHandler);
        }
    }
    public T GetEntity(Vector2Int tilePos)
    {
        foreach (var entity in entities)
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
        foreach (var entity in entities)
        {
            if (entity.position == tilePos)
            {
                return true;
            }
        }
        return false;
    }
}
