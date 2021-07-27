using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public int Health { get => _health; private set { _health = value; } }
    [SerializeField] private int _health;
    public event System.Action OnDeath;
    public event System.Action OnDamage;
    private int defaultHealth;
    private void Start()
    {
        defaultHealth = Health;
    }
    public void Heal(int amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, defaultHealth);
    }
    public void Damage(int damage)
    {
        if (Health > 0) //no way to kill twice
        {
            Health -= damage;
            OnDamage?.Invoke();
            if (Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
