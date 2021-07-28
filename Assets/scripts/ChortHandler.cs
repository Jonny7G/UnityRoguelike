using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortHandler : Enemy
{
    GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
    }
    
    public override void TakeTurn()
    {
        base.TakeTurn();
        if (seen)
        {
            Vector2Int move = new Vector2Int(0, 0);
            Vector2Int test = new Vector2Int(0, 0);

            if ((Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) > Mathf.Abs(player.transform.position.x - gameObject.transform.position.x)))
            {
                if (player.transform.position.y > gameObject.transform.position.y) { move.y = 1; }
                else if (player.transform.position.y < gameObject.transform.position.y) { move.y = -1; }
            }
            if ((Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) < Mathf.Abs(player.transform.position.x - gameObject.transform.position.x)))
            {
                if (player.transform.position.x > gameObject.transform.position.x) { move.x = 1; }
                else if (player.transform.position.x < gameObject.transform.position.x) { move.x = -1; }
            }
            if ((Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) == Mathf.Abs(player.transform.position.x - gameObject.transform.position.x)))
            {
                if (player.transform.position.x > gameObject.transform.position.x) { move.x = 1; }
                else if (player.transform.position.x < gameObject.transform.position.x) { move.x = -1; }
            }

            test.y = (int)gameObject.transform.position.y;
            test.x = (int)gameObject.transform.position.x;

            if (AttemptMove(test + move))
            {
                AttemptMove(test + move);
            }
        }
    }
}
