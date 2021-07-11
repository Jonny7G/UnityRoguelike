using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapGenerator generator;

    private void Start()
    {
        LoadNewLevel(); //on start to test behavior, eventually it would be something called upon the player reaching a level exit
    }
    private void LoadNewLevel()
    {
        generator.GenerateMap();
    }
}
