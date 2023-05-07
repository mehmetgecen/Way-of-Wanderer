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
        public void DestroyText()
        {
            Destroy(gameObject);
        }
    }
}

