using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerLevel : Resetable
{
    public System.Action OnLevelUp;
    public System.Action XPChange;
    public int MaxLevel;
    public float xpIncreasePerLevel;
    public int levelOneXP;
    public int Level { get => _level; private set => _level = value; }
    [SerializeField] private int _level;
    public int XP { get => _xp; private set => _xp = value; }
    [SerializeField] private int _xp;
    public int levelXP { get => _levelXP; private set => _levelXP = value; }
    [SerializeField, ReadOnly] private int _levelXP;
    public void AddXP(int xpAmount)
    {
        XP += xpAmount;
        if (XP >= levelXP)
        {
            if (Level < MaxLevel)
            {
                LevelUp();
            }
            else
            {
                XP = levelXP;
            }
        }
        XPChange?.Invoke();
    }
    public void LevelUp()
    {
        Level++;
        levelXP = Mathf.RoundToInt(levelXP * xpIncreasePerLevel);
        XP = 0;
        OnLevelUp?.Invoke();
    }
    public override void ResetState()
    {
        XP = 0;
        levelXP = levelOneXP;
        Level = 1;
    }
}
