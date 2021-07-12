using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(MapGenerator))]
public class MapRenderer : MonoBehaviour
{
    [Header("Tile maps")]
    [SerializeField] private Tilemap walls, ground;
    [Header("Tiles")]
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase groundTile;

    private void Awake()
    {
        ClearTiles();
    }
    private void ClearTiles()
    {
        walls.ClearAllTiles();
        ground.ClearAllTiles();
    }
    public void Render(MapData data)
    {
        for (int x = 0; x < data.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < data.tiles.GetLength(1); y++)
            {
                if (data.tiles[x, y] == 0)
                {
                    walls.SetTile(new Vector3Int(x, y, 0), wallTile); //place wall tile using the rule tile on wall tilemap
                }
                else
                {
                    ground.SetTile(new Vector3Int(x, y, 0), groundTile); //place ground tile on ground tilemap
                }
            }
        }
    }
}
