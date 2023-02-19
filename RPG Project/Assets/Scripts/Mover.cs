using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        _playerNavMesh.SetDestination(targetTransform.position);
    }
}
