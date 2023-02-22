using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private NavMeshAgent _playerNavMesh;
    
    
    void Start()
    {
        _playerNavMesh = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor(); 
        }
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
}
