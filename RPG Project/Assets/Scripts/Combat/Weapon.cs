using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Weapons/Make New Weapon",order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private float weaponRange = 2f;

        public float WeaponRange
        {
            get => weaponRange;
            set => weaponRange = value;
        }

        public float WeaponDamage
        {
            get => weaponDamage;
            set => weaponDamage = value;
        }

        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private AnimatorOverrideController animatorOverride;
    
        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);    
            }

            if (animator!=null)
            {
                animator.runtimeAnimatorController = animatorOverride;    
            }
            
            
        }
    }  
}

