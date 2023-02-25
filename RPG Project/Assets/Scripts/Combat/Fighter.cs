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
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float attackCooldown = 1f;
        
        private Transform _target;
        private float _distance;
        private float _timeSinceLastAttack;

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
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
            if (_timeSinceLastAttack >= attackCooldown)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                _timeSinceLastAttack = 0;
                Hit();
            }
            

        }
        
        // Important !
        // Animation Event called by Animator.
        // Health reducement of target will wait until the animation cycle ends.
        // If a normal method were implemented,damage will applied to target instantly.
        private void Hit()
        {
            Health healthComponent = _target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
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
        
        

    }
}

