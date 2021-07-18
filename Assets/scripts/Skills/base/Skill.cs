using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    [TextArea] public string skillDescription;
}
