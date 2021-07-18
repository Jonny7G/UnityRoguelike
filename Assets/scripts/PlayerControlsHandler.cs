using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsHandler : MonoBehaviour
{
    [SerializeField] private LiveEntity player;
    [SerializeField] private PlayerSkillsHandler skillsHandler;
    [SerializeField] private EntitiesHandler entHandler;

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
            if (!player.AttemptMove(player.position + move))
            {
                var entity = entHandler.liveEntities.GetEntity(player.position + move);
                if (entity != null)
                {
                    skillsHandler.Attack(player, player.position + move);
                }
            }
            MoveTurn();
        }
    }
    private void MoveTurn()
    {
        skillsHandler.MoveTurn();
        entHandler.MoveTurn(); //makes every active entity move their turn, important it happens after player turn.
    }
}
