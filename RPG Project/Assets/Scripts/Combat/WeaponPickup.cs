using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon = null;
        [SerializeField] private float spawnTime = 5;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(SpawnPickup(spawnTime));
                
            }
            
            
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
    }
}
