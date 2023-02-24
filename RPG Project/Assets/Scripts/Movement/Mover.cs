using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction
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
        
        // For Cancelling Attack Operation,
        // There will be no need to reach Fighter script after using IAction Interface. Line 36. (Dependency Inversion)
        // RPG.Combat namespace will not be necessary.
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            //GetComponent<Fighter>().Cancel(); (inefficient line after dependency inversion)
            MoveTo(destination);
        }

        // General Movement Method
        // NavMesh Motion Switches
        public void MoveTo(Vector3 destination)
        {
            _playerNavMesh.destination = destination;
            _playerNavMesh.isStopped = false;
        }

        public void Cancel()
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
