using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
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

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _playerNavMesh.destination = destination;
            _playerNavMesh.isStopped = false;
        }

        public void Stop()
        {
            _playerNavMesh.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 characterVelocity = _playerNavMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(characterVelocity);
            float speed = localVelocity.z;

            _characterAnimator.SetFloat("ForwardSpeed", speed);


        }
    }
}
