using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public MapGenerator generator;
    public EntitiesHandler entities;
    private void Start()
    {
        LoadNewLevel(); //on start to test behavior, eventually it would be something called upon the player reaching a level exit
    }
    private void LoadNewLevel()
    {
        var map = generator.GenerateMap();
        entities.LoadEntities(map);
    }
}
