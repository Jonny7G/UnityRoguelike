using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LiveEntity : Entity
{
    [SerializeField] public HealthHandler health;
    [SerializeField] public LootDropHandler dropHandler;
    [HideInInspector] public float shakeMag = 0.3f;
    [HideInInspector] public int spawnRoom;
    [SerializeField, HideInInspector] private SpriteRenderer sr;

    protected bool seen = false;
    protected bool damagedOnTurn = false;
    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.enabled = false;
    }
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
    public void Show()
    {
        seen = true;
        sr.enabled = true;
    }
    public override void TakeTurn()
    {
        base.TakeTurn();
        if (!seen)
        {
            if (entHandler.map.rooms[spawnRoom].Contains(entHandler.player.position))
            {
                Show();
            }
        }
    }
    public override void AfterTurns()
    {
        base.AfterTurns();
        DoShake();
    }
    public void DoAttack(Vector2 direction)
    {
        moveTween = transform.DOPunchPosition(direction * 0.5f, 0.2f, 0);
    }
    protected virtual void OnDeath()
    {
        if (dropHandler != null)
        {
            dropHandler.DropLoot(this);
        }
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
