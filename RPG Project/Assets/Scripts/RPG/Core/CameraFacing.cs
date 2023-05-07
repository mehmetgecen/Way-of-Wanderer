using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFacing : MonoBehaviour
{
    private TextMeshProUGUI healthText;

    private void LateUpdate()
    {
        var rotation = Camera.main.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward,rotation * Vector3.up);
    }
}
