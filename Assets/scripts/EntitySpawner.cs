using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private Entity player;
    [SerializeField] private List<Enemy> enemyPrefabs;
    
    public List<Entity> SpawnEntities(MapData map)
    {
        //spawn entities using map data and store in allEntities, set player for enemies
        return new List<Entity>();
    }
}
