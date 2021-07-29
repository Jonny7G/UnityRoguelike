using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerLevel level;
    [SerializeField] private Stats playerStats;
    private bool menuActive;
    private void Start()
    {
        level.OnLevelUp += DoMenu;
    }
    private void OnDestroy()
    {
        level.OnLevelUp -= DoMenu;
    }
    private void Update()
    {
        if (menuActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerStats.Damage++;
                menuActive = false;
                panel.SetActive(false);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerStats.Health++;
                menuActive = false;
                panel.SetActive(false);
            }
        }
    }
    private void DoMenu()
    {
        menuActive = true;
        panel.SetActive(true);
    }
}
