using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        private void Update()
        {
            transform.forward = Camera.main.transform.forward;

            // var rotation = Camera.main.transform.rotation;
            // transform.LookAt(transform.position + rotation * Vector3.forward,rotation * Vector3.up);
        }
    } 
}

