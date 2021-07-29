using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Counter", menuName = "Counter", order = 0)]
public class Counter : Resetable
{
    public int DefaultValue = 0;
    public System.Action OnChange;
    public int count { get => _count; private set => _count = value; }
    [SerializeField]private int _count;

    public void Increment()
    {
        count++;
        OnChange?.Invoke();
    }
    public void AddValue(int value)
    {
        count += value;
        OnChange?.Invoke();
    }
    public override void ResetState()
    {
        count = DefaultValue;
        OnChange?.Invoke();
    }
}
