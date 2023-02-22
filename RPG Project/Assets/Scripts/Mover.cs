using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private NavMeshAgent _playerNavMesh;
    private Animator _characterAnimator;
    
    void Start()
    {
        _playerNavMesh = GetComponent<NavMeshAgent>();
        _characterAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor(); 
        }
        
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            _playerNavMesh.SetDestination(hit.point);
        }
        
        //Debug.DrawRay(ray.origin,ray.direction * 100); 
    }

    private void UpdateAnimator()
    {
        Vector3 characterVelocity = _playerNavMesh.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(characterVelocity);
        float speed = localVelocity.z;
        
        _characterAnimator.SetFloat("ForwardSpeed",speed);
        
        
    }
}
