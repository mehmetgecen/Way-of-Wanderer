using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float projectileSpeed;
    
    private float projectileDamage;
    Health _target;
    void Update()
    {
        transform.LookAt(GetAimPosition());
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    public void SetTarget(Health target,float damage)
    {
        _target = target;
        projectileDamage = damage;
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
        
        Destroy(gameObject);
        other.gameObject.GetComponent<Health>().TakeDamage(projectileDamage);
        
    }
}
