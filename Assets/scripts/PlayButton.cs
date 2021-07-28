using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("pressed");
        SceneLoader.Instance.LoadGame();
    }
}
