using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : Resetable
{
    public int Damage { get => _damage; set { _damage = value; OnChange?.Invoke(); } }
    [SerializeField] private int _damage;
    public int Health { get => _health; set { _health = value; OnChange?.Invoke(); } }
    [SerializeField] private int _health;
    public event System.Action OnChange;
    public override void ResetState()
    {
        Damage = Health = 0;
    }
}
