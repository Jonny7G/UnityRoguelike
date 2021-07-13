using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsHandler : MonoBehaviour
{
    [SerializeField] private Entity player;
    [SerializeField] private TurnHandler turnHandler;
    private void Update()
    {
        Vector2Int move = new Vector2Int();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move.x = 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move.y = -1;
        }
        bool moved = move.magnitude > 0;
        if (moved)
        {
            player.AttemptMove(turnHandler, player.position + move);
            turnHandler.MoveTurn();
        }
    }
}
