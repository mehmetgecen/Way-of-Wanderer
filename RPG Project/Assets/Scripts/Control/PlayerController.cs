using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject enemy;

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }

            //Debug.DrawRay(ray.origin,ray.direction * 100); 
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }
    }
}
