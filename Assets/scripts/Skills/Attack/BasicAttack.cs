using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BasicAttack : Attack
{
    public override void AttackPosition(LiveEntity attacker, Vector2Int targetPos)
    {
        var target = attacker.entHandler.liveEntities.GetEntity(targetPos);
        if (target != null)
        {
            target.health.Damage(GetDamage());
        }
    }
}
