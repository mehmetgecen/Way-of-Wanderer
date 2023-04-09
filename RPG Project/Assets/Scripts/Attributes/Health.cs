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
        float health = -1f;
        float startHealth;
        float regenerationPercentage = 70;

        private GameObject _instigator = null;
        private bool _isDead = false;
        

         private void Start()
         {
             GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            
            if (health<0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.Heatlh);    
            }
            
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

        public bool IsDamageTaken()
        {
            return health < startHealth;
        }
        public bool IsDead()
        {
            return _isDead;
        }

        public float GetPercentage()
        {
            return (health / GetComponent<BaseStats>().GetStat(Stat.Heatlh) * 100);
        }

        public void RegenerateHealth()
        {
            float regenHealth = GetComponent<BaseStats>().GetStat(Stat.Heatlh) * (regenerationPercentage / 100);
            health = Mathf.Max(health, regenHealth);
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

