using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : Skill
{
    [SerializeField] private int minDamage, maxDamage;
    public int GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
    public abstract void AttackPosition(LiveEntity attacker, Vector2Int targetPos);
}
