using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerEntityHandler : MonoBehaviour
{
    public bool animating { get; private set; }
    public HealthHandler health;
    public LiveEntity entity;
    public PlayerLevel level;
    public Stats playerStats;
    [SerializeField, HideInInspector] private CameraShake camShake;
    private void Awake()
    {
        camShake = FindObjectOfType<CameraShake>();
    }
    private void Start()
    {
        health.OnDeath += OnDeath;
        health.OnDamage += OnDamaged;
        level.OnLevelUp += FullHeal;
        playerStats.OnChange += UpdateHealth;
        entity.shakeMag = 0; //don't want player to shake when damaged
        entity.Show(); //don't hide the player
    }
    private void OnDestroy()
    {
        level.OnLevelUp -= FullHeal;
        playerStats.OnChange -= UpdateHealth;
    }
    private void UpdateHealth()
    {
        FullHeal();
        health.UpdateHealth();
    }
    private void FullHeal()
    {
        health.Heal(health.maxHealth);
    }
    private void OnDeath()
    {
        SceneLoader.Instance.LoadDeathScreen();
    }
    private void OnDamaged()
    {
        Debug.Log("damaged!");
        camShake.Shake();
    }
}
