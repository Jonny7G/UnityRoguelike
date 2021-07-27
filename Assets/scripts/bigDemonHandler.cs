using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigDemonHandler : Enemy
{
    public override void TakeTurn()
    {
        Vector2Int move = new Vector2Int(0,0);

        int rand_num = Random.Range(0,4);

        if (rand_num < 1) {move.y = -1;}
        if (rand_num >= 1 && rand_num < 2) {move.y = 1;}
        if (rand_num >= 3) {move.x = -1;}
        if (rand_num >= 2 && rand_num < 3) { move.x = 1; }

        Vector2Int test = new Vector2Int(0, 0);
        test.y = (int)gameObject.transform.position.y;
        test.x = (int)gameObject.transform.position.x;

        if(AttemptMove(test + move))
        {
        AttemptMove(test + move);
        }
    }
}


