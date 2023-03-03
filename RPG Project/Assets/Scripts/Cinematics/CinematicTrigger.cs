using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        public event Action<float> onFinish;

        private bool _alreadyTriggered = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_alreadyTriggered)
            {
                _alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
                
                // GetComponent<BoxCollider>().isTrigger = false;
            }
            
            
        }
        
    } 
}


