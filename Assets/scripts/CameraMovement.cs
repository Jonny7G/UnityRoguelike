using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    private void LateUpdate()
    {
        transform.position = target.position + new Vector3(0, 0, -10);
    }
}
