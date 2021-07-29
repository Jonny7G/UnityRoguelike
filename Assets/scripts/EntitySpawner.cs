using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private List<Enemy> enemyPrefabs;
    [Header("Spawning")]
    [Tooltip("How full should the map be")]
    [SerializeField, Range(0f, 1f)] private float density;
    [Tooltip("Enemies to spawn in a given room")]
    [SerializeField] private int minEnemies, maxEnemies;
    [SerializeField] private HeartDrop heartAtStartDrop;
    //serialize but don't show
    [SerializeField, HideInInspector] private LevelExit exitObject;

    public void SpawnEntities(EntitiesHandler entHandler)
    {
        //spawn entities using map data and store in allEntities, set player for enemies, turnHandler for
        SpawnHeart(entHandler);
        SpawnExit(entHandler);
        List<int> rooms = new List<int>();
        //1 because 0 is where player is at start
        for (int i = 1; i < entHandler.map.rooms.Count; i++)
        {
            rooms.Add(i);
        }
        int fullRooms = Mathf.FloorToInt(density * (rooms.Count - 1));
        for (int i = 0; i < fullRooms; i++)
        {
            int roomIndex = Random.Range(0, rooms.Count);
            SpawnInRoom(rooms[roomIndex], entHandler);
            rooms.Remove(roomIndex);
        }
    }
    private void SpawnHeart(EntitiesHandler entHandler)
    {
        if (heartAtStartDrop.ShouldDrop(entHandler))
        {
            var heart = Instantiate(heartAtStartDrop.item);
            heart.SetPosition(GetRandomValidRoomPosition(0, entHandler));
            entHandler.AddItemEntity(heart);
        }
    }
    private void SpawnExit(EntitiesHandler entHandler)
    {
        exitObject = new GameObject("exit").AddComponent<LevelExit>();
        exitObject.entHandler = entHandler;
        exitObject.SetPosition(entHandler.map.exit);
        entHandler.otherEntities.AddEntity(exitObject);
    }
    private Vector2Int GetRandomValidRoomPosition(int room, EntitiesHandler entHandler)
    {
        RectInt roomBnds = entHandler.map.rooms[room];
        var pos = new Vector2Int(
                Random.Range(roomBnds.min.x, roomBnds.max.x),
                Random.Range(roomBnds.min.y, roomBnds.max.y)
                );
        return pos;
    }
    private void SpawnInRoom(int room, EntitiesHandler entHandler)
    {
        int numToSpawn = Random.Range(minEnemies, maxEnemies);
        for (int i = 0; i <= numToSpawn; i++)
        {
            var pos = GetRandomValidRoomPosition(room, entHandler);
            var newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            newEnemy.SetPosition(pos);
            newEnemy.entHandler = entHandler;
            newEnemy.spawnRoom = room;
            entHandler.liveEntities.AddEntity(newEnemy);
        }
    }
}
