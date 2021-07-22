using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesHandler : MonoBehaviour
{
    public LiveEntity player;
    public EntityCollection<LiveEntity> liveEntities = new EntityCollection<LiveEntity>();
    public EntityCollection<ItemPickup> items = new EntityCollection<ItemPickup>();
    public EntityCollection<Entity> otherEntities = new EntityCollection<Entity>();
    [SerializeField] private EntitySpawner spawner;
    public MapData map;
    public void MoveTurn()
    {
        liveEntities.TakeTurns();
        items.TakeTurns();
        otherEntities.TakeTurns();
        Debug.Log("turn moved");
    }
    public void LoadEntities(MapData map)
    {
        this.map = map;
        player.SetPosition(map.entrance);
        liveEntities.AddEntity(player);
        spawner.SpawnEntities(this);
        //need to subscribe to entity health ondeath event so we can remove it
    }
    public void RemoveEntities()
    {
        liveEntities.entities.Remove(player); //remove but don't destroy
        liveEntities.RemoveEntities();
        items.RemoveEntities();
        otherEntities.RemoveEntities();
    }

    public bool IsOpenTile(Vector2Int position)
    {
        if (position.x >= 0 && position.y >= 0 && position.x < map.tiles.GetLength(0) && position.y < map.tiles.GetLength(1))
        {
            return map.tiles[position.x, position.y] == 1 && !liveEntities.HasEntity(position);
        }
        else
        {
            return false;
        }
    }
}
[System.Serializable]
public class EntityCollection<T> where T : Entity
{
    public List<T> entities = new List<T>();

    public void LoadEntities(List<T> entities)
    {
        entities.AddRange(entities);
    }
    public void AddEntity(T entity)
    {
        entities.Add(entity);
    }
    public void RemoveEntity(T entity)
    {
        entities.Remove(entity);
        Object.Destroy(entity.gameObject);
        Debug.Log("entity removed");
    }
    private void RemoveObjects()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            if (entities[i] != null)
            {
                Object.Destroy(entities[i].gameObject);
            }
        }
        entities.Clear();
    }
    public void RemoveEntities()
    {
        RemoveObjects();
    }
    public void TakeTurns()
    {
        for(int i = entities.Count - 1; i >= 0; i--)
        {
            entities[i].TakeTurn();
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
