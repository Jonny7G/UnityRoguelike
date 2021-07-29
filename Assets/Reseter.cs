using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    public List<Resetable> resetableObjects;

    private void Awake()
    {
        ResetAll();
    }
    public void ResetAll()
    {
        foreach (var item in resetableObjects)
        {
            item?.ResetState();
        }
    }
}
