using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public int Health { get => _health; private set { _health = value; } }
    [SerializeField] private int _health;

    public void Damage(int damage)
    {
        Health -= damage;
    }
}
