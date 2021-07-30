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
    public bool showTiles, showRooms, showContainers, showHall;
    public const int MIN_ROOM_DELTA = 3;
    private BspTree tree;
    private List<RectInt> containers = new List<RectInt>();
    private List<Vector2> hallways = new List<Vector2>();
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
        //GenerateHallwayNode(tree);
        GenerateHallwayNodeBetter(tree);
    }
    private void GenerateHallwayNodeBetter(BspTree node)
    {
        if (node.what_is_internal())
        {
            RectInt leftContainer = node.left.container;
            RectInt rightContainer = node.right.container;
            Vector2Int leftCenter = new Vector2Int(Mathf.CeilToInt(leftContainer.center.x), Mathf.CeilToInt(leftContainer.center.y));
            Vector2Int rightCenter = new Vector2Int(Mathf.CeilToInt(rightContainer.center.x), Mathf.CeilToInt(rightContainer.center.y));
            Vector2 direction = (rightCenter - (Vector2)leftCenter).normalized;
            while (leftCenter!=rightCenter)
            {
                if (direction.Equals(Vector2.right))
                {
                    for (int i = 0; i < hallway; i++)
                    {
                        hallways.Add(new Vector2((int)leftCenter.x, (int)leftCenter.y + i));
                        mapData.tiles[(int)leftCenter.x, (int)leftCenter.y + i] = 1;
                    }
                }
                else if (direction.Equals(Vector2.up))
                {
                    for (int i = 0; i < hallway; i++)
                    {
                        hallways.Add(new Vector2((int)leftCenter.x + i, (int)leftCenter.y));
                        mapData.tiles[(int)leftCenter.x + i, (int)leftCenter.y] = 1;
                    }
                }
                leftCenter.x += (int)direction.x;
                leftCenter.y += (int)direction.y;
            }
            if (node.left != null) GenerateHallwayNodeBetter(node.left);
            if (node.right != null) GenerateHallwayNodeBetter(node.right);
        }
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
            while (Vector2.Distance(leftCenter, rightCenter) >= 1)
            {
                if (direction.Equals(Vector2.right))
                {
                    for (int i = 0; i < hallway; i++)
                    {
                        hallways.Add(new Vector2((int)leftCenter.x, (int)leftCenter.y + i));
                        mapData.tiles[(int)leftCenter.x, (int)leftCenter.y + i] = 1;
                    }
                }
                else if (direction.Equals(Vector2.up))
                {
                    for (int i = 0; i < hallway; i++)
                    {
                        hallways.Add(new Vector2((int)leftCenter.x + i, (int)leftCenter.y));
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
        mapData.tiles = new int[mapSize + 1, mapSize + 1];
        InitReferences();
        GenerateContainer();
        conters(tree);
        GenerateRooms();
        GenerateHallway();
        FillRooms();
        SetDoors();
        rndr.Render(mapData);
        return mapData;
    }
    private void conters(BspTree tree)
    {
        if (tree == null)
        {
            return;
        }
        conters(tree.left);
        containers.Add(tree.container);
        conters(tree.right);
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
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere((Vector2)item.center, 0.3f);
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
            if (showContainers)
            {
                foreach (var item in containers)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireCube(Vector2.Lerp(item.min, item.max, 0.5f), (Vector2)item.size);
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere((Vector2)item.min, 0.3f);
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere((Vector2)item.max, 0.3f);
                }
            }
            if (showHall)
            {
                foreach (var hPos in hallways)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere(hPos + new Vector2(0.5f, 0.5f), 0.3f);
                }
            }
        }
    }
}
