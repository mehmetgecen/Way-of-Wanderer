using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using UnityEngine.UIElements;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;
        
        private void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            // Action Priority Implementation
            if(health.IsDead()) return;
             
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nothing to do.");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if (target== null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                return true;
            }

            return false;    
        }
        
        private bool InteractWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }

                return true;
            }
            return false;

            //Debug.DrawRay(ray.origin,ray.direction * 100); 
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        
        
    }
}
