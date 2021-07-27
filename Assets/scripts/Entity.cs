using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2Int position { get; private set; }
    public EntitiesHandler entHandler = null; //used when an entity creates entities or needs to determine its turn

    public virtual void TakeTurn()
    {
    }
    public virtual void AfterTurns()
    {

    }
    public bool AttemptMove(Vector2Int position)
    {
        if (entHandler.IsOpenTile(position))
        {
            SetPosition(position);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetPosition(Vector2Int pos)
    {
        this.position = pos;
        transform.position = new Vector3(pos.x + 0.5f, pos.y, 0);
    }
}
