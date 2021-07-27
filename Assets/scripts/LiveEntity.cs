using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LiveEntity : Entity
{
    [SerializeField] public HealthHandler health;
    [SerializeField, HideInInspector] protected Tween moveTween;
    private float shakeMag = 0.3f;
    protected bool damagedOnTurn = false;
    protected virtual void Start()
    {
        health.OnDeath += OnDeath;
        health.OnDamage += OnDamage;
    }
    protected virtual void OnDestroy()
    {
        health.OnDeath -= OnDeath;
        health.OnDamage -= OnDamage;
    }
    public override void AfterTurns()
    {
        base.AfterTurns();
        DoShake();
    }
    public void DoAttack(Vector2 direction)
    {
        moveTween = transform.DOPunchPosition(direction*0.5f, 0.2f, 0);
    }
    protected virtual void OnDeath()
    {
        entHandler.liveEntities.RemoveEntity(this);
    }
    protected virtual void OnDamage()
    {
        damagedOnTurn = true;
    }
    protected void DoShake()
    {
        if (damagedOnTurn)
        {
            moveTween = transform.DOShakePosition(0.2f, shakeMag, 30, 90, false, false);
            damagedOnTurn = false;
        }
    }
}
