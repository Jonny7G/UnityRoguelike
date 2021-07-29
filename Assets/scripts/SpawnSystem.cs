using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public List<SpawnChange> allSpawners;
    [SerializeField] private Counter dungeonLevel;
    int currentSpawner = 0;

    [System.Serializable]
    public class SpawnChange
    {
        public EntitySpawner spawner;
        public int dungeonFloor;
    }
    public EntitySpawner GetSpawner()
    {
        if (currentSpawner < allSpawners.Count - 1 && allSpawners[currentSpawner + 1].dungeonFloor == dungeonLevel.count)
        {
            currentSpawner++;
        }
        return allSpawners[currentSpawner].spawner;
    }
}
