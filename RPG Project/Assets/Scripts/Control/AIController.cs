using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionDuration = 5f;
        [SerializeField] private float waypointDwellTime = 2f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float patrolMovementSpeed = 2f;
        [SerializeField] private float attackMovementSpeed = 5f;
        [SerializeField] PatrolPath patrolPath;
        
        //Cache References
        private Vector3 _guardPos;
        private GameObject _player;
        private Mover _mover;
        private Fighter _fighter;
        private Health _health;
        
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        
        
        private int _currentWaypointIndex = 0;
        
        private void Start()
        {
            _guardPos = transform.position;
            _player = GameObject.FindWithTag("Player");
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if(_health.IsDead()) return;
            
            if (IsInAttackRange() && _fighter.CanAttack(_player))
            {
                AttackBehaviour();
            }
            
            else if (_timeSinceLastSawPlayer < suspicionDuration)
            {
                SuspicionBehaviour();
            }

            else
            {
                if (patrolPath!= null)
                {
                    PatrolBehaviour();
                }

                else
                {
                    GuardBehaviour();
                }
                
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            GetComponent<NavMeshAgent>().speed = patrolMovementSpeed;
            
            Vector3 nextPosition = _guardPos;
            
            if (patrolPath!= null)
            {
                if (AtWaypoint())
                { 
                    CycleWaypoint();
                    _timeSinceArrivedAtWaypoint = 0;
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint >=waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition);
            }
            
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float joinPathDistance = Vector3.Distance(transform.position,GetCurrentWaypoint());
            return joinPathDistance < waypointTolerance;
        }
        

        private void GuardBehaviour()
        {
            _mover.StartMoveAction(_guardPos);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            GetComponent<NavMeshAgent>().speed = attackMovementSpeed;
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
            
        }

        private bool IsInAttackRange()
        {
            if (_health.IsDamageTaken())
            {
                return true;
            }
            
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
            return distanceToPlayer < chaseDistance;
            

        }

        // Called from Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);

            if (_player != null)
            {
                Gizmos.DrawLine(transform.position,_player.transform.position);  
            }
            
        }
    }
}
