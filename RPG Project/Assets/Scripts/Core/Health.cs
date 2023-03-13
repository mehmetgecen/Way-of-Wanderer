using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] float health = 20f;
        [SerializeField] private float startHealth;
        
        bool _isDead = false;

        private void Start()
        {
            startHealth = health;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            
            if (health==0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) return;
            
            _isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }

        public bool isDamageTaken()
        {
            return health < startHealth;
        }
        public bool IsDead()
        {
            return _isDead;
        }

        // Save Health Value
        
        public object CaptureState()
        {
            return health;
        }

        // Load Health Value
        
        public void RestoreState(object state)
        {
            health = (float) state;

            if (health == 0 )
            {
                Die();
            }
        }
    }  
}

