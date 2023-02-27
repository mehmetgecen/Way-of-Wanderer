using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
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
        
        private void Start()
        {
            GameObject player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (gameObject.GetComponent<Health>().IsDead() && player.GetComponent<Health>().IsDead())
            {
                return;
            }
            
            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }

            else
            {
                fighter.Cancel();
            }
            
        }

        private bool IsInAttackRange(GameObject targetPlayer)
        {
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, targetPlayer.transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}
