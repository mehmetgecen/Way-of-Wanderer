using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour,IRaycastable
    {
        [SerializeField] private Weapon weapon = null;
        [SerializeField] private float spawnTime = 5;
        [SerializeField] private float healthToRestore = 0;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PickUp(other.gameObject);
            }
            
        }

        private void PickUp(GameObject subject)
        {
            if (weapon !=null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }

            if (healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            
            
            StartCoroutine(SpawnPickup(spawnTime));
        }

        IEnumerator SpawnPickup(float seconds)
        {
            HidePickup();
            yield return new WaitForSeconds(seconds);
            ShowPickup();

        }

        private void ShowPickup()
        {
            GetComponent<SphereCollider>().enabled = true;
            Transform pickUpTransform = transform;
            
            for (int i = 0; i < pickUpTransform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        private void HidePickup()
        {
            GetComponent<SphereCollider>().enabled = false;

            Transform pickUpTransform = transform;
            
            for (int i = 0; i < pickUpTransform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }


        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }

        public bool HandleRaycast(PlayerController controller)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(controller.gameObject);
            }

            return true;
        }
        
        
    }
}
