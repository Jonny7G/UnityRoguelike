using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinHandler : Enemy
{
    [SerializeField] private int damage;
    public override void TakeTurn()
    {
        base.TakeTurn();
        if (seen)
        {
            Vector2Int move = new Vector2Int(0, 0);

            if ((Mathf.Abs(entHandler.player.position.y - position.y) + Mathf.Abs(entHandler.player.position.y - position.x)) > 6)
            {
                int rand_num = Random.Range(0, 4);

                if (rand_num < 1) { move.y = -1; }
                if (rand_num >= 1 && rand_num < 2) { move.y = 1; }
                if (rand_num >= 3) { move.x = -1; }
                if (rand_num >= 2 && rand_num < 3) { move.x = 1; }

                if (!AttemptMove(position + move))
                {
                    var entity = entHandler.liveEntities.GetEntity(position + move);
                    if (entity == entHandler.player)
                    {
                        DoAttack(position + move);
                        entHandler.player.health.Damage(damage);
                    }
                }
            }
            if ((Mathf.Abs(entHandler.player.position.y - position.y) + Mathf.Abs(entHandler.player.position.x - position.x)) <= 6)
            {
                for (int i = 0; i < 2; i++)
                {
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
                            DoAttack(position + move);
                            entHandler.player.health.Damage(damage);
                        }
                    }
                }
            }
        }
    }
}
