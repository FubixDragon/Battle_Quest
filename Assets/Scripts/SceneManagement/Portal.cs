using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
using Fubix.Saving;

namespace Fubix.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 5f;
        [SerializeField] float fadeWaitTime = 0.5f;
        [SerializeField] GameObject Fader;
     
        enum DestinationIdentifier
        {
            A,
            B,
            C,
            D
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            Fader.GetComponent<FaderMyWay>().startFadeOut(fadeOutTime);

            yield return new WaitForSeconds(fadeOutTime);
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();


            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            //wrapper.Load(); // already do this on Awake in savingWrapper
            GameObject otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            

            wrapper.Save();

            
            yield return new WaitForSeconds(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(GameObject otherPortal)
        {
          
            GameObject player = GameObject.FindWithTag("Player");
           // player.GetComponent<NavMeshAgent>().enabled = false;

            player.transform.position = otherPortal.GetComponent<Portal>().spawnPoint.transform.position;
            player.transform.rotation = otherPortal.GetComponent<Portal>().spawnPoint.transform.rotation;

            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private GameObject GetOtherPortal()
        {
            GameObject[] Portals = GameObject.FindGameObjectsWithTag("Portal");
        
            foreach (GameObject portal in Portals)
            {
                if (portal == this.gameObject) continue;
                if (portal.GetComponent<Portal>().destination != destination) continue;
                return portal;
            }
            return null;
        }

    }

}
