using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LiveEntity
{
    public override void TakeTurn()
    {
        base.TakeTurn();
        //code for enemy to move their turn. use entHandler to access player position and map data.
    }
    protected bool IsPlayerAdjacent()
    {
        var playerPos = entHandler.player.position;
        return Mathf.Abs(playerPos.x - position.x) <= 1 && playerPos.y - position.y == 0 || Mathf.Abs(playerPos.y - position.y) <= 1 && playerPos.x - position.x == 0;
    }
    protected void MoveToPlayer()
    {
        Vector2Int move = new Vector2Int();
        var playerPos = entHandler.player.position;
        int yDiff = playerPos.y - position.y;
        int xDiff = playerPos.x - position.x;
        if (Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
        {
            move.y = (int)Mathf.Sign(yDiff);
            if (!AttemptMove(position + move))
            {
                move.y = 0;
                move.x = (int)Mathf.Sign(xDiff);
                AttemptMove(position + move);
            }
        }
        else
        {
            move.x = (int)Mathf.Sign(xDiff);
            if (!AttemptMove(position + move))
            {
                move.x = 0;
                move.y = (int)Mathf.Sign(yDiff);
                AttemptMove(position + move);
            }
        }
    }
}
