using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private LiveEntity target;
    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + new Vector2(0.5f, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
