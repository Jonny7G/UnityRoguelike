using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2Int position { get; private set; }
    

    public virtual void TakeTurn(TurnHandler turnHandler)
    {

    }
    public bool AttemptMove(TurnHandler turnHandler, Vector2Int position)
    {
        if (turnHandler.IsOpenTile(position))
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
