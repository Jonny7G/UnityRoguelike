using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2Int position { get; private set; }
    [SerializeField] protected HealthHandler health;
}
