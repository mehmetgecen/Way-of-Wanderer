using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 40f;
        public bool isDead = false;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
            
            if (health==0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            
            GetComponent<Animator>().SetTrigger("Die");
            isDead = true;
        }
    }  
}

