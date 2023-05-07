using System;
using RPG.Attributes;
using RPG.Combat;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText damageTextPrefab = null;
        
        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    
    }
}