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
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private float attackCooldown = 2f;
        
        Health _target;
        private float _distance;
        private float _timeSinceLastAttack = Mathf.Infinity;

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
                _timeSinceLastAttack = 0;
                TriggerAttackAnimations();
                print("Attack Behaviour Called.");
            }
        }

        private void TriggerAttackAnimations()
        {
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        // Attack function must be generic in order to use by AI.
        // Player has no combatTarget component.
        // Our attacks are triggering by mouse click input.
        // AI cant click so we need to update our generic attack behaviour.
        // CombatTarget types converted to GameObject.
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health combatTargetHealth = combatTarget.GetComponent<Health>();
            
            return combatTargetHealth != null && !combatTargetHealth.IsDead();
        }
        
        public void Attack(GameObject combatTarget)
        {
            _target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }
        
        
        // Animation Event called by Unity
        private void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(weaponDamage);
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) < weaponRange;
        }
        
        public void Cancel()
        {
            StopAttackAnimatons();
            _target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttackAnimatons()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("CancelAttack");
        }
    }
}

