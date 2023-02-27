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
        
        Health _target;
        private float _distance;
        private float _timeSinceLastAttack;

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target == null) return;
            if (_target.IsDead()) return;
            
            if (_target!=null)
            {
                if (!IsInRange())
                {
                    GetComponent<Mover>().MoveTo(_target.transform.position); 
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
            transform.LookAt(_target.transform);
            
            if (_timeSinceLastAttack >= attackCooldown)
            {
                TriggerAttackAnimations();
                _timeSinceLastAttack = 0;
                Hit();
            }
        }

        private void TriggerAttackAnimations()
        {
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }

            Health combatTargetHealth = combatTarget.GetComponent<Health>();

            return combatTargetHealth != null && !combatTargetHealth.IsDead();
        }
        
        // Important !
        // Animation Event called by Animator.
        // Health reducement of target will wait until the animation cycle ends.
        // If a normal method were implemented,damage will applied to target instantly.
        private void Hit()
        {
            if (_target == null) return;
            
            _target.TakeDamage(weaponDamage);
            
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }
        
        public void Cancel()
        {
            StopAttackAnimatons();
            _target = null;
        }

        private void StopAttackAnimatons()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("CancelAttack");
        }
    }
}

