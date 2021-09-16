using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Fubix.Saving;


namespace Fubix.Core
{
    public class triggerFight : MonoBehaviour, ISaveable
    {
        [SerializeField] float maxSpeed = 4.1f;
        [SerializeField] float stopDistance = 2;
        [SerializeField] GameObject movePlayerBackTarget;

        [SerializeField] string[] DisplayText;
        [SerializeField] GameObject DialogBox = null;
        [SerializeField] bool removeAfterUse = false;

        [SerializeField] bool ChangeScene = false;
        [SerializeField] int TargetScene = 0;

        GameObject Player;
        NavMeshAgent navMeshAgent;

        private bool readyToFight = true;// was true
        private bool stateLoaded = false;

        public bool moveToPlayer = true;
        public bool RepeatFight = true;
        public bool hasYesNoQuestion = false;
        Vector3 targetLocation;


        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            Player = GameObject.FindGameObjectWithTag("Player");
        }



        private void Update()
        {
            if (isInRange())
            {
                navMeshAgent.enabled = false;
            }

        }

        private bool isInRange()
        {
            return Vector3.Distance(targetLocation, transform.position) < stopDistance;
        }


        public void MoveTo(Vector3 destination, float speedFraction)
        {
            targetLocation = destination;
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }


        private void dialogMessage()
        {
            DialogBox.SetActive(true);

            // give Dialog Box the text to display
            DialogBox.GetComponent<dialogHolder>().onRecieveText(DisplayText, ChangeScene, TargetScene, hasYesNoQuestion);

            // it wasn't working right, had to use below to make it work, now I don't so idk.
           // Player.GetComponent<NavMeshAgent>().destination = movePlayerBackTarget.transform.position;
           // Player.GetComponent<PlayerAreaExplorererController>().Cancel();
            Player.GetComponent<PlayerAreaExplorererController>().controlsEnabled = false;

            // remove trigger box after triggered if you don't want
            // dialog to reappear when you cross again later.
            if (removeAfterUse) Destroy(this.gameObject);
        }



        private void OnTriggerEnter(Collider other)
        {
           // if (other.name == "Player" && stateLoaded && readyToFight)
             if (other.name == "Player" && readyToFight)
            {
                // if I don't want to repeat the fight later
                if (RepeatFight) readyToFight = false;
              
                // move game object to player
                if(moveToPlayer)MoveTo(other.transform.position, 1f);
                dialogMessage();
            }

        }



        public object CaptureState()
        {
            return readyToFight;
        }

        public void RestoreState(object state)
        {
            readyToFight = (bool)state;
           // stateLoaded = true;
        }

    }
}

