﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerEntityHandler : MonoBehaviour
{
    public bool animating { get; private set; }
    public HealthHandler health;
    public LiveEntity entity;
    [SerializeField, HideInInspector] private CameraShake camShake;
    private void Awake()
    {
        camShake = FindObjectOfType<CameraShake>();
    }
    private void Start()
    {
        health.OnDeath += OnDeath;
        health.OnDamage += OnDamaged;
        //entity.shakeMag = 0;
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
