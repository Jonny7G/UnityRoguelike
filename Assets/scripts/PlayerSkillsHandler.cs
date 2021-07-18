using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsHandler : MonoBehaviour
{
    public SkillInstance[] activeSkills = new SkillInstance[4];
    public List<SkillInstance> inactiveSkills = new List<SkillInstance>();
    [SerializeField] protected LiveEntity player;
    [SerializeField] protected Attack defaultAttack = default;

    protected SkillInstance currSkill;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("pressed");
            currSkill = activeSkills[0];
            TryDoSkillUtility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currSkill = activeSkills[1];
            TryDoSkillUtility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currSkill = activeSkills[2];
            TryDoSkillUtility();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currSkill = activeSkills[3];
            TryDoSkillUtility();
        }
    }
    public void MoveTurn()
    {
        UpdateSkills();
    }
    protected void UpdateSkills()
    {
        foreach (SkillInstance skill in activeSkills)
        {
            skill.UpdateCooldown();
        }
    }
    public void SetSkill(int index, SkillInstance newSkill)
    {
        if (inactiveSkills.Contains(newSkill))
        {
            inactiveSkills.Remove(newSkill);
        }
        if (activeSkills[index] != null)
        {
            inactiveSkills.Add(activeSkills[index]);
        }
        activeSkills[index] = newSkill;
    }
    public bool TryDoSkillUtility()
    {
        if (currSkill != null && currSkill.skill is UtilitySkill)
        {
            UtilitySkill utilSkill = (UtilitySkill)currSkill.skill;
            utilSkill.UseSkill(player);
            currSkill.StartCooldown();
            Debug.Log("used");
            currSkill = null;
            return true;
        }
        return false;
    }
    public void Attack(LiveEntity player, Vector2Int attackPos)
    {
        Attack currAttack = defaultAttack;
        if (currSkill != null && currSkill.skill is Attack)
        {
            currAttack = (Attack)currSkill.skill;
        }
        currAttack.AttackPosition(player, attackPos);
        if (currSkill != null && currAttack == currSkill.skill)
        {
            currSkill.StartCooldown();
        }
    }
}
