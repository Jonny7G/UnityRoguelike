using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthText : MonoBehaviour
{
    [SerializeField] private HealthHandler health;
    [SerializeField] private TMP_Text text;
    
    private void Update()
    {
        text.SetText("Health: " + health.Health + "/" + health.defaultHealth);
    }
}
