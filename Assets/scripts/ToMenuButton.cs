using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMenuButton : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneLoader.Instance.LoadMainMenu();
    }
}
