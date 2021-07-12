using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesHandler : MonoBehaviour
{
    [SerializeField] private EntitySpawner spawner;
    [SerializeField] private List<Entity> allEntities;

    public void LoadEntities(MapData map)
    {
        allEntities = spawner.SpawnEntities(map);
    }
    public void RemoveEntities()
    {
        for(int i = allEntities.Count - 1; i >= 0; i--)
        {
            Destroy(allEntities[i].gameObject);
        }
        allEntities.Clear();
    }
}
