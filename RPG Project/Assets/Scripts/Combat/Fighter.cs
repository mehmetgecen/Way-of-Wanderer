using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float weaponRange = 2f;
        private Transform _target;
        private float _distance;

        private void Update()
        {
            if (_target == null) return;
            
            if (_target!=null)
            {
                if (!IsInRange())
                {
                    GetComponent<Mover>().MoveTo(_target.position); 
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public void Cancel()
        {
            _target = null;
            GetComponent<Animator>().ResetTrigger("Attack");
        }

        // Animation Event called by Animator.
        private void Hit()
        {
            
        }

    }
}

