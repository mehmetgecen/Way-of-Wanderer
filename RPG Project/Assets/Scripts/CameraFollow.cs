using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    void LateUpdate()
    {
        transform.position = target.transform.position;
    }
}
