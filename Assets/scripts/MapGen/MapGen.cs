using UnityEngine.Tilemaps;
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
        Random.InitState(3);
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

    //Fill rooms in tilemap
    private void FillRooms()
    {
        UpdateTilemap(tree);
    }
    private void SetDoors()
    {
        if (mapData.rooms.Count > 0)
        {
            mapData.entrance = new Vector2Int(Random.Range(mapData.rooms[0].x + 1, mapData.rooms[0].max.x),
            Random.Range(mapData.rooms[0].y + 1, mapData.rooms[0].max.y));
            int minExitIndex = Mathf.Max(mapData.rooms.Count - 1, minExitDistance);
            int exitIndex = mapData.rooms.Count - 1;
            mapData.exit = new Vector2Int(Random.Range(mapData.rooms[exitIndex].x, mapData.rooms[exitIndex].max.x),
                Random.Range(mapData.rooms[exitIndex].y, mapData.rooms[exitIndex].max.y));
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
}
