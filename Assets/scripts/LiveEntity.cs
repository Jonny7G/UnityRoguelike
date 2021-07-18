using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveEntity : Entity
{
    [SerializeField] public HealthHandler health;

    protected virtual void Start()
    {
        health.OnDeath += OnDeath;
    }
    protected virtual void OnDestroy()
    {
        health.OnDeath -= OnDeath;
    }
    protected virtual void OnDeath()
    {
        entHandler.liveEntities.RemoveEntity(this);
    }
}
