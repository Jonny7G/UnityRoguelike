using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerEntityHandler player;
    [SerializeField] private PlayerAttackHandler attackHandler;
    [SerializeField] private EntitiesHandler entHandler;
    [SerializeField] private float initialHoldTime, continuousHoldTime;
    private Vector2Int facingDirection;
    private Timer holdMoveTime;

    private void Update()
    {
        if (!gameManager.loading)
        {
            Vector2Int move = new Vector2Int();
            bool hold = false;
            if (CheckMoveDirection(KeyCode.LeftArrow, ref hold))
            {
                move.x = -1;
            }
            else if (CheckMoveDirection(KeyCode.RightArrow, ref hold))
            {
                move.x = 1;
            }
            else if (CheckMoveDirection(KeyCode.UpArrow, ref hold))
            {
                move.y = 1;
            }
            else if (CheckMoveDirection(KeyCode.DownArrow, ref hold))
            {
                move.y = -1;
            }
            if (Input.GetKeyDown(KeyCode.R)) //quick restart
            {
                player.health.Damage(1000);
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Y)) //cheat while testing in editor
            {
                player.health.Heal(100);
            }
#endif
            bool moved = move.magnitude > 0;
            facingDirection = move;
            if (moved)
            {
                if (player.entity.AttemptMove(player.entity.position + move))
                {
                    MoveTurn();
                }
                else if (!hold)
                {
                    var entity = entHandler.liveEntities.GetEntity(player.entity.position + move);
                    if (entity != null)
                    {
                        attackHandler.Attack(entity);
                    }
                    MoveTurn();
                }
            }
        }
    }
    private bool CheckMoveDirection(KeyCode key, ref bool hold)
    {
        if (Input.GetKeyDown(key))
        {
            holdMoveTime = new Timer(initialHoldTime);
            return true;
        }
        else if (Input.GetKey(key))
        {
            hold = true;
            if (holdMoveTime.CheckUpdateTimer())
            {
                holdMoveTime.SetDuration(continuousHoldTime);
                holdMoveTime.RestartTimer();
                return true;
            }
        }
        return false;
    }
    private void MoveTurn()
    {
        entHandler.MoveTurn(); //makes every active entity move their turn, important it happens after player turn.
    }
}
