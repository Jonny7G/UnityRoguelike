using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField, Range(0f, 1f)] private float spawnChance;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float minDistToPlayer;

    [SerializeField, HideInInspector] private LevelExit exitObject;
    private void Awake()
    {
    }
    private void SpawnExit(EntitiesHandler entHandler)
    {
        exitObject = new GameObject("exit").AddComponent<LevelExit>();
        exitObject.entHandler = entHandler;
        exitObject.SetPosition(entHandler.map.exit);
        entHandler.otherEntities.AddEntity(exitObject);
    }
    public void SpawnEntities(EntitiesHandler entHandler)
    {
        //spawn entities using map data and store in allEntities, set player for enemies, turnHandler for
        SpawnExit(entHandler);
        RoomBounds room = entHandler.map.rooms[0];
        int spawned = 0;
        for (int x = room.min.x; x <= room.max.x; x++)
        {
            for (int y = room.min.y; y < room.max.y; y++)
            {
                var pos = new Vector2Int(x, y);
                if (entHandler.IsOpenTile(pos) && pos != entHandler.map.exit && Random.Range(0f, 1f) < spawnChance && Vector2Int.Distance(pos, entHandler.map.entrance) > minDistToPlayer)
                {
                    var newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
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
