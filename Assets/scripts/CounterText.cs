using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CounterText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Counter counter;
    [SerializeField] private string headerText;

    private void Awake()
    {
        counter.OnChange += SetText;
    }
    private void OnDestroy()
    {
        counter.OnChange -= SetText;
    }
    private void SetText()
    {
        text.text = headerText + counter.count.ToString();
    }
}
