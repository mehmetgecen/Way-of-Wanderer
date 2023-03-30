using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Attributes
{
    public class XPDisplay : MonoBehaviour
    {
        private Experience experience;
    
        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
        
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}",experience.GetPoints());
        }
    }
}

