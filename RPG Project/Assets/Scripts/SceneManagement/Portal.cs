using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int sceneIndex = -1;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private PortalDestination destination;

        enum PortalDestination
        {
            Caladan,Dune,GiediPrime
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(SceneTransition());
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }

        private IEnumerator SceneTransition()
        {
            if (sceneIndex < 0)
            {
                Debug.Log("No Scene to load.");
                yield break;
            }
            
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            print("Portal Loaded");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            
            //player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            
            // Alternative
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue; // Self Portal Transition Condition
                
                if (portal.destination != destination) continue; // Portal Mismatch Condition needed for couple portals.
                
                return portal;
            }

            return null;
        }
    }

}
