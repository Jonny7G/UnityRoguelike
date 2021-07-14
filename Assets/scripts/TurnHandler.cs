using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    public EntitiesHandler entities;
    public MapData map;
    public void SetMap(MapData map)
    {
        this.map = map;
    }
    public void MoveTurn()
    {
        entities.MoveTurn(this);
    }
    public bool IsOpenTile(Vector2Int position)
    {
        if (position.x >= 0 && position.y >= 0 && position.x < map.tiles.GetLength(0) && position.y < map.tiles.GetLength(1))
        {
            return map.tiles[position.x, position.y] == 1 && !entities.liveEntities.HasEntity(position);
        }
        else
        {
            return false;
        }
    }
}
