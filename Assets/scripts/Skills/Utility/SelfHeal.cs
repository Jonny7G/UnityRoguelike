using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SelfHeal : UtilitySkill
{
    public int minHeal, maxHeal;
    public int GetHeal()
    {
        return Random.Range(minHeal, maxHeal);
    }
    public override void UseSkill(LiveEntity user)
    {
        user.health.Heal(GetHeal());
    }
}
