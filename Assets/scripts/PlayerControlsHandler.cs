﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerEntityHandler player;
    [SerializeField] private PlayerSkillsHandler skillsHandler;
    [SerializeField] private EntitiesHandler entHandler;

    [SerializeField, HideInInspector] private Vector2Int facingDirection;
    private void Update()
    {
        if (!gameManager.loading)
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
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                move.y = 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                move.y = -1;
            }
            if (Input.GetKeyDown(KeyCode.R)) //DETH
            {
                player.health.Damage(1000);
            }
            if (Input.GetKeyDown(KeyCode.Y)) //test damage
            {
                player.health.Damage(1);
            }
            bool moved = move.magnitude > 0;
            facingDirection = move;
            if (moved)
            {
                if (!player.entity.AttemptMove(player.entity.position + move))
                {
                    var entity = entHandler.liveEntities.GetEntity(player.entity.position + move);
                    if (entity != null)
                    {
                        skillsHandler.Attack(facingDirection);
                    }
                }
                MoveTurn();
            }
        }
    }
    private void MoveTurn()
    {
        skillsHandler.MoveTurn();
        entHandler.MoveTurn(); //makes every active entity move their turn, important it happens after player turn.
    }
}
