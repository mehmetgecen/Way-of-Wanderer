using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour,ISaveable
    {
        [SerializeField] private UnityEvent<float> damageTaken;
        [SerializeField] private UnityEvent onDie;
        
        private GameObject _instigator = null;
        private bool _isDead = false;
        
        float health = -1f;
        float startHealth;
        float regenerationPercentage = 70;
        
        
         private void Start()
         {
             if (health<0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.Heatlh);    
            }
            
            startHealth = health;
        }

         private void OnEnable()
         {
             GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;   
         }

         private void OnDisable()
         {
             GetComponent<BaseStats>().OnLevelUp -= RegenerateHealth;
         }

         public void TakeDamage(GameObject instigator,float damage)
         {
             print(gameObject.name + " took damage: " + damage);
            
            health = Mathf.Max(health - damage, 0);
            
            if (health==0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }

            else
            {
                damageTaken.Invoke(damage);
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

        public float GetHealthPoints()
        {
            return health;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Heatlh);
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

