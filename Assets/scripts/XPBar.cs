using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] private PlayerLevel level;
    [SerializeField] private Slider xpSlider;
    private void Start()
    {
        xpSlider.value = 0;
        level.XPChange += UpdateBar;
    }
    private void OnDestroy()
    {
        level.XPChange -= UpdateBar;
    }
    private void UpdateBar()
    {
        if (xpSlider != null)
        {
            Debug.Log("increasing!");
            xpSlider.value = level.XP / (float)level.levelXP;
        }
    }
}
