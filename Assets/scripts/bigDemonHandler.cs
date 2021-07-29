using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigDemonHandler : Enemy
{
    [SerializeField] private int damage;
    [SerializeField] private float chaseDist;
    public override void TakeTurn()
    {
        base.TakeTurn();
        if (seen)
        {
            Vector2Int move = new Vector2Int(0, 0);
            float dist = Vector2.Distance(position, entHandler.player.position);
            var playerPos = entHandler.player.position;
            if (IsPlayerAdjacent())
            {
                entHandler.player.health.Damage(damage);
            }
            else if( dist < chaseDist && dist > 1.5f)
            {
                for (int i = 0; i < 2; i++)
                {
                    int rand_num = Random.Range(0, 4);

                    if (rand_num < 1) { move.y = -1; }
                    if (rand_num >= 1 && rand_num < 2) { move.y = 1; }
                    if (rand_num >= 3) { move.x = -1; }
                    if (rand_num >= 2 && rand_num < 3) { move.x = 1; }

                    AttemptMove(position + move);

                    if ((Mathf.Abs(entHandler.player.position.y - position.y) + Mathf.Abs(entHandler.player.position.y - position.x)) <= 2)
                    {
                        entHandler.player.health.Damage(damage);
                        DoAttack(((Vector2)entHandler.player.position - position).normalized);
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


