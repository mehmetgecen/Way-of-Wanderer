using System;
using RPG.Attributes;
using RPG.Combat;
using RPG.Stats;
using TMPro;
using UnityEngine;


namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        public void SetValue(float amount)
        {
            text.text =  String.Format("{0:0}",amount);
        }
    }
}

