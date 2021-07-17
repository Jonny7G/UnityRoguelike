using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    public GameObject[] rooms; // 0 LR, 1 LRB, 2 LRT, 3 LRTB

    private int direction;
    public float moveAmount;

    private float timeRoom;
    public float startTimeRoom = 0.25f;

    //Generation constraints
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGen;

    public LayerMask room;

    private int downCounter; //Counts amount of down paths

    void Start()
    {
        //Set random spawn point for first room
        int randStartingPos = Random.Range(0, startingPositions.Length); 
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    //Room placement
    private void Move()
    {
        if (direction == 1 || direction == 2) //Move Right
     { 
            if (transform.position.x < maxX)
            {
                downCounter = 0; //reset down counter
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); //Spawn random room
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); //Keep from stacking
                if (direction == 3)
                {
                    direction = 2; //Place right
                }
                else if (direction == 4)
                {
                    direction = 5; //Place down
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) //Move Left
        { 
            if (transform.position.x > minX)
            {
                downCounter = 0; //reset down counter
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); //Spawn random room
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6); //Keep from stacking
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5) //Move Down
        {
            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room); //Stops dead-ends

                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2) //Stops the 2 down tiles from creating a dead end?
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestroy();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestroy();
                        int randBottomRoom = Random.Range(1, 4); //Replace with one that doesnt create dead end
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity); //Place room
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6); //Keep from stacking
            }
            else
            {
                stopGen = true; //Stops generation
            }
        }

        
        
    }
    
    void Update()
    {
        //Rate
        if (timeRoom <= 0 && stopGen == false)
        {
            Move();
            timeRoom = startTimeRoom;
        }
       else 
        {
            timeRoom -= Time.deltaTime;
        }
    }
}
