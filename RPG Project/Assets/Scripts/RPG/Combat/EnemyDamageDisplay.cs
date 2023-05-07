using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyDamageDisplay : MonoBehaviour
    {
        private Fighter _fighter;
        
        private void Awake()
        {
            _fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (_fighter.GetTarget()!=null)
            {
                Health enemyHealth = _fighter.GetTarget();

                if (enemyHealth.IsDamageTaken())
                {
                    GetComponent<TextMeshProUGUI>().text = enemyHealth.GetComponent<Fighter>().GetDamage().ToString();
                }
                
            }
            
        }
    }
}

