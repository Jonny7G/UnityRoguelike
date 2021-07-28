using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : Entity
{
    public override void TakeTurn()
    {
        base.TakeTurn();
        if(entHandler.player.position == position)
        {
            FindObjectOfType<GameManager>().LoadNewLevel();
        }
    }
}
