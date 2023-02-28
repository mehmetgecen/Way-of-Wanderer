using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        //Cache References
        
        private GameObject player; 
        private Fighter fighter;
        private Health _health;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if(_health.IsDead()) return;
            
            if (IsInAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }

            else
            {
                fighter.Cancel();
            }
            
        }

        private bool IsInAttackRange()
        {
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            
            Gizmos.DrawWireSphere(transform.position,chaseDistance);

            if (player != null)
            {
                Gizmos.DrawLine(transform.position,player.transform.position);  
            }
            
        }
    }
}
