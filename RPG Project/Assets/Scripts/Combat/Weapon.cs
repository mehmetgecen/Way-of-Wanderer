using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Weapons/Make New Weapon",order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile = null;

        private const string weaponName = "Weapon";

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
        
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DisableOldWeapon(rightHand,leftHand);
            
            if (weaponPrefab != null)
            {
                var handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon =  Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }

            if (animator!=null)
            {
                animator.runtimeAnimatorController = animatorOverride;    
            }
            
            
        }

        void DisableOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }

            if (oldWeapon == null) return;
            
            
            oldWeapon.name = "Destroyed";
            Destroy(oldWeapon.gameObject);
            
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand,Transform leftHand,Health target,GameObject instigator)
        {
            if (projectile!=null)
            {
                Projectile projectileInstance = Instantiate(projectile,GetTransform(rightHand,leftHand).position,Quaternion.identity);
                projectileInstance.SetTarget(target,instigator,weaponDamage);
            }
            
        }
    }  
}

