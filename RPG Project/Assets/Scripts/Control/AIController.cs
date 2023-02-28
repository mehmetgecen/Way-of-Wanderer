using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] private float suspicionDuration = 5f;
        
        //Cache References
        private Vector3 _guardPos;
        private GameObject _player;
        private Mover _mover;
        private Fighter _fighter;
        private Health _health;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        
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
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            
            else if (timeSinceLastSawPlayer < suspicionDuration)
            {
                SuspicionBehaviour();
            }

            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;

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
            _fighter.Attack(_player);
        }

        private bool IsInAttackRange()
        {
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
