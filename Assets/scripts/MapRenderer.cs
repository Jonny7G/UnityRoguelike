using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapRenderer : MonoBehaviour
{
    [SerializeField] private int edgeThickness;
    [Header("Tile maps")]
    [SerializeField] private Tilemap walls, ground;
    [Header("Tiles")]
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase exitTile;
    private void Awake()
    {
        ClearTiles();
    }
    private void ClearTiles()
    {
        walls.ClearAllTiles();
        ground.ClearAllTiles();
    }
    private void FillWall(Vector2Int min, Vector2Int max)
    {
        for (int x = min.x; x < max.x; x++)
        {
            for (int y = min.y; y < max.y; y++)
            {
                walls.SetTile(new Vector3Int(x, y - 1, 0), wallTile);
            }
        }
    }
    public void Render(MapData data)
    {
        //fill in with all walls before 
        FillWall(new Vector2Int(-edgeThickness, -edgeThickness), new Vector2Int(data.tiles.GetLength(0) + edgeThickness, data.tiles.GetLength(0) + edgeThickness));

        for (int x = 0; x < data.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < data.tiles.GetLength(1); y++)
            {
                if (data.tiles[x, y] == 1)
                {
                    var pos = new Vector3Int(x, y, 0);
                    walls.SetTile(pos, null); //remove wall
                    ground.SetTile(pos, groundTile); //place ground tile on ground tilemap
                }
            }
        }
        ground.SetTile((Vector3Int)data.exit, exitTile);
        //ground.SetTile(new Vector3Int(-10, -10, 0), groundTile);
        //walls.SetTile(new Vector3Int(-11, -10, 0), wallTile);
    }
}
