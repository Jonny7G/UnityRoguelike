using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LiveEntity
{
    [HideInInspector]public Entity Player;
    [SerializeField] private ItemPickup dropitem;

    protected virtual void DropItems(TurnHandler turnHndlr)
    {

    }
}
