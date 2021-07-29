﻿using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapSize;
    public int minExitDistance;
    [Range(0, 6)]
    public int iterations;

    [Range(1, 4)]
    public int hallway;

    public bool shouldDebugDrawBsp;
    public bool showTiles, showRooms;
    public const int MIN_ROOM_DELTA = 3;
    private BspTree tree;
    private MapData mapData;
    [SerializeField] private MapRenderer rndr;
    void Start()
    {

    }
    void Update()
    {
    }
    //Clears prev map
    private void InitReferences()
    {
        //int ranSeed = Random.Range(3, 6);
        //Random.InitState(ranSeed);
        //iterations = ranSeed;
    }

    //Creates Rooms inside container
    private void GenerateRooms()
    {
        BspTree.GenerateNode(tree);
    }

    //Creates container/rectangle
    private void GenerateContainer()
    {
        tree = BspTree.Split(iterations, new RectInt(0, 0, mapSize, mapSize));
    }

    //Apply to tile map
    private void UpdateTilemap(BspTree node)
    {
        if (node.left == null && node.right == null)
        {
            mapData.rooms.Add(node.room);
            for (int i = node.room.x; i < node.room.xMax; i++)
            {
                for (int j = node.room.y; j < node.room.yMax; j++)
                {
                    mapData.tiles[i, j] = 1;
                }
            }
        }
        else
        {
            if (node.left != null) UpdateTilemap(node.left);
            if (node.right != null) UpdateTilemap(node.right);
        }
    }
    private void UpdateTilemapInOrder(BspTree node)
    {
        if (node == null)
        {
            return;
        }
        UpdateTilemapInOrder(node.left);
        if (Mathf.Abs(node.room.max.x - node.room.min.x) == 0 || Mathf.Abs(node.room.max.y - node.room.min.y) <= 0)
        {

        }
        else
        {
            mapData.rooms.Add(node.room);
        }
        for (int i = node.room.min.x; i < node.room.xMax; i++)
        {
            for (int j = node.room.min.y; j < node.room.yMax; j++)
            {
                mapData.tiles[i, j] = 1;
            }
        }
        UpdateTilemapInOrder(node.right);
    }
    //Fill rooms in tilemap
    private void FillRooms()
    {
        UpdateTilemapInOrder(tree);
    }
    private void SetDoors()
    {
        if (mapData.rooms.Count > 0)
        {
            mapData.entrance = new Vector2Int(Random.Range(mapData.rooms[0].min.x, mapData.rooms[0].max.x),
            Random.Range(mapData.rooms[0].min.y, mapData.rooms[0].max.y));
            int minExitIndex = Mathf.Max(mapData.rooms.Count - 1, minExitDistance);
            int exitIndex = mapData.rooms.Count - 1;
            mapData.exit = new Vector2Int(Random.Range(mapData.rooms[exitIndex].min.x, mapData.rooms[exitIndex].max.x),
                Random.Range(mapData.rooms[exitIndex].min.y, mapData.rooms[exitIndex].max.y));
        }
        else
        {
            Debug.LogError("no rooms!");
        }
    }
    //Create hallways
    private void GenerateHallway()
    {
        GenerateHallwayNode(tree);
    }

    //Create spawn point for hallway
    private void GenerateHallwayNode(BspTree node)
    {
        if (node.what_is_internal())
        {
            RectInt leftContainer = node.left.container;
            RectInt rightContainer = node.right.container;
            Vector2 leftCenter = leftContainer.center;
            Vector2 rightCenter = rightContainer.center;
            Vector2 direction = (rightCenter - leftCenter).normalized;
            while (Vector2.Distance(leftCenter, rightCenter) > 1)
            {
                if (direction.Equals(Vector2.right))
                {
                    for (int i = 0; i < hallway; i++)
                    {
                        mapData.tiles[(int)leftCenter.x, (int)leftCenter.y + i] = 1;
                    }
                }
                else if (direction.Equals(Vector2.up))
                {
                    for (int i = 0; i < hallway; i++)
                    {
                        mapData.tiles[(int)leftCenter.x + i, (int)leftCenter.y] = 1;
                    }
                }
                leftCenter.x += direction.x;
                leftCenter.y += direction.y;
            }
            if (node.left != null) GenerateHallwayNode(node.left);
            if (node.right != null) GenerateHallwayNode(node.right);
        }

    }
    public MapData GenerateMap()
    {
        mapData = new MapData();
        mapData.tiles = new int[mapSize, mapSize];
        InitReferences();
        GenerateContainer();
        GenerateRooms();
        GenerateHallway();
        FillRooms();
        SetDoors();
        rndr.Render(mapData);
        return mapData;
    }
    private void OnDrawGizmos()
    {
        if (mapData != null)
        {
            if (showRooms)
            {
                foreach (var item in mapData.rooms)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(Vector2.Lerp(item.min, item.max, 0.5f), (Vector2)item.size);
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere((Vector2)item.min, 0.3f);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere((Vector2)item.max, 0.3f);

                }
            }
            if (showTiles)
            {
                for (int x = 0; x < mapData.tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < mapData.tiles.GetLength(0); y++)
                    {
                        if (mapData.tiles[x, y] == 1)
                        {
                            Gizmos.color = Color.white;
                            Gizmos.DrawWireSphere(new Vector2(x + 0.5f, y), 0.3f);
                        }
                    }
                }
            }

        }
    }
}
