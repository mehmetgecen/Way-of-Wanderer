using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] float health = 20f;
        [SerializeField] private float startHealth;

        private GameObject instigator = null;
        
        bool _isDead = false;
        

         private void Start()
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Heatlh);
            startHealth = health;
        }

        public void TakeDamage(GameObject instigator,float damage)
        {
            health = Mathf.Max(health - damage, 0);
            
            if (health==0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void Die()
        {
            if (_isDead) return;
            
            _isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
            
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();

            if (experience == null) return;
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public bool isDamageTaken()
        {
            return health < startHealth;
        }
        public bool IsDead()
        {
            return _isDead;
        }

        public float GetPercentage()
        {
            return (health / startHealth) * 100;
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

