using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            // String Format can be used alternatively.
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}/{1:0}",_health.GetHealthPoints(),_health.GetMaxHealthPoints());
        }
    }
}

