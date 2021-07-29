using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelText : MonoBehaviour
{
    [SerializeField] private PlayerLevel level;

    [SerializeField, HideInInspector] private TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        level.OnLevelUp += UpdateText;
        UpdateText();
    }
    private void OnDestroy()
    {
        level.OnLevelUp -= UpdateText;
    }
    private void UpdateText()
    {
        text.SetText("Level: " + level.Level.ToString());
    }
}
