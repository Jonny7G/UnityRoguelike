using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillRoom : MonoBehaviour
{
    public LayerMask Proxy; 
    public LevelGeneration levelGen;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, Proxy); //Fills random room
        if (roomDetection == null && levelGen.stopGen == true) //Checks if area is empty and spawns room
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
