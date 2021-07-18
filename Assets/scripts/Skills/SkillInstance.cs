using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillInstance //instance for the entity
{
    public Skill skill; //skill asset instance uses
    public int cooldown;
    public int currentCooldown { get => _currentCooldown; private set => _currentCooldown = value; }
    [SerializeField,ReadOnly] private int _currentCooldown;
    public bool canUse => currentCooldown == 0;
    public void StartCooldown()
    {
        currentCooldown = cooldown;
    }
    public void UpdateCooldown()
    {
        Debug.Log("updating cooldown");
        if (currentCooldown > 0)
        {
            currentCooldown--;
        }
    }
}
