using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    public int minDamage, maxDamage;
    public Stats playerStats;

    public void Attack(LiveEntity entity)
    {
        entity.health.Damage(Random.Range(minDamage + playerStats.Damage, maxDamage + playerStats.Damage));
    }
}
