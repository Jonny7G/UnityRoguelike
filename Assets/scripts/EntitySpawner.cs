using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;

    public List<LiveEntity> SpawnEntities(MapData map, Entity player)
    {
        //spawn entities using map data and store in allEntities, set player for enemies
        return new List<LiveEntity>();
    }
}
