using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortHandler : Enemy
{
    [SerializeField] private int damage;
    [SerializeField] private float chaseDist;
    private bool attackedLast;
    public override void TakeTurn()
    {
        base.TakeTurn();
        if (seen)
        {
            float dist = Vector2.Distance(position, entHandler.player.position);
            
            if (IsPlayerAdjacent())
            {
                if (!attackedLast)
                {
                    entHandler.player.health.Damage(damage);
                    attackedLast = true;
                }
                else
                {
                    attackedLast = false;
                }
            }
            else if (dist < chaseDist && dist > 1.5f)
            {
                Vector2Int move = new Vector2Int(0, 0);

                if ((Mathf.Abs(entHandler.player.position.y - position.y) > Mathf.Abs(entHandler.player.position.x - position.x)))
                {
                    if (entHandler.player.position.y > position.y) { move.y = 1; }
                    else if (entHandler.player.position.y < position.y) { move.y = -1; }
                }
                if ((Mathf.Abs(entHandler.player.position.y - position.y) < Mathf.Abs(entHandler.player.position.x - position.x)))
                {
                    if (entHandler.player.position.x > position.x) { move.x = 1; }
                    else if (entHandler.player.position.x < position.x) { move.x = -1; }
                }
                if ((Mathf.Abs(entHandler.player.position.y - position.y) == Mathf.Abs(entHandler.player.position.x - position.x)))
                {
                    if (entHandler.player.position.x > position.x) { move.x = 1; }
                    else if (entHandler.player.position.x < position.x) { move.x = -1; }
                }

                if (!AttemptMove(position + move))
                {
                    var entity = entHandler.liveEntities.GetEntity(position + move);
                    if (entity == entHandler.player)
                    {
                        DoAttack(move);
                        if (!attackedLast)
                        {
                            entHandler.player.health.Damage(damage);
                            attackedLast = true;
                        }
                        else
                        {
                            attackedLast = false;
                        }
                    }
                }
            }
            else
            {
                MoveToPlayer();
            }
        }
    }
}
