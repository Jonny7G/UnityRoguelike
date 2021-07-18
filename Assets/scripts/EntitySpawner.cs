using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyPrefabs;
    public void SpawnEntities(EntitiesHandler entHandler)
    {
        //spawn entities using map data and store in allEntities, set player for enemies, turnHandler for
        for (int x = 0; x < entHandler.map.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < entHandler.map.tiles.GetLength(1); y++)
            {
                if (entHandler.IsOpenTile(new Vector2Int(x,y)))
                {
                    var newEnemy = Instantiate(enemyPrefabs[0]);
                    newEnemy.SetPosition(new Vector2Int(x, y));
                    newEnemy.entHandler = entHandler;
                    entHandler.liveEntities.AddEntity(newEnemy);
                    return;
                }
            }
        }

        return;
    }
}
