using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField, Range(0f, 1f)] private float spawnChance;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float minDistToPlayer;
    public void SpawnEntities(EntitiesHandler entHandler)
    {
        //spawn entities using map data and store in allEntities, set player for enemies, turnHandler for
        RoomBounds room = entHandler.map.rooms[0];
        int spawned = 0;
        for (int x = room.min.x; x <= room.max.x; x++)
        {
            for (int y = room.min.y; y < room.max.y; y++)
            {
                if (entHandler.IsOpenTile(new Vector2Int(x, y)) && Random.Range(0f, 1f) < spawnChance && Vector2Int.Distance(new Vector2Int(x, y), entHandler.map.entrance) > minDistToPlayer)
                {
                    var newEnemy = Instantiate(enemyPrefabs[0]);
                    newEnemy.SetPosition(new Vector2Int(x, y));
                    newEnemy.entHandler = entHandler;
                    entHandler.liveEntities.AddEntity(newEnemy);
                    spawned++;
                    if (spawned >= maxEnemies)
                    {
                        return; //stop spawning
                    }
                }
            }
        }

        return;
    }
}
