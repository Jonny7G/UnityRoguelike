using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HealthHandler))]
public class EnemyXPHandler : MonoBehaviour
{
    public PlayerLevel pLevel;
    public int deathXP;
    [SerializeField, HideInInspector] private HealthHandler health;
    private void Awake()
    {
        health = GetComponent<HealthHandler>();
    }
    private void Start()
    {
        health.OnDeath += AddXP;
    }
    private void OnDestroy()
    {
        health.OnDeath -= AddXP;
    }
    private void AddXP()
    {
        pLevel.AddXP(deathXP);
    }
}
