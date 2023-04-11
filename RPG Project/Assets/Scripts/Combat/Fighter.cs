using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private Weapon defaultWeapon = null;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        
        private Weapon _currentWeapon = null;
        private Health _target;
        
        private float _distance;
        private float _timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            if (_currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);    
            }
        }

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
        
        public void EquipWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform,leftHandTransform,animator);
            
        }

        public Health GetTarget()
        {
            return _target;
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
            
            var statDamage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            
            if (_currentWeapon.HasProjectile())
            {
                _currentWeapon.LaunchProjectile(rightHandTransform,leftHandTransform,_target,gameObject,statDamage);
            }
            else
            {
                _target.TakeDamage(gameObject,statDamage);
                
                // _target.TakeDamage(gameObject,_currentWeapon.WeaponDamage);
            }
            
        }

        void Shoot()
        {
            Hit();
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) < _currentWeapon.WeaponRange;
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

        public object CaptureState()
        {
            return _currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}

