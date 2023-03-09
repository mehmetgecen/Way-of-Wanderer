using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float projectileSpeed;
    [SerializeField] private bool isHoming;
    [SerializeField] private GameObject hitEffect = null;
    
    
    float _projectileDamage;
    Health _target;

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

    public void SetTarget(Health target,float damage)
    {
        _target = target;
        _projectileDamage = damage;
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
        if (other.GetComponent<Health>() != _target) return;
        if(_target.IsDead()) return;
        
        Destroy(gameObject);
        other.gameObject.GetComponent<Health>().TakeDamage(_projectileDamage);

        if (gameObject.name.Contains("Fireball"))
        {
            Instantiate(hitEffect, GetAimPosition(), Quaternion.identity);
            
        }
        
        
        
        
        
    }
}
