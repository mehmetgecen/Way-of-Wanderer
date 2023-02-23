using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        private NavMeshAgent _playerNavMesh;
        private Animator _characterAnimator;

        void Start()
        {
            _playerNavMesh = GetComponent<NavMeshAgent>();
            _characterAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        
        // This Method Interrupts Combat and Starts Movement
        // Special for Combat -> Movement Transition.
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        // General Movement Method
        // NavMesh Motion Switches
        public void MoveTo(Vector3 destination)
        {
            _playerNavMesh.destination = destination;
            _playerNavMesh.isStopped = false;
        }

        public void Stop()
        {
            _playerNavMesh.isStopped = true;
        }

        // Animation Velocity Equalized to NavMesh Velocity
        // InverseTransformDirection turns Worldspace (Global) Velocity to Local Velocity relative to NavMesh Agent.
        
        private void UpdateAnimator()
        {
            Vector3 characterVelocity = _playerNavMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(characterVelocity);
            float speed = localVelocity.z;

            _characterAnimator.SetFloat("ForwardSpeed", speed);


        }
    }
}
