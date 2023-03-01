using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 20f;
        
        bool _isDead = false;

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
        
        public bool IsDead()
        {
            return _isDead;
        }
    }  
}

