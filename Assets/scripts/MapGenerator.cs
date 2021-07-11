﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapRenderer mapRndr;
    private void Start()
    {
        //called on scene load, this object will persist throughout the game so its only called once on game start
    }
    public void GenerateMap()
    {
        mapRndr.Render(GetMap());
    }
    private MapData GetMap()
    {
        MapData data = new MapData();
        //put tile data in data.tiles as an int[,] of 1's and 0's and set the entrance and exit as (x,y)indexes of data.tiles
        data.tiles = new int[,]
        {
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,1,1,1,1,0,0,0,0},
            { 0,0,1,1,1,1,0,0,0,0},
            { 0,0,1,0,0,0,1,1,0,0},
            { 0,0,1,0,0,1,1,1,0,0},
            { 0,0,1,0,1,1,1,1,0,0},
            { 0,0,1,0,1,1,1,1,0,0},
            { 0,0,1,1,1,1,1,1,0,0},
            { 0,0,0,0,1,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0}
        };
        return data;
    }
}
//Stores the basic data of a a level
public class MapData
{
    public int[,] tiles;
    public Vector2Int entrance, exit;
}