using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Core;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private UnityEvent onProjectileHit;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private bool isHoming;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private float maxLifetime = 10f;
            
        
        float _projectileDamage;
        Health _target = null;
        GameObject instigator = null;
    
        private void Start()
        {
            transform.LookAt(GetAimPosition());    
        }
    
        void Update()
        {
            if (isHoming && !_target.IsDead())
            {
                transform.LookAt(GetAimPosition()); 
            }
            
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    
        public void SetTarget(Health target,GameObject instigator,float damage)
        {
            _target = target;
            _projectileDamage = damage;
            this.instigator = instigator;
            
            Destroy(gameObject,maxLifetime);
        }
        
        private Vector3 GetAimPosition()
        {
            CapsuleCollider targetCapsule = _target.GetComponent<CapsuleCollider>();
    
            if (targetCapsule == null)
            {
                return _target.gameObject.transform.position;
            }
            return _target.gameObject.transform.position + (Vector3.up * targetCapsule.height / 2);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            print("Arrow");
            
            if (other.GetComponent<Health>() != _target) return;
            if(_target.IsDead()) return;
            
            _target.TakeDamage(instigator,_projectileDamage);
            
            projectileSpeed = 0;
            onProjectileHit.Invoke();
            Destroy(gameObject);
            
            if (gameObject.name.Contains("Fireball")) // will be edited for performance issues
            {
                Instantiate(hitEffect, GetAimPosition(), Quaternion.identity);
                
            }
            
        }
        
    }

}
