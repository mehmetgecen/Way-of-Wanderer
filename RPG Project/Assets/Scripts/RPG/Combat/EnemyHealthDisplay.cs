using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter _fighter;
        
        private void Awake()
        {
            _fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (_fighter.GetTarget() == null)
            {
                GetComponent<TextMeshProUGUI>().text = "N/A";
            }

            if (_fighter.GetTarget()!=null)
            {
                Health enemyHealth = _fighter.GetTarget();
                GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}/{1:0}",enemyHealth.GetHealthPoints(),enemyHealth.GetMaxHealthPoints());
            }
            
        }
    }
}
    

